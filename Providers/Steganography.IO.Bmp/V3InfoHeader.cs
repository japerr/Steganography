using System.IO;

namespace Steganography.IO.Bmp
{
	/// <summary>
	/// Undocumented, Adobe Photoshop
	/// </summary>
	/// <remarks>
	/// Adds mandatory alpha channel bit mask.
	/// http://en.wikipedia.org/wiki/BMP_file_format#DIB_header_.28bitmap_information_header.29
	/// </remarks>
	internal class V3InfoHeader : V2InfoHeader
	{
		/// <summary>
		/// Size of this header, also indicates the header type
		/// </summary>
		public override uint Size
		{
			get
			{
				return (uint)HeaderType.BITMAPV3INFOHEADER;
			}
		}

		public override void Initialize(byte[] dibInfo)
		{
			base.Initialize(dibInfo);
		}
	}
}
