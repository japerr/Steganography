using System;
using System.IO;

namespace Steganography.IO.Bmp
{
	/// <summary>
	/// Info header implementation of Bitmap file format
	/// </summary>
	/// <remarks>
	/// http://en.wikipedia.org/wiki/BMP_file_format#Bitmap_file_header
	/// </remarks>
    internal class BitmapInfoHeader : BitmapCoreHeader
    {
		/// <summary>
		/// Compression method being used. See the next table for a list of possible values
		/// </summary>
		public CompressionType CompressionType { get; set; }
		/// <summary>
		/// Image size. This is the size of the raw bitmap data; a dummy 
		/// 0 can be given for BI_RGB bitmaps.
		/// </summary>
		public uint BitmapSize { get; set; }
		/// <summary>
		/// Horizontal resolution of the image.
		/// </summary>
		public int HorizontalResolution { get; set; }
		/// <summary>
		/// Vertical resolution of the image.
		/// </summary>
		public int VerticalResolution { get; set; }
		/// <summary>
		/// Number of colors in the color palette, or 0 to default to 2n
		/// </summary>
		public uint NumberColors { get; set; }
		/// <summary>
		/// Number of important colors used, or 0 when every color 
		/// is important; generally ignored
		/// </summary>
		public uint NumberImportantColors { get; set; }

		/// <summary>
		/// Constructor initialzied with the Bitmap stream
		/// Set the windows info header values from the stream
		/// </summary>
		/// <param name="stream">Bitmap stream</param>
		public BitmapInfoHeader(Stream stream)
			: base(stream)
		{
			byte[] header = new byte[24];
			if (stream.Read(header, 0, header.Length) < header.Length)
				throw new InvalidDataException(
					string.Format("Unable to read header byte[{0}] from stream", header.Length));

			CompressionType = (CompressionType)BitConverter.ToUInt32(header, 0);
			BitmapSize = BitConverter.ToUInt32(header, 4);
			HorizontalResolution = BitConverter.ToInt32(header, 8);
			VerticalResolution = BitConverter.ToInt32(header, 12);
			NumberColors = BitConverter.ToUInt32(header, 16);
			NumberImportantColors = BitConverter.ToUInt32(header, 20);
		}

		/// <summary>
		/// Set the width and height from stream
		/// BITMAPINFOHEADER stores the width and height 
		/// as an signed 32bit(int)
		/// </summary>
		/// <param name="stream">Stream to pull width/height from</param>
		public override void SetWidthAndHeight(Stream stream)
		{
			byte[] header = new byte[8];
			if (stream.Read(header, 0, header.Length) < header.Length)
				throw new InvalidDataException(
					string.Format("Unable to read width/height header byte[{0}] from stream", header.Length));

			Width = BitConverter.ToInt32(header, 0);
			Height = BitConverter.ToInt32(header, 4);
		}
    }
}
