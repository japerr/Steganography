namespace Steganography.IO.Bmp
{
	/// <summary>
	/// Header field enum
	/// </summary>
	/// <remarks>
	/// http://en.wikipedia.org/wiki/BMP_file_format#Bitmap_file_header
	/// </remarks>
	internal enum HeaderField : ushort
	{
		/// <summary>
		/// 0x424D, Windows 3.1x, 95, NT, ... etc.
		/// </summary>
		BM = 19778,
		/// <summary>
		/// 0x4241, OS/2 struct Bitmap Array
		/// </summary>
		BA = 16961,
		/// <summary>
		/// 0x4349, OS/2 struct Color Icon
		/// </summary>
		CI = 17225, 
		/// <summary>
		/// 0x4350, OS/2 const Color Pointer
		/// </summary>
		CP = 17232,
		/// <summary>
		/// 0x4943, OS/2 struct Icon
		/// </summary>
		IC = 18755,
		/// <summary>
		/// 0x5054, OS/2 Pointer
		/// </summary>
		PT = 20564
	}
}
