using System;
using NUnit.Framework;
using Steganography.IO.Bmp;

namespace Steganography.IO.UnitTests.Bmp
{
	[TestFixture]
	public class FileHeaderTests
	{
		[Test]
		public void Constructor_Throws_Exception_When_Argument_Is_Null()
		{
			Assert.Throws<ArgumentNullException>(() => new FileHeader(null));
		}

		[Test]
		public void Constructor_Thows_Exception_When_Argument_Is_To_Short()
		{
			Assert.Throws<ArgumentException>(() => new FileHeader(new byte[] { 0x01 }));
		}

		[Test]
		public void Constructor_Successful()
		{
			byte[] headerInfo = new byte[] 
			{
				0x00, 0x00,
				0xE7, 0x03, 0x00, 0x00, 
				0x00, 0x00, 0x00, 0x00, 
				0xE7, 0x03, 0x00, 0x00,
				0x00, 0x00, 0x00, 0x00
			};

			FileHeader fileHeader = new FileHeader(headerInfo);

			Assert.AreEqual(999, fileHeader.Size);
			Assert.AreEqual(999, fileHeader.PixelArrayOffset);
		}
	}
}
