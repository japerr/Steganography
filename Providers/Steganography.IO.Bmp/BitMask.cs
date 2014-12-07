using System;
using System.IO;

namespace Steganography.IO.Bmp
{
	internal class BitMask
	{
		/// <summary>
		/// Type of defined bitmask
		/// </summary>
		public BitMaskType Type { get; set; }
		/// <summary>
		/// DWORD for the alpha bit mask
		/// </summary>
		public uint AlphaMask { get; set; }
		/// <summary>
		/// DWORD for the red bit mask
		/// </summary>
		public uint RedMask { get; set; }
		/// <summary>
		/// DWORD for the green bit mask
		/// </summary>
		public uint GreenMask { get; set; }
		/// <summary>
		/// DWORD for the blue bit mask
		/// </summary>
		public uint BlueMask { get; set; }

		public BitMask(HeaderType headerType, CompressionType compressionType, Stream stream)
		{
			if(compressionType != CompressionType.BI_BITFIELDS || compressionType != CompressionType.BI_ALPHABITFIELDS)
				throw new InvalidOperationException("BitMask can only be used with compression "
					+ "types of BI_BITFIELDS or BI_ALPHABITFIELDS");

			if(headerType < HeaderType.BITMAPINFOHEADER)
				throw new InvalidOperationException("BitMask can only be used with HeaderTypes "
					+ "greater than or equal to BITMAPINFOHEADER");

			stream.Seek((int)headerType + FileHeader.FILE_HEADER_SIZE, SeekOrigin.Begin);

			if (headerType == HeaderType.BITMAPINFOHEADER)
				SetRGB(stream);
			else //(headerType > HeaderType.BITMAPINFOHEADER)
				SetRGBA(stream);
		}

		private void SetRGB(Stream stream)
		{
			Type = BitMaskType.RGB;

			byte[] rgbMasks = new byte[12];
			if (stream.Read(rgbMasks, 0, rgbMasks.Length) < rgbMasks.Length)
				throw new InvalidOperationException("Unable to read bitmap RGB bitmasks, less than 12bytes read.");

			RedMask = BitConverter.ToUInt32(rgbMasks, 0);
			GreenMask = BitConverter.ToUInt32(rgbMasks, 4);
			BlueMask = BitConverter.ToUInt32(rgbMasks, 8);
		}

		private void SetRGBA(Stream stream)
		{
			Type = BitMaskType.RGBA;

			byte[] rgbaMasks = new byte[16];
			if (stream.Read(rgbaMasks, 0, rgbaMasks.Length) < rgbaMasks.Length)
				throw new InvalidOperationException("Unable to read bitmap RGBA bitmasks, less than 16bytes read.");

			AlphaMask = BitConverter.ToUInt32(rgbaMasks, 0);
			RedMask = BitConverter.ToUInt32(rgbaMasks, 4);
			GreenMask = BitConverter.ToUInt32(rgbaMasks, 8);
			BlueMask = BitConverter.ToUInt32(rgbaMasks, 12);
		}
	}
}
