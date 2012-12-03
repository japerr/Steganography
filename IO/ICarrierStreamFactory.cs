using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Steganography.IO
{
	/// <summary>
	/// Interface describing the expected functionality of a CarrierStreamFactory
	/// </summary>
	public interface ICarrierStreamFactory
	{
		/// <summary>
		/// Register all the CarrierStream implementations defined in Yapi.IO assembly
		/// </summary
		void RegisterCarrierStreams();
		/// <summary>
		/// Register all the CarrierStream implementations defined in the given assembly
		/// </summary>
		/// <param name="assembly">Assembly to search for ICarrierStream implementations</param>
		void RegisterCarrierStreams(Assembly assembly);
		/// <summary>
		/// Register all the CarrierStream implementations defined in the given enumerable of types
		/// </summary>
		/// <param name="types">Enumerable of types to search for ICarrierStream implementations</param>
		void RegisterCarrierStreams(IEnumerable<Type> types);
		/// <summary>
		/// Register a type as a ICarrierStream implementation
		/// </summary>
		/// <param name="type">Type to register</param>
		void RegisterCarrierStream(Type type);
		/// <summary>
		/// Build a carrier cluster stream with the given keysequence and ordered streams
		/// </summary>
		/// <param name="keySequence">IKeySequence instance</param>
		/// <param name="orderedStreams">Ordered list of streams, used to generate stream segements</param>
		/// <returns>New TCarrierClusterStream</returns>
		TCarrierClusterStream BuildClusterStream<TCarrierClusterStream>(IKeySequence keySequence, IList<Stream> orderedStreams)
			where TCarrierClusterStream : ICarrierClusterStream, new();
	}
}
