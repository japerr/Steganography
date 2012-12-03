using System;
using System.Collections.Generic;
using System.IO;

namespace Steganography.IO
{
	/// <summary>
	/// Public carrier stream interface
	/// </summary>
	public interface ICarrierClusterStream : IDisposable
	{
		/// <summary>
		/// Indicates the stream can be read
		/// </summary>
		/// <remarks>true if the stream supports reading; otherwise, false</remarks>
		bool CanRead { get; }
		/// <summary>
		/// Indicates the stream allows seeking
		/// </summary>
		/// <remarks>true if the stream supports seeking; otherwise, false</remarks>
		bool CanSeek { get; }
		/// <summary>
		/// Indicates the stream is writable
		/// </summary>
		/// <remarks>A long value representing the length of the stream in bytes</remarks>
		bool CanWrite { get; }
		/// <summary>
		/// Gets the length in bytes of the stream
		/// </summary>
		long Length { get; }
		/// <summary>
		/// Gets or sets the position within the current stream
		/// </summary>
		long Position { get; set; }

		/// <summary>
		/// Initialize the cluster stream with key sequence instance and carrierStream list
		/// </summary>
		/// <param name="keySequence">Key sequence instance</param>
		/// <param name="carrierStreams">Carrier streams to initialized with</param>
		void Initialize(IKeySequence keySequence, IEnumerable<ICarrierStream> carrierStreams);
		/// <summary>
		/// Move the current postion based on the origin and the offset
		/// </summary>
		/// <param name="offset">Distance from the origin to set the position</param>
		/// <param name="origin">The initial position to calculate the new position from</param>
		/// <returns>The new position</returns>
		long Seek(long offset, SeekOrigin origin);
		/// <summary>
		/// Sets the length of the current stream
		/// </summary>
		/// <param name="value">The desired length of the current stream in bytes</param>
		void SetLength(long value);
		/// <summary>
		/// Reads the carrier stream cluster given the offset and count
		/// </summary>
		/// <param name="buffer">Byte array to populate while reading stream</param>
		/// <param name="offset">Zero-based byte offset in buffer at which to begin storing the data</param>
		/// <param name="count">The maximum number of bytes to be read from the current stream</param>
		/// <returns>The total number of bytes read into the buffer</returns>
		int Read(byte[] buffer, int offset, int count);
		/// <summary>
		/// Writes to the carrier stream cluster given the offset and count
		/// </summary>
		/// <param name="buffer">An array of bytes. This method copies count bytes from buffer to the current stream</param>
		/// <param name="offset">The zero-based byte offset in buffer at which to begin copying bytes to the
		/// current stream</param>
		/// <param name="count">The number of bytes to be written to the current stream</param>
		void Write(byte[] buffer, int offset, int count);
		/// <summary>
		/// clears all streams and forces the underlying carrier streams to flush
		/// </summary>
		void Flush();
	}
}
