using System.IO;
using Common.Logging;
using System;

namespace Steganography.IO
{
	/// <summary>
	/// Base carrier stream implementiation
	/// </summary>
	public abstract class BaseCarrierStream : ICarrierStream
	{
		/// <summary>
		/// Backing logger instance
		/// </summary>
		private ILog _logger = LogManager.GetCurrentClassLogger();
		/// <summary>
		/// Logger instance
		/// </summary>
		protected ILog Logger
		{
			get
			{
				return _logger;
			}
		}
		/// <summary>
		/// Backing binary stream
		/// </summary>
		protected Stream BackingStream { get; private set; }

		/// <summary>
		/// Indicates the stream can be read
		/// </summary>
		/// <remarks>true if the stream supports reading; otherwise, false</remarks>
		public virtual bool CanRead
		{
			get
			{
				return BackingStream.CanRead;
			}
		}
		/// <summary>
		/// Indicates the stream allows seeking
		/// </summary>
		/// <remarks>true if the stream supports seeking; otherwise, false</remarks>
		public virtual bool CanSeek
		{
			get
			{
				return BackingStream.CanSeek;
			}
		}
		/// <summary>
		/// Indicates the stream is writable
		/// </summary>
		/// <remarks>A long value representing the length of the stream in bytes</remarks>
		public virtual bool CanWrite
		{
			get
			{
				return BackingStream.CanWrite;
			}
		}
		/// <summary>
		/// Length in bytes that can be written in this carrier stream
		/// </summary>
		public long Length { get; protected set; }

		/// <summary>
		/// Initialization method which sets the backing stream
		/// </summary>
		/// <param name="stream">Backing stream</param>
		public virtual void Initialize(Stream stream)
		{
			if (stream == null)
				throw new ArgumentNullException("stream");

			if(BackingStream != null)
				throw new InvalidOperationException("CarrierStream is already initialized.");

			BackingStream = stream;
		}
		/// <summary>
		/// Read a given number of bytes from the current stream position
		/// </summary>
		/// <param name="position">Position in the carrier stream</param>
		/// <param name="buffer">Byte array to populate while reading stream</param>
		/// <param name="offset">Zero-based byte offset in buffer at which to begin storing the data</param>
		/// <param name="count">The maximum number of bytes to be read from the current stream</param>
		/// <returns>The total number of bytes read into the buffer</returns>
		public abstract int Read(long position, byte[] buffer, int offset, int count);
		/// <summary>
		/// Write a given number of bytes from the current stream position
		/// </summary>
		/// <param name="position">Position in the carrier stream</param>
		/// <param name="buffer">Byte array to write to the current stream position</param>
		/// <param name="offset">Zero-based byte offset in buffer at which to begin reading the data</param>
		/// <param name="count">The maximum number of bytes to be read from the buffer</param>
		public abstract void Write(long position, byte[] buffer, int offset, int count);
		/// <summary>
		/// Clear the underlying buffer of the stream
		/// </summary>
		public virtual void Flush()
		{
			_logger.Debug("Flushing");
			BackingStream.Flush();
		}
		/// <summary>
		/// Free the stream instance
		/// </summary>
		public virtual void Dispose()
		{
			if (BackingStream == null)
				return;

			BackingStream.Flush();

			_logger.Debug("Dispose");
			BackingStream.Dispose();
			BackingStream = null;
		}
	}
}
