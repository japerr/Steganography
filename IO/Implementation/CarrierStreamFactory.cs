using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using CuttingEdge.Conditions;

namespace Steganography.IO.Implementation
{
	/// <summary>
	/// Carrier stream factory implementation
	/// </summary>
	public class CarrierStreamFactory : ICarrierStreamFactory
	{
		/// <summary>
		/// Dictionary of magic numbers and carrier stream types
		/// </summary>
		private ConcurrentDictionary<byte[], Type> _carrierStreamTypes = new ConcurrentDictionary<byte[], Type>();

		/// <summary>
		/// Register all the CarrierStream implementations defined in Yapi.IO assembly
		/// </summary>
		public void RegisterCarrierStreams()
		{
			RegisterCarrierStreams(typeof(CarrierStreamFactory).Assembly);
		}
		/// <summary>
		/// Register all the CarrierStream implementations defined in the given assembly
		/// </summary>
		/// <param name="assembly">Assembly to search for ICarrierStream implementations</param>
		public void RegisterCarrierStreams(Assembly assembly)
		{
			Condition.Requires(assembly).IsNotNull();

			RegisterCarrierStreams(assembly.GetTypes());
		}
		/// <summary>
		/// Register all the CarrierStream implementations defined in the given enumerable of types
		/// </summary>
		/// <param name="types">Enumerable of types to search for ICarrierStream implementations</param>
		public void RegisterCarrierStreams(IEnumerable<Type> types)
		{
			Condition.Requires(types).IsNotNull().IsNotEmpty();

			types.Where(t => !t.IsAbstract && t.GetInterfaces().Any(i => i == typeof(ICarrierStream)))
				.ToList()
				.ForEach(t => 
				{
					RegisterCarrierStream(t);
				});
		}
		/// <summary>
		/// Register a type as a ICarrierStream implementation
		/// </summary>
		/// <param name="type">Type to register</param>
		public void RegisterCarrierStream(Type type)
		{
			Condition.Requires(type).IsNotNull();

			if (!Attribute.IsDefined(type, typeof(MagicNumberAttribute)))
				throw new CarrierStreamMissingMagicNumberAttributeException(type);

			Attribute.GetCustomAttributes(type, typeof(MagicNumberAttribute), true)
				.Select(a => (MagicNumberAttribute)a)
				.ToList()
				.ForEach(a =>
				{
					Type existingDefinition = _carrierStreamTypes
						.Where(kvp => kvp.Key.SequenceEqual(a.MagicNumber))
						.Select(kvp => kvp.Value)
						.FirstOrDefault();

					if (existingDefinition != null)
						throw new DuplicateMagicNumberDefinedException(a.MagicNumber, existingDefinition, type);

					_carrierStreamTypes.TryAdd(((MagicNumberAttribute)a).MagicNumber, type);
				});
		}
		/// <summary>
		/// Build a carrier cluster stream with the given keysequence and ordered streams
		/// </summary>
		/// <typeparam name="TCarrierClusterStream">Type of carrier cluster stream to generate</typeparam>
		/// <param name="keySequence">IKeySequence instance</param>
		/// <param name="orderedStreams">Ordered list of streams, used to generate stream segements</param>
		/// <returns>New TCarrierClusterStream</returns>
		public TCarrierClusterStream BuildClusterStream<TCarrierClusterStream>(IKeySequence keySequence, IList<Stream> orderedStreams)
			where TCarrierClusterStream : ICarrierClusterStream, new()
		{
			Condition.Requires(keySequence).IsNotNull();
			Condition.Requires(orderedStreams).IsNotNull().IsNotEmpty();

			TCarrierClusterStream carrierClusterStream = new TCarrierClusterStream();
			carrierClusterStream.Initialize(keySequence, orderedStreams
				.Select(s => CreateCarrierStream(s)));

			return carrierClusterStream;
		}
		/// <summary>
		/// Create a carrier stream from the given stream and key sequence
		/// </summary>
		/// <param name="stream">Stream to find </param>
		/// <returns>A new carrier stream </returns>
		private ICarrierStream CreateCarrierStream(Stream stream)
		{
			Condition.Requires(stream).IsNotNull();

			int longestMagicNumber = _carrierStreamTypes.Max(kvp => kvp.Key.Length);
			byte[] magicNumberBuffer = new byte[longestMagicNumber];
			int bytesRead = stream.Read(magicNumberBuffer, 0, longestMagicNumber);
			IEnumerable<KeyValuePair<byte[], Type>> streamTypes = _carrierStreamTypes
				.Where(kvp => kvp.Key.SequenceEqual(magicNumberBuffer.Take(kvp.Key.Length)));

			if(streamTypes.Count() > 1)
				throw new Exception();

			KeyValuePair<byte[], Type> carrierDefinition = streamTypes.First();
			stream.Seek(0, SeekOrigin.Begin);

			return (ICarrierStream)Activator.CreateInstance(carrierDefinition.Value, stream);
		}
	}
}
