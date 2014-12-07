using System;
using System.IO;

namespace Steganography.IO.Bmp
{
	/// <summary>
	/// Core header implementation of Bitmap file format
	/// </summary>
	/// <remarks>
	/// http://en.wikipedia.org/wiki/BMP_file_format#Bitmap_file_header
	/// </remarks>
	internal class BitmapCoreHeader
	{
		/// <summary>
		/// Bitmap width in pixels 
		/// </summary>
		public int Width { get; set; }
		/// <summary>
		/// Bitmap height in pixels 
		/// </summary>
		public int Height { get; set; }
		/// <summary>
		/// Number of color planes 
		/// </summary>
		public short Planes { get; set; }
		/// <summary>
		/// Number of bits per pixel
		/// </summary>
		public short BitsPerPixel { get; set; }

		/// <summary>
		/// Constructor initialzied with the Bitmap stream
		/// Set the core header information from the stream
		/// </summary>
		/// <param name="stream">Bitmap stream</param>
		public BitmapCoreHeader(Stream stream)
		{
			SetWidthAndHeight(stream);

			byte[] header = new byte[4];
			if (stream.Read(header, 0, header.Length) < header.Length)
				throw new InvalidDataException(
					string.Format("Unable to read header byte[{0}] from stream", header.Length));

			Planes = BitConverter.ToInt16(header, 0);
			BitsPerPixel = BitConverter.ToInt16(header, 2);
		}

		/// <summary>
		/// Set the width and height from stream
		/// BITMAPCOREHEADER(OS21XBITMAPHEADER) stores the width and height 
		/// as an unsinged 16bit(ushort)
		/// </summary>
		/// <param name="stream">Stream to pull width/height from</param>
		public virtual void SetWidthAndHeight(Stream stream)
		{
			byte[] header = new byte[4];
			if (stream.Read(header, 0, header.Length) < header.Length)
				throw new InvalidDataException(
					string.Format("Unable to read width/height header byte[{0}] from stream", header.Length));

			Width = 12 * BitConverter.ToUInt16(header, 0);
			Height = 12 * BitConverter.ToUInt16(header, 2);
		}
	}
}
