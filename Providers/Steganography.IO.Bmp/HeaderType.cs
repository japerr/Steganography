namespace Steganography.IO.Bmp
{
	/// <summary>
	/// DIB Header Types
	/// </summary>
	/// <remarks>
	/// http://en.wikipedia.org/wiki/BMP_file_format#DIB_header_.28bitmap_information_header.29
	/// </remarks>
	internal enum HeaderType : ushort
	{
		/// <summary>
		/// Windows 2.0 or later
		/// OS/2 1.x[3]
		/// </summary>
		BITMAPCOREHEADER = 12,
		/// <summary>
		/// OS/2 OS22XBITMAPHEADER
		/// </summary>
		/// <remarks>
		/// Adds halftoning. Adds RLE and Huffman 1D compression.
		/// </remarks>
		BITMAPCOREHEADER2 = 64,
		/// <summary>
		/// all Windows versions since Windows 3.0
		/// </summary>
		/// <remarks>
		/// Removes RLE-24 and Huffman 1D compression. Adds 16bpp and 32bpp pixel formats. 
		/// Adds optional RGB bit masks.
		/// </remarks>
		BITMAPINFOHEADER = 40,
		/// <summary>
		/// Undocumented
		/// </summary>
		/// <remarks>
		/// Removes optional RGB bit masks. Adds mandatory RGB bit masks.
		/// </remarks>
		BITMAPV2INFOHEADER = 52,
		/// <summary>
		/// Undocumented, Adobe Photoshop
		/// </summary>
		/// <remarks>
		/// Adds mandatory alpha channel bit mask.
		/// </remarks>
		BITMAPV3INFOHEADER = 56,
		/// <summary>
		/// all Windows versions since Windows 95/NT4
		/// </summary>
		/// <remarks>
		/// Adds color space type and gamma correction
		/// </remarks>
		BITMAPV4HEADER = 108,
		/// <summary>
		/// Windows 98/2000 and newer
		/// </summary>
		/// <remarks>
		/// Adds ICC color profiles
		/// </remarks>
		BITMAPV5HEADER = 124
	}
}
