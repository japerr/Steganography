using System.IO;

namespace Steganography.IO.Bmp
{
	/// <summary>
	/// all Windows versions since Windows 95/NT4
	/// </summary>
	/// <remarks>
	/// Adds color space type and gamma correction
	/// http://en.wikipedia.org/wiki/BMP_file_format#DIB_header_.28bitmap_information_header.29
	/// </remarks>
	internal class V4InfoHeader : V3InfoHeader
	{
		/// <summary>
		/// Size of this header, also indicates the header type
		/// </summary>
		public override uint Size
		{
			get
			{
				return (uint)DIBHeaderTypes.BITMAPV4HEADER;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public uint CsType { get; set; }
		public uint[] Endpoints { get; set; } // see http://msdn2.microsoft.com/en-us/library/ms536569.aspx
		/// <summary>
		/// Red gamma correction
		/// </summary>
		public uint GammaRed { get; set; }
		/// <summary>
		/// Green gamma correction
		/// </summary>
		public uint GammaGreen { get; set; }
		/// <summary>
		/// Blue gamma correction
		/// </summary>
		public uint GammaBlue { get; set; }

		public override void Initialize(byte[] dibInfo)
		{
			base.Initialize(dibInfo);
		}
	}
}
