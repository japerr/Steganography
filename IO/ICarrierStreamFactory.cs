using System.Collections.Generic;
using System.IO;

namespace Steganography.IO
{
	/// <summary>
	/// Interface describing the expected functionality of a CarrierStreamFactory
	/// </summary>
	public interface ICarrierStreamFactory
	{
		/// <summary>
		/// Build a carrier cluster stream with the given keysequence and ordered streams
		/// </summary>
		/// <param name="keySequence">IKeySequence instance</param>
		/// <param name="orderedStreams">Ordered list of streams, used to generate stream segements</param>
		/// <returns>New TCarrierClusterStream</returns>
		BaseCarrierClusterStream BuildClusterStream(string name, IKeySequence keySequence, IList<Stream> orderedStreams);
		/// <summary>
		/// Create a carrier stream from the given stream and key sequence
		/// </summary>
		/// <param name="stream">Stream to find </param>
		/// <returns>A new carrier stream </returns>
		BaseCarrierStream BuildCarrierStream(Stream stream);
	}
}
