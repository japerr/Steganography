 using System;
using System.IO;

namespace Steganography.IO.Bmp
{
	/// <summary>
	/// Carrier stream implementiation with a Bitmap backing store
	/// </summary>
	/// <remarks>
	/// http://en.wikipedia.org/wiki/BMP_file_format
	/// </remarks>
	[MagicNumber(new byte[] { 0x42, 0x4D })] //BM – Windows 3.1x, 95, NT, ... etc.
	public class BmpCarrierStream : BaseCarrierStream
	{
		/// <summary>
		/// Bitmap file header
		/// </summary>
		private FileHeader _fileHeader = null;
		/// <summary>
		/// Bitmap DIB info header
		/// </summary>
		private InfoHeader _dibInfoHeader = null;
		/// <summary>
		/// Bitmap color table
		/// </summary>
		private BGRA[] _colorTable = null;

		/// <summary>
		/// Constructor initialized with the backing stream
		/// </summary>
		/// <param name="stream">Backing stream instance</param>
		public BmpCarrierStream(Stream stream)
			: base(stream)
		{
			byte[] headerInfo = new byte[18];
			BackingStream.Read(headerInfo, 0, headerInfo.Length);
			_fileHeader = new FileHeader(headerInfo);

			DIBHeaderTypes dibHeaderType = (DIBHeaderTypes)BitConverter.ToUInt32(headerInfo, 14);
			byte[] dibHeaderInfo = null;
			switch (dibHeaderType)
			{
				case DIBHeaderTypes.BITMAPINFOHEADER:
					_dibInfoHeader = new InfoHeader();
					break;

				case DIBHeaderTypes.BITMAPV2INFOHEADER:
					_dibInfoHeader = new V2InfoHeader();
					break;

				case DIBHeaderTypes.BITMAPV3INFOHEADER:
					_dibInfoHeader = new V3InfoHeader();
					break;

				case DIBHeaderTypes.BITMAPV4HEADER:
					_dibInfoHeader = new V4InfoHeader();
					break;

				case DIBHeaderTypes.BITMAPV5HEADER:
					_dibInfoHeader = new V5InfoHeader();
					break;

				default:
					throw new UnsupportedDIBHeaderTypeException((uint)dibHeaderType);
			}

			dibHeaderInfo = new byte[_dibInfoHeader.Size - 4];
			BackingStream.Read(dibHeaderInfo, 0, dibHeaderInfo.Length);

			_dibInfoHeader.Initialize(dibHeaderInfo);			

			Length = (long)(_dibInfoHeader.BitsPerPixel / 8
				* _dibInfoHeader.Width * _dibInfoHeader.Height / 8);
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

			//_stream.Position = position;
			//byte[] carrierValues = new byte[count];
			//_stream.Read(carrierValues, 0, count);

			//int readIndex = 0;
			//for (int i = 0; i < count; ++i)
			//{
			//	if (i != 0 && i % 8 == 0)
			//		++readIndex;
				
			//	buffer[i + offset] = 

			//	Console.Write("Initial: {0}", DisplayBits(readValues[readIndex]));
			//	Console.Write(" reading({0}): {1}", (7 - i % 8), GetBit(_carrier[i], (byte)(7 - i % 8)) ? "1" : "0");

			//	readValues[readIndex] = SetBit(readValues[readIndex], (byte)(7 - i % 8), GetBit(_carrier[i], 0));

			//	Console.WriteLine(" after: {0}", DisplayBits(readValues[readIndex]));
			//}
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
