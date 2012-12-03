using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using Steganography.IO.Bmp;

namespace Steganography.IO.Tests.Bmp
{
	[TestFixture]
	public class StructureTests
	{
		[Test]
		public void Constructor_Throws_Exception_When_Stream_Is_Null()
		{
			Assert.Throws<ArgumentNullException>(() => new Structure(null));
		}

		[Test]
		public void Successfully_Parse_2x2_24bit_BITMAPINFOHEADER()
		{
			using (Stream resourceStream = 	Assembly.GetExecutingAssembly()
				.GetManifestResourceStream("Steganography.IO.Tests.Bmp.Resources.2x2-24bit-BITMAPINFOHEADER.bmp"))
			{
				resourceStream.Seek(2, SeekOrigin.Begin);
				Structure structure = new Structure(resourceStream);

				Assert.AreEqual(70, structure.ByteLength);
				Assert.AreEqual(54, structure.ImageDataOffset);
				Assert.AreEqual(DIBHeaderTypes.BITMAPINFOHEADER, structure.DIBHeaderType);
				Assert.AreEqual(2, structure.Width);
				Assert.AreEqual(2, structure.Height);
				Assert.AreEqual(24, structure.BitsPerPixel);
				Assert.AreEqual(16, structure.ImageDataLength);
				Assert.AreEqual(8, structure.RowSize);
			}
		}

		[Test]
		public void Successfully_Parse_4x2_32bit_BITMAPINFOHEADER()
		{
			using (Stream resourceStream = Assembly.GetExecutingAssembly()
				.GetManifestResourceStream("Steganography.IO.Tests.Bmp.Resources.4x2-32bit-BITMAPINFOHEADER.bmp"))
			{
				resourceStream.Seek(2, SeekOrigin.Begin);
				Structure structure = new Structure(resourceStream);

				Assert.AreEqual(72, structure.ByteLength);
				Assert.AreEqual(54, structure.ImageDataOffset);
				Assert.AreEqual(DIBHeaderTypes.BITMAPV3INFOHEADER, structure.DIBHeaderType);
				Assert.AreEqual(4, structure.Width);
				Assert.AreEqual(2, structure.Height);
				Assert.AreEqual(16, structure.BitsPerPixel);
				Assert.AreEqual(18, structure.ImageDataLength);
				Assert.AreEqual(8, structure.RowSize);
			}
		}

		[Test]
		public void Successfully_Parse_4x2_32bit_BITMAPV3INFOHEADER()
		{
			using (Stream resourceStream = Assembly.GetExecutingAssembly()
				.GetManifestResourceStream("Steganography.IO.Tests.Bmp.Resources.4x2-32bit-BITMAPV3INFOHEADER.bmp"))
			{
				resourceStream.Seek(2, SeekOrigin.Begin);
				Structure structure = new Structure(resourceStream);

				Assert.AreEqual(70, structure.ByteLength);
				Assert.AreEqual(54, structure.ImageDataOffset);
				Assert.AreEqual(DIBHeaderTypes.BITMAPV3INFOHEADER, structure.DIBHeaderType);
				Assert.AreEqual(2, structure.Width);
				Assert.AreEqual(2, structure.Height);
				Assert.AreEqual(24, structure.BitsPerPixel);
				Assert.AreEqual(16, structure.ImageDataLength);
				Assert.AreEqual(8, structure.RowSize);
			}
		}
	}
}
