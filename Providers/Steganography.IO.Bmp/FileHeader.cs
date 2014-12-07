using System;
using System.IO;

namespace Steganography.IO.Bmp
{
	/// <summary>
	/// 
	/// </summary>
	/// <remarks>
	/// http://msdn.microsoft.com/en-us/library/windows/desktop/dd183392(v=vs.85).aspx
	/// </remarks>
	internal class FileHeader
	{
		/// <summary>
		/// Static file header size, in bytes
		/// </summary>
		public static int FILE_HEADER_SIZE = 14;

		public HeaderField Type { get; set; }
		/// <summary>
		/// Size of the BMP file in bytes
		/// </summary>
		public uint Size { get; set; }
		/// <summary>
		/// Actual value depends on the application that creates the image
		/// </summary>
		public uint Reserved { get; set; }
		/// <summary>
		/// Offset, i.e. starting address, of the byte where the bitmap image 
		/// data (pixel array) can be found.
		/// </summary>
		public uint BitmapOffset { get; set; }
		/// <summary>
		/// Size of the yeader, as an enum, used to determine which type of 
		/// BitmapCoreHeader to create
		/// </summary>
		public HeaderType HeaderSize { get; set; }

		public BitMask BitMask { get; set; }
		/// <summary>
		/// Bitmap width in pixels 
		/// </summary>
		public int Width 
		{
			get
			{
				return _bitmapCoreHeader.Width;
			}
		}
		/// <summary>
		/// Bitmap height in pixels 
		/// </summary>
		public int Height
		{
			get
			{
				return _bitmapCoreHeader.Height;
			}
		}
		/// <summary>
		/// Number of bits per pixel
		/// </summary>
		public short BitsPerPixel
		{
			get
			{
				return _bitmapCoreHeader.BitsPerPixel;
			}		
		}
		/// <summary>
		/// Number of bytes per pixel
		/// </summary>
		public int BytesPerPixel
		{
			get
			{
				return (int)Math.Ceiling((double)_bitmapCoreHeader.BitsPerPixel / 8);
			}
		}

		private BitmapCoreHeader _bitmapCoreHeader = null;

		/// <summary>
		/// Constructor initialized with the bitmap header info
		/// </summary>
		/// <param name="headerInfo">Bitmap header info</param>
		public FileHeader(Stream stream)
		{
			if(stream == null)
				throw new ArgumentNullException("headerInfo");

			byte[] fileHeader = new byte[18];
			if(stream.Read(fileHeader, 0, fileHeader.Length) < fileHeader.Length)
				throw new InvalidOperationException("Unable to read bitmap file header, less than 18bytes read.");	

			Type = (HeaderField)BitConverter.ToUInt16(fileHeader, 0);
			Size = BitConverter.ToUInt32(fileHeader, 2);
			Reserved = BitConverter.ToUInt32(fileHeader, 6);
			BitmapOffset = BitConverter.ToUInt32(fileHeader, 10);
			HeaderSize = (HeaderType)BitConverter.ToUInt32(fileHeader, 14);

			switch (HeaderSize)
			{
				case HeaderType.BITMAPCOREHEADER:
					_bitmapCoreHeader = new BitmapCoreHeader(stream);
					break;

				case HeaderType.BITMAPINFOHEADER:
				case HeaderType.BITMAPCOREHEADER2:
				case HeaderType.BITMAPV2INFOHEADER:
				case HeaderType.BITMAPV3INFOHEADER:
				case HeaderType.BITMAPV4HEADER:
				case HeaderType.BITMAPV5HEADER:
					BitmapInfoHeader infoHeader = new BitmapInfoHeader(stream);
					_bitmapCoreHeader = infoHeader;
					if(infoHeader.CompressionType == CompressionType.BI_BITFIELDS 
						|| infoHeader.CompressionType == CompressionType.BI_ALPHABITFIELDS)
						BitMask = new BitMask(HeaderSize, infoHeader.CompressionType, stream);

					break;
			}
		}
	}
}
