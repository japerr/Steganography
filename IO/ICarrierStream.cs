using System;
using System.IO;

namespace Steganography.IO
{
	/// <summary>
	/// Base carrier stream interface
	/// </summary>
	public interface ICarrierStream : IDisposable
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
		/// Length of stream
		/// </summary>
		long Length { get; }

		/// <summary>
		/// Initialization method which sets the backing stream
		/// </summary>
		/// <param name="stream">Backing stream</param>
		void Initialize(Stream stream);
		/// <summary>
		/// Read a given number of bytes from the current stream position
		/// </summary>
		/// <param name="position">Current parent position, used to find the relative location in the buffer</param>
		/// <param name="buffer">Byte array to populate while reading stream</param>
		/// <param name="offset">Zero-based byte offset in buffer at which to begin storing the data</param>
		/// <param name="count">The maximum number of bytes to be read from the current stream</param>
		/// <returns>The total number of bytes read into the buffer</returns>
		int Read(long position, byte[] buffer, int offset, int count);
		/// <summary>
		/// Write a given number of bytes from the current stream position
		/// </summary>
		/// <param name="position">Current parent position, used to find the relative location in the buffer</param>
		/// <param name="buffer">Byte array to write to the current stream position</param>
		/// <param name="offset">Zero-based byte offset in buffer at which to begin reading the data</param>
		/// <param name="count">The maximum number of bytes to be read from the buffer</param>
		void Write(long position, byte[] buffer, int offset, int count);
		/// <summary>
		/// Clear the underlying buffer
		/// </summary>
		void Flush();
	}
}
