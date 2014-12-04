using System.IO;

namespace Steganography.IO.Bmp
{
	/// <summary>
	/// Windows 98/2000 and newer
	/// </summary>
	/// <remarks>
	/// Adds ICC color profiles
	/// http://en.wikipedia.org/wiki/BMP_file_format#DIB_header_.28bitmap_information_header.29
	/// </remarks>
	internal class V5InfoHeader : V4InfoHeader
	{
		/// <summary>
		/// Size of this header, also indicates the header type
		/// </summary>
		public override uint Size
		{
			get
			{
				return (uint)DIBHeaderTypes.BITMAPV5HEADER;
			}
		}

		public override void Initialize(byte[] dibInfo)
		{
			base.Initialize(dibInfo);
		}
	}
}
