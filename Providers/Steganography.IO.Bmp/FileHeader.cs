using System;

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
		/// Size of the BMP file in bytes
		/// </summary>
		public int Size { get; set; }
		/// <summary>
		/// The offset, i.e. starting address, of the byte where the bitmap image data (pixel array) can be found.
		/// </summary>
		public int PixelArrayOffset { get; set; }

		/// <summary>
		/// Constructor initialized with the bitmap header info
		/// </summary>
		/// <param name="headerInfo">Bitmap header info</param>
		public FileHeader(byte[] headerInfo)
		{
			if(headerInfo == null)
				throw new ArgumentNullException("headerInfo");

			if(headerInfo.Length >= 18)
				throw new ArgumentOutOfRangeException("headerInfo",
					string.Format("headerInfo array cannot have a length greater than or equal to {0}", 18));

			Size = BitConverter.ToInt32(headerInfo, 2);
			PixelArrayOffset = BitConverter.ToInt32(headerInfo, 10);
		}
	}
}
