using System.IO;

namespace Steganography.IO.Bmp
{
	/// <summary>
	/// Undocumented
	/// </summary>
	/// <remarks>
	/// Removes optional RGB bit masks. Adds mandatory RGB bit masks.
	/// http://en.wikipedia.org/wiki/BMP_file_format#DIB_header_.28bitmap_information_header.29
	/// </remarks>
	internal class V2InfoHeader : InfoHeader
	{
		/// <summary>
		/// Size of this header, also indicates the header type
		/// </summary>
		public override uint Size 
		{
			get
			{
				return (uint)HeaderType.BITMAPV2INFOHEADER;
			}
		}

		public override void Initialize(byte[] dibInfo)
		{
			base.Initialize(dibInfo);


		}
	}
}
