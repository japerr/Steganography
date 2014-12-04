using System;

namespace Steganography.IO.Bmp
{
	/// <summary>
	/// Exception thrown when an unsupported DIB header type is encountered
	/// </summary>
	/// <remarks>
	/// http://en.wikipedia.org/wiki/BMP_file_format#DIB_header_.28bitmap_information_header.29
	/// </remarks>
	public class UnsupportedDIBHeaderTypeException : Exception
	{
		/// <summary>
		/// Constructor initialized with the unsupported DIB header type
		/// </summary>
		/// <param name="dibHeaderType">Unsupported DIB header type</param>
		public UnsupportedDIBHeaderTypeException(uint dibHeaderType)
			: base(string.Format("DIBHeader value: {0} is not currently supported", dibHeaderType))
		{
		}
	}
}
