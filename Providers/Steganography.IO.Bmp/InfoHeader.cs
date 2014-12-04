using System;

namespace Steganography.IO.Bmp
{
	/// <summary>
	/// all Windows versions since Windows 3.0
	/// </summary>
	/// <remarks>
	/// Removes RLE-24 and Huffman 1D compression. Adds 16bpp and 32bpp pixel formats. 
	/// Adds optional RGB bit masks.
	/// http://en.wikipedia.org/wiki/BMP_file_format#DIB_header_.28bitmap_information_header.29
	/// </remarks>
	internal class InfoHeader
	{
		/// <summary>
		/// Size of this header, also indicates the header type
		/// </summary>
		public virtual uint Size 
		{
			get
			{
				return (uint)DIBHeaderTypes.BITMAPINFOHEADER;
			}
		}
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
		public ushort BitsPerPixel { get; set; }
		/// <summary>
		/// The compression method being used.
		/// </summary>
		public CompressionTypes CompressionType { get; set; }
		/// <summary>
		/// Size of the raw bitmap data (see below), and should not be confused with the file size.
		/// </summary>
		public uint PixelArrayLength { get; set; }
		/// <summary>
		/// The horizontal resolution of the image. (pixel per meter, signed integer)
		/// </summary>
		public int HorizontalResolution { get; set; }
		/// <summary>
		/// The vertical resolution of the image. (pixel per meter, signed integer)
		/// </summary>
		public int VerticalResolution { get; set; }
		/// <summary>
		/// The number of colors in the color palette, or 0 to default to 2n.
		/// </summary>
		public uint PaletteColorCount { get; set; }
		/// <summary>
		/// The number of important colors used, or 0 when every color is important; generally ignored.
		/// </summary>
		public uint ImportantColorCount { get; set; }
		/// <summary>
		/// The red bit mask
		/// </summary>
		public uint RedBitMask { get; set; }
		/// <summary>
		/// The green bit mask
		/// </summary>
		public uint GreenBitMask { get; set; }
		/// <summary>
		/// The blue bit mask
		/// </summary>
		public uint BlueBitMask { get; set; }
		/// <summary>
		/// Alpha bit mask
		/// </summary>
		public uint AlphaBitMask { get; set; }

		/// <summary>
		/// Total number of bytes necessary to store one row of pixels
		/// </summary>
		public uint RowSize { get; set; }

		/// <summary>
		/// Constructor initalized with the bitmap image stream
		/// </summary>
		/// <param name="stream">Bitmap stream</param>
		public virtual void Initialize(byte[] dibInfo)
		{
			if(dibInfo == null)
				throw new ArgumentNullException("dibInfo");

			if(dibInfo.Length >= (int)(Size - 4))
				throw new ArgumentOutOfRangeException("dibInfo", 
					string.Format("dibInfo array cannot have a length greater than or equal to {0}", (int)(Size - 4)));

			Width = BitConverter.ToInt32(dibInfo, 0);
			Height = Math.Abs(BitConverter.ToInt32(dibInfo, 4));
			BitsPerPixel = BitConverter.ToUInt16(dibInfo, 10);
			CompressionType = (CompressionTypes)BitConverter.ToInt32(dibInfo, 12);
			PixelArrayLength = BitConverter.ToUInt32(dibInfo, 16);
			HorizontalResolution = BitConverter.ToInt32(dibInfo, 20);
			VerticalResolution = BitConverter.ToInt32(dibInfo, 24);
			PaletteColorCount = BitConverter.ToUInt32(dibInfo, 28);
			ImportantColorCount = BitConverter.ToUInt32(dibInfo, 32);

			RowSize = (uint)((BitsPerPixel * Width + 31) / 32 * 4);

			if (PixelArrayLength == 0)
				PixelArrayLength = (uint)(RowSize * Height);
		}
	}
}
