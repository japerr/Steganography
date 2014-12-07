using System;
using System.IO;

namespace Steganography.IO.Bmp
{
	/// <summary>
	/// Carrier stream implementation with a Bitmap backing store
	/// </summary>
	/// <remarks>
	/// http://en.wikipedia.org/wiki/BMP_file_format
	/// </remarks>
	[CarrierStream(new byte[] { 0x42, 0x4D })] //BM – Windows 3.1x, 95, NT, ... etc.
	public class BmpCarrierStream : BaseCarrierStream
	{
		/// <summary>
		/// Bitmap file header
		/// </summary>
		private FileHeader _fileHeader = null;

		/// <summary>
		/// Initialization method which sets the backing stream
		/// </summary>
		/// <param name="stream">Backing stream</param>
		public override void Initialize(Stream stream)
		{
			base.Initialize(stream);
			
			_fileHeader = new FileHeader(BackingStream);

			if (_fileHeader.BitsPerPixel <= 8)
				throw new InvalidOperationException("Indexed bitmaps currently not a supported carrier stream");

			Length = (long)((_fileHeader.Width * _fileHeader.Height) / 8);
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
			int backingCount = count * 8 * _fileHeader.BytesPerPixel;

			BackingStream.Position = _fileHeader.BitmapOffset + (position * 8 * _fileHeader.BytesPerPixel);
			byte[] carrierValues = new byte[backingCount];
			BackingStream.Read(carrierValues, 0, backingCount);

			int read = 0;
			int readIndex = 0;
			for (int i = 0, carrierIndex = 0; i < count * 8; ++i, carrierIndex += _fileHeader.BytesPerPixel)
			{
				read = read << 1;
				read |= carrierValues[carrierIndex] & 0x01;

				if (i == 0 || (i + 1) % 8 != 0)
					continue;

				buffer[readIndex + offset] = (byte)read;
				read = 0;
				++readIndex;
			}

			return readIndex;
		}
		/// <summary>
		/// Write a given number of bytes from the current stream position
		/// </summary>
		/// <param name="position">Position in the carrier stream</param>
		/// <param name="buffer">Byte array to write to the current stream position</param>
		/// <param name="offset">Zero-based byte offset in buffer at which to begin reading the data</param>
		/// <param name="count">The maximum number of bytes to be read from the buffer</param>
		/// <remarks>
		/// http://graphics.stanford.edu/~seander/bithacks.html#ConditionalSetOrClearBitsWithoutBranching
		/// </remarks>
		public override void Write(long position, byte[] buffer, int offset, int count)
		{
			int pixelCount = count * 8 *_fileHeader.BytesPerPixel;

			BackingStream.Position = _fileHeader.BitmapOffset + (position * 8 * _fileHeader.BytesPerPixel);
			byte[] pixels = new byte[pixelCount];
			if (BackingStream.Read(pixels, 0, pixelCount) != pixelCount)
				throw new InvalidOperationException(
					string.Format("Unable to read enough carrier pixels for writing, {0}bytes", pixelCount));

			int pixelIndex = 0;
			for(int i = 0; i < count; ++i)
			{
				//Reverse the byte, so the value isn't written backwards
				// http://stackoverflow.com/a/2602885
				byte reverse = (byte)((buffer[offset + i] & 0xF0) >> 4 | (buffer[offset + i] & 0x0F) << 4);
				reverse = (byte)((reverse & 0xCC) >> 2 | (reverse & 0x33) << 2);
				reverse = (byte)((reverse & 0xAA) >> 1 | (reverse & 0x55) << 1);



				int writeIndex = 0;
				while (writeIndex < 8)
				{
					//http://graphics.stanford.edu/~seander/bithacks.html#ConditionalSetOrClearBitsWithoutBranching
					pixels[pixelIndex] = (byte)((pixels[pixelIndex] & ~0x01) | (-(reverse >> writeIndex++) & 0x01));
					pixelIndex += _fileHeader.BytesPerPixel;
				}
			}

			BackingStream.Position = _fileHeader.BitmapOffset + (position * 8 * _fileHeader.BytesPerPixel);
			BackingStream.Write(pixels, 0, pixelCount);
			BackingStream.Flush();
		}
	}
}
