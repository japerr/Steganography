using System;
using System.IO;
using Common.Logging;
using CuttingEdge.Conditions;

namespace Steganography.IO.Png
{
	/// <summary>
	/// Carrier stream implementiation with a PNG backing store
	/// </summary>
	/// <remarks>
	/// http://en.wikipedia.org/wiki/PNG_file_format
	/// </remarks>
	[MagicNumber(new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A })]
	public class PngCarrierStream : BaseCarrierStream
	{
		/// <summary>
		/// Constructor initialized with the backing stream
		/// </summary>
		/// <param name="stream">Backing stream instance</param>
		public PngCarrierStream(Stream stream)
			: base(stream)
		{
		}

		/// <summary>
		/// Read a given number of bytes from the current stream position
		/// </summary>
		/// <param name="position">Position in the carrier stream</param>
		/// <param name="buffer">Byte array to populate while reading stream</param>
		/// <param name="offset">Zero-based byte offset in buffer at which to begin storing the data</param>
		/// <param name="count">The maximum number of bytes to be read from the current stream</param>
		/// <returns>The total number of bytes read into the buffer</returns>
		public override int Read(long position, byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// Write a given number of bytes from the current stream position
		/// </summary>
		/// <param name="position">Position in the carrier stream</param>
		/// <param name="buffer">Byte array to write to the current stream position</param>
		/// <param name="offset">Zero-based byte offset in buffer at which to begin reading the data</param>
		/// <param name="count">The maximum number of bytes to be read from the buffer</param>
		public override void Write(long position, byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}
	}
}
