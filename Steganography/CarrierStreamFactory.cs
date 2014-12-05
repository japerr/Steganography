using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using Steganography.IO;

namespace Steganography
{
	/// <summary>
	/// Carrier stream factory implementation
	/// </summary>
	public class CarrierStreamFactory : ICarrierStreamFactory
	{
		/// <summary>
		/// Flag indicating that cluster stream existance should be enforced.
		/// </summary>
		private bool _requireClusterStreams = true;
		/// <summary>
		/// imported carrier cluster streams and thier metadata
		/// </summary>
		[ImportMany(typeof(BaseCarrierClusterStream), RequiredCreationPolicy = CreationPolicy.NonShared)]
		private ExportFactory<BaseCarrierClusterStream, ICarrierClusterStreamMetadata>[] _carrierClusterStreams { get; set; }
		/// <summary>
		/// imported carrier streams and thier metadata
		/// </summary>
		[ImportMany(typeof(BaseCarrierStream), RequiredCreationPolicy = CreationPolicy.NonShared)]
		private ExportFactory<BaseCarrierStream, ICarrierStreamMetadata>[] _carrierStreams { get; set; }
		/// <summary>
		/// Dictionary of names and carrier cluster stream types
		/// </summary>
		private ConcurrentDictionary<string, ExportFactory<BaseCarrierClusterStream, ICarrierClusterStreamMetadata>> _carrierClusterStreamTypes =
			new ConcurrentDictionary<string, ExportFactory<BaseCarrierClusterStream, ICarrierClusterStreamMetadata>>();
		/// <summary>
		/// Dictionary of magic numbers and carrier stream types
		/// </summary>
		private ConcurrentDictionary<byte[], ExportFactory<BaseCarrierStream, ICarrierStreamMetadata>> _carrierStreamTypes =
			new ConcurrentDictionary<byte[], ExportFactory<BaseCarrierStream, ICarrierStreamMetadata>>();		

		/// <summary>
		/// Constructor initialized with the provider directory. 
		/// The providers in the provider directory the registered
		/// </summary>
		/// <param name="providerDirectory">Provider directory</param>
		/// <param name="requireClusterStreams">
		///	Flag indicating that cluster stream existance should be enforced.
		///	If true and no ICarrierClusterStream implementations are found, a NoCarrierClusterStreamsFoundException is thrown.
		///	No exception is thrown if false and no ICarrierClusterStream implementations are found.
		///	</param>
		///	<exception cref="System.ArgumentNullException">Thrown if providerDirectory is null</exception>
		///	<exception cref="System.ArgumentException">
		///	Thrown when:
		///		the providerDirectory is empty or whitespace
		///		the providerDirectory is not a valid or existing directory
		///		the providerDirectory is not readable directory
		///	</exception>
		public CarrierStreamFactory(string providerDirectory, bool requireClusterStreams = true)
		{
			if(providerDirectory == null)
				throw new ArgumentNullException("providerDirectory");

			if(string.IsNullOrWhiteSpace(providerDirectory))
				throw new ArgumentException("providerDirectory", "pluginDirectory cannot be empty or white space.");

			if(!Directory.Exists(providerDirectory))
				throw new ArgumentException("providerDirectory", "pluginDirectory is not a valid or existing directory.");

			if(!CanReadDirectory(providerDirectory))
				throw new ArgumentException("providerDirectory", "Unable to read from specified pluginDirectory");

			_requireClusterStreams = requireClusterStreams;
			RegisterCarrierProviders(providerDirectory);
		}

		/// <summary>
		/// Determine if the path is readable
		/// </summary>
		/// <param name="path">plugin path</param>
		/// <returns>True if read is allow, false otherwise</returns>
		/// <remarks>http://stackoverflow.com/a/11710169</remarks>
		private static bool CanReadDirectory(string path)
		{
			bool readAllow = false;
			bool readDeny = false;
			DirectorySecurity directorySecurity = Directory.GetAccessControl(path);
			if (directorySecurity == null)
				return false;

			AuthorizationRuleCollection authorizationRules = directorySecurity.GetAccessRules(true, true, typeof(SecurityIdentifier));
			if (authorizationRules == null)
				return false;

			foreach (FileSystemAccessRule rule in authorizationRules)
			{
				if ((FileSystemRights.Read & rule.FileSystemRights) != FileSystemRights.Read)
					continue;

				if (rule.AccessControlType == AccessControlType.Allow)
					readAllow = true;
				else if (rule.AccessControlType == AccessControlType.Deny)
					readDeny = true;
			}

			return readAllow && !readDeny;
		}
		/// <summary>
		/// Register all the ICarrierClusterStream and ICarrierStream implementations defined in the providerDirectory
		/// </summary>
		/// <param name="providerDirectory">Provider directory to scan for carrier implmentations</param>
		/// <exception cref="Steganography.NoCarrierClusterStreamsFoundException">
		///		Thrown when no ICarrierClusterStreams are found and cluster streams are required.
		///	</exception>
		/// <exception cref="Steganography.NoCarrierStreamsFoundException">Thrown when no ICarrierStreams are found.</exception>
		private void RegisterCarrierProviders(string providerDirectory)
		{
			CompositionContainer container = new CompositionContainer(
				new DirectoryCatalog(providerDirectory), CompositionOptions.DisableSilentRejection);

			container.SatisfyImportsOnce(this);

			if (_requireClusterStreams && _carrierClusterStreams.Count() == 0)
				throw new NoCarrierClusterStreamsFoundException(providerDirectory);

			if (_carrierStreams.Count() == 0)
				throw new NoCarrierStreamsFoundException(providerDirectory);

			if (_carrierClusterStreams.Count() > 0)
				_carrierClusterStreams.ToList()
					.AsParallel()
					.ForAll(RegisterCarrierClusters);

			_carrierStreams.ToList()
				.AsParallel()
				.ForAll(RegisterCarrierStreams);
		}
		/// <summary>
		/// Regsiter the carrier cluster export factory with the internal ConcurrentDictionary
		/// </summary>
		/// <param name="factory">ExportFactory instance for the carrier cluster and metadata</param>
		/// <exception cref="Steganography.DuplicateClusterNameDefinedException">
		///		Thrown if an existing ICarrierClusterStream is already defined by the given metadata name.
		///	</exception>
		private void RegisterCarrierClusters(ExportFactory<BaseCarrierClusterStream, ICarrierClusterStreamMetadata> factory)
		{
			Type existingDefinition = _carrierClusterStreamTypes
				.Where(kvp => kvp.Key.SequenceEqual(factory.Metadata.Name))
				.Select(kvp => kvp.Value.GetType())
				.FirstOrDefault();

			if (existingDefinition != null)
				throw new DuplicateClusterNameDefinedException(factory.Metadata.Name,
					existingDefinition, factory.CreateExport().Value.GetType());

			_carrierClusterStreamTypes.TryAdd(factory.Metadata.Name, factory);
		}
		/// <summary>
		/// Regsiter the carrier stream export factory with the internal ConcurrentDictionary
		/// </summary>
		/// <param name="factory">ExportFactory instance for the carrier stream and metadata</param>
		/// <exception cref="Steganography.DuplicateMagicNumberDefinedException">Thrown if an existing ICarrierStream is already defined by the given metadata magic number.</exception>
		private void RegisterCarrierStreams(ExportFactory<BaseCarrierStream, ICarrierStreamMetadata> factory)
		{
			Type existingDefinition = _carrierStreamTypes
				.Where(kvp => kvp.Key.SequenceEqual(factory.Metadata.MagicNumber))
				.Select(kvp => kvp.Value.GetType())
				.FirstOrDefault();

			if (existingDefinition != null)
				throw new DuplicateMagicNumberDefinedException(factory.Metadata.MagicNumber,
					existingDefinition, factory.CreateExport().Value.GetType());

			_carrierStreamTypes.TryAdd(factory.Metadata.MagicNumber, factory);
		}
		/// <summary>
		/// Build a carrier cluster stream with the given keysequence and ordered streams
		/// </summary>
		/// <typeparam name="TCarrierClusterStream">Type of carrier cluster stream to generate</typeparam>
		/// <param name="keySequence">IKeySequence instance</param>
		/// <param name="orderedStreams">Ordered list of streams, used to generate stream segements</param>
		/// <returns>New TCarrierClusterStream</returns>
		public BaseCarrierClusterStream BuildClusterStream(string name, IKeySequence keySequence, IList<Stream> orderedStreams)
		{
			if (keySequence == null)
				throw new ArgumentNullException("keySequence");

			if (orderedStreams == null)
				throw new ArgumentNullException("orderedStreams");

			if (orderedStreams.Count == 0)
				throw new ArgumentOutOfRangeException("orderedStreams", "orderedStreams cannot be empty");

			if (!_carrierClusterStreamTypes.ContainsKey(name))
				throw new InvalidOperationException(string.Format("CarrierClusterStream not found for Name {0}", name));

			ExportFactory<BaseCarrierClusterStream, ICarrierClusterStreamMetadata> factory = 
				_carrierClusterStreamTypes[name];

			BaseCarrierClusterStream result = factory.CreateExport().Value;
			result.Initialize(keySequence, orderedStreams.Select(s => BuildCarrierStream(s)));

			return result;
		}
		/// <summary>
		/// Create a carrier stream from the given stream and key sequence
		/// </summary>
		/// <param name="stream">Stream to find </param>
		/// <returns>A new carrier stream </returns>
		public BaseCarrierStream BuildCarrierStream(Stream stream)
		{
			if (stream == null)
				throw new ArgumentNullException("stream");

			int longestMagicNumber = _carrierStreamTypes.Max(kvp => kvp.Key.Length);
			byte[] magicNumberBuffer = new byte[longestMagicNumber];
			int bytesRead = stream.Read(magicNumberBuffer, 0, longestMagicNumber);
			ExportFactory<BaseCarrierStream, ICarrierStreamMetadata> factory = 
				_carrierStreamTypes.Where(kvp => kvp.Key.SequenceEqual(magicNumberBuffer.Take(kvp.Key.Length)))
					.Select(kvp => kvp.Value)
					.FirstOrDefault();

			if (factory == null)
				throw new InvalidOperationException(string.Format("CarrierStream not found for MagicNumber {0}", 
					Convert.ToBase64String(magicNumberBuffer)));

			stream.Seek(0, SeekOrigin.Begin);

			BaseCarrierStream carrierStream = factory.CreateExport().Value;
			carrierStream.Initialize(stream);

			return carrierStream;
		}
	}
}
