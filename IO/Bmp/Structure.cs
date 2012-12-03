using System;
using System.IO;
using CuttingEdge.Conditions;

namespace Steganography.IO.Bmp
{
	/// <summary>
	/// Class which handles the bitmap file structures
	/// </summary>
	/// <remarks>
	/// http://en.wikipedia.org/wiki/BMP_file_format
	/// </remarks>
	internal class Structure
	{
		#region Properties
		/// <summary>
		/// Size of the BMP file in bytes
		/// </summary>
		public int ByteLength { get; set; }
		/// <summary>
		/// The offset, i.e. starting address, of the byte where the bitmap image data (pixel array) can be found.
		/// </summary>
		public int ImageDataOffset { get; set; }
		/// <summary>
		/// Size of this header, also indicates the header type
		/// </summary>
		public DIBHeaderTypes DIBHeaderType { get; set; }
		/// <summary>
		/// Bitmap width in pixels
		/// </summary>
		public int Width { get; set; }
		/// <summary>
		/// Bitmap height in pixels
		/// </summary>
		public int Height { get; set; }
		/// <summary>
		/// Bits per pixel, which is the color depth of the image. 
		/// Typical values are 1, 4, 8, 16, 24 and 32.
		/// </summary>
		public int BitsPerPixel { get; set; }
		/// <summary>
		/// Size of the raw bitmap data (see below), and should not be confused with the file size.
		/// </summary>
		public int ImageDataLength { get; set; }
		/// <summary>
		/// Total number of bytes necessary to store one row of pixels
		/// </summary>
		public int RowSize { get; set; }
		#endregion

		/// <summary>
		/// Constructor initalized with the bitmap image stream
		/// </summary>
		/// <param name="stream">Bitmap stream</param>
		public Structure(Stream stream)
		{
			Condition.Requires(stream).IsNotNull();

			byte[] info = new byte[16];
			stream.Read(info, 0, info.Length);
			ByteLength = BitConverter.ToInt32(info, 0);
			ImageDataOffset = BitConverter.ToInt32(info, 8);
			DIBHeaderType = (DIBHeaderTypes)BitConverter.ToInt32(info, 12);

			switch (DIBHeaderType)
			{
				case DIBHeaderTypes.BITMAPINFOHEADER:
					ParseBITMAPINFOHEADER(stream);
					break;

				case DIBHeaderTypes.BITMAPV3INFOHEADER:
					ParseBITMAPV3INFOHEADER(stream);
					break;

			}

			RowSize = (BitsPerPixel * Width + 31) / 32 * 4;
		}

		/// <summary>
		/// DIB header parser for BITMAPINFOHEADER
		/// </summary>
		/// <param name="stream">Bitmap stream</param>
		private void ParseBITMAPINFOHEADER(Stream stream)
		{
			byte[] dimensions = new byte[20];
			stream.Read(dimensions, 0, dimensions.Length);
			Width = BitConverter.ToInt32(dimensions, 0);
			Height = Math.Abs(BitConverter.ToInt32(dimensions, 4));
			BitsPerPixel = BitConverter.ToInt16(dimensions, 10);
			ImageDataLength = BitConverter.ToInt32(dimensions, 16);

			if (ImageDataLength == 0)
				ImageDataLength = Height * (BitsPerPixel * Width + 31) / 32 * 4;
		}
		/// <summary>
		/// DIB header parser for BITMAPV3INFOHEADE
		/// </summary>
		/// <param name="stream">Bitmap stream</param>
		private void ParseBITMAPV3INFOHEADER(Stream stream)
		{

		}
		/// <summary>
		/// DIB header parser for BITMAPCOREHEADER_OR_OS21XBITMAPHEADER
		/// </summary>
		/// <param name="stream">Bitmap stream</param>
		private void ParseBITMAPCOREHEADER_OR_OS21XBITMAPHEADER(Stream stream)
		{
			byte[] dimensions = new byte[8];
			stream.Read(dimensions, 0, dimensions.Length);
			Width = BitConverter.ToInt32(dimensions, 0);
			Height = Math.Abs(BitConverter.ToInt32(dimensions, 2));
			BitsPerPixel = BitConverter.ToInt16(dimensions, 6);
			ImageDataLength = Height * (BitsPerPixel * Width + 31) / 32 * 4;
		}
	}
}
