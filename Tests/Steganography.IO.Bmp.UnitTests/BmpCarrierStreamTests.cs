using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;

namespace Steganography.IO.Bmp.UnitTests
{
	[TestFixture]
	public class BmpCarrierStreamTests
	{
		[Test]
		public void Initialize_Throws_Exception_When_Stream_Is_Null()
		{
			Assert.Throws<ArgumentNullException>(() => new BmpCarrierStream().Initialize(null));
		}

		[Test]
		public void Successfully_Parse_pal1()
		{
			using (Stream resourceStream = Assembly.GetExecutingAssembly()
				.GetManifestResourceStream("Steganography.IO.Bmp.UnitTests.Resources.Good.pal1.bmp"))
			using (BmpCarrierStream bmpCarrierStream = new BmpCarrierStream())
			{
				Assert.Throws<InvalidOperationException>(() => bmpCarrierStream.Initialize(resourceStream));
			}
		}

		[Test]
		public void Successfully_Parse_pal1bg()
		{
			using (Stream resourceStream = Assembly.GetExecutingAssembly()
				.GetManifestResourceStream("Steganography.IO.Bmp.UnitTests.Resources.Good.pal1bg.bmp"))
			using (BmpCarrierStream bmpCarrierStream = new BmpCarrierStream())
			{
				Assert.Throws<InvalidOperationException>(() => bmpCarrierStream.Initialize(resourceStream));
			}
		}

		[Test]
		public void Successfully_Parse_pal1wb()
		{
			using (Stream resourceStream = Assembly.GetExecutingAssembly()
				.GetManifestResourceStream("Steganography.IO.Bmp.UnitTests.Resources.Good.pal1wb.bmp"))
			using (BmpCarrierStream bmpCarrierStream = new BmpCarrierStream())
			{
				Assert.Throws<InvalidOperationException>(() => bmpCarrierStream.Initialize(resourceStream));
			}
		}

		[Test]
		public void Successfully_Parse_pal4()
		{
			using (Stream resourceStream = Assembly.GetExecutingAssembly()
				.GetManifestResourceStream("Steganography.IO.Bmp.UnitTests.Resources.Good.pal4.bmp"))
			using (BmpCarrierStream bmpCarrierStream = new BmpCarrierStream())
			{
				Assert.Throws<InvalidOperationException>(() => bmpCarrierStream.Initialize(resourceStream));
			}
		}

		[Test]
		public void Successfully_Parse_pal4rle()
		{
			using (Stream resourceStream = Assembly.GetExecutingAssembly()
				.GetManifestResourceStream("Steganography.IO.Bmp.UnitTests.Resources.Good.pal4rle.bmp"))
			using (BmpCarrierStream bmpCarrierStream = new BmpCarrierStream())
			{
				Assert.Throws<InvalidOperationException>(() => bmpCarrierStream.Initialize(resourceStream));
			}
		}

		[Test]
		public void Successfully_Parse_pal8_0()
		{
			using (Stream resourceStream = Assembly.GetExecutingAssembly()
				.GetManifestResourceStream("Steganography.IO.Bmp.UnitTests.Resources.Good.pal8-0.bmp"))
			using (BmpCarrierStream bmpCarrierStream = new BmpCarrierStream())
			{
				Assert.Throws<InvalidOperationException>(() => bmpCarrierStream.Initialize(resourceStream));
			}
		}

		[Test]
		public void Successfully_Parse_pal8()
		{
			using (Stream resourceStream = Assembly.GetExecutingAssembly()
				.GetManifestResourceStream("Steganography.IO.Bmp.UnitTests.Resources.Good.pal8.bmp"))
			using (BmpCarrierStream bmpCarrierStream = new BmpCarrierStream())
			{
				Assert.Throws<InvalidOperationException>(() => bmpCarrierStream.Initialize(resourceStream));
			}
		}

		[Test]
		public void Successfully_Parse_pal8nonsquare()
		{
			using (Stream resourceStream = Assembly.GetExecutingAssembly()
				.GetManifestResourceStream("Steganography.IO.Bmp.UnitTests.Resources.Good.pal8nonsquare.bmp"))
			using (BmpCarrierStream bmpCarrierStream = new BmpCarrierStream())
			{
				Assert.Throws<InvalidOperationException>(() => bmpCarrierStream.Initialize(resourceStream));
			}
		}

		[Test]
		public void Successfully_Parse_pal8rle()
		{
			using (Stream resourceStream = Assembly.GetExecutingAssembly()
				.GetManifestResourceStream("Steganography.IO.Bmp.UnitTests.Resources.Good.pal8rle.bmp"))
			using (BmpCarrierStream bmpCarrierStream = new BmpCarrierStream())
			{
				Assert.Throws<InvalidOperationException>(() => bmpCarrierStream.Initialize(resourceStream));
			}
		}

		[Test]
		public void Successfully_Parse_pal8topdown()
		{
			using (Stream resourceStream = Assembly.GetExecutingAssembly()
				.GetManifestResourceStream("Steganography.IO.Bmp.UnitTests.Resources.Good.pal8topdown.bmp"))
			using (BmpCarrierStream bmpCarrierStream = new BmpCarrierStream())
			{
				Assert.Throws<InvalidOperationException>(() => bmpCarrierStream.Initialize(resourceStream));
			}
		}

		[Test]
		public void Successfully_Parse_pal8v2()
		{
			using (Stream resourceStream = Assembly.GetExecutingAssembly()
				.GetManifestResourceStream("Steganography.IO.Bmp.UnitTests.Resources.Good.pal8v2.bmp"))
			using (BmpCarrierStream bmpCarrierStream = new BmpCarrierStream())
			{
				Assert.Throws<InvalidOperationException>(() => bmpCarrierStream.Initialize(resourceStream));
			}
		}

		[Test]
		public void Successfully_Parse_pal8v4()
		{
			using (Stream resourceStream = Assembly.GetExecutingAssembly()
				.GetManifestResourceStream("Steganography.IO.Bmp.UnitTests.Resources.Good.pal8v4.bmp"))
			using (BmpCarrierStream bmpCarrierStream = new BmpCarrierStream())
			{
				Assert.Throws<InvalidOperationException>(() => bmpCarrierStream.Initialize(resourceStream));
			}
		}

		[Test]
		public void Successfully_Parse_pal8v5()
		{
			using (Stream resourceStream = Assembly.GetExecutingAssembly()
				.GetManifestResourceStream("Steganography.IO.Bmp.UnitTests.Resources.Good.pal8v5.bmp"))
			using (BmpCarrierStream bmpCarrierStream = new BmpCarrierStream())
			{
				Assert.Throws<InvalidOperationException>(() => bmpCarrierStream.Initialize(resourceStream));
			}
		}

		[Test]
		public void Successfully_Parse_pal8w124()
		{
			using (Stream resourceStream = Assembly.GetExecutingAssembly()
				.GetManifestResourceStream("Steganography.IO.Bmp.UnitTests.Resources.Good.pal8w124.bmp"))
			using (BmpCarrierStream bmpCarrierStream = new BmpCarrierStream())
			{
				Assert.Throws<InvalidOperationException>(() => bmpCarrierStream.Initialize(resourceStream));
			}
		}

		[Test]
		public void Successfully_Parse_pal8w125()
		{
			using (Stream resourceStream = Assembly.GetExecutingAssembly()
				.GetManifestResourceStream("Steganography.IO.Bmp.UnitTests.Resources.Good.pal8w125.bmp"))
			using (BmpCarrierStream bmpCarrierStream = new BmpCarrierStream())
			{
				Assert.Throws<InvalidOperationException>(() => bmpCarrierStream.Initialize(resourceStream));
			}
		}

		[Test]
		public void Successfully_Parse_pal8w126()
		{
			using (Stream resourceStream = Assembly.GetExecutingAssembly()
				.GetManifestResourceStream("Steganography.IO.Bmp.UnitTests.Resources.Good.pal8w126.bmp"))
			using (BmpCarrierStream bmpCarrierStream = new BmpCarrierStream())
			{
				Assert.Throws<InvalidOperationException>(() => bmpCarrierStream.Initialize(resourceStream));
			}
		}

		[Test]
		public void Successfully_Parse_rgb16_565()
		{
			using (Stream resourceStream = Assembly.GetExecutingAssembly()
				.GetManifestResourceStream("Steganography.IO.Bmp.UnitTests.Resources.Good.rgb16-565.bmp"))
			using (BmpCarrierStream bmpCarrierStream = new BmpCarrierStream())
			{
				bmpCarrierStream.Initialize(resourceStream);
				Assert.AreEqual(1, bmpCarrierStream.Length);
			}
		}

		[Test]
		public void Successfully_Parse_rgb16_565pal()
		{
			using (Stream resourceStream = Assembly.GetExecutingAssembly()
				.GetManifestResourceStream("Steganography.IO.Bmp.UnitTests.Resources.Good.rgb16-565pal.bmp"))
			using (BmpCarrierStream bmpCarrierStream = new BmpCarrierStream())
			{
				bmpCarrierStream.Initialize(resourceStream);
				Assert.AreEqual(1, bmpCarrierStream.Length);
			}
		}

		[Test]
		public void Successfully_Parse_rgb16()
		{
			using (Stream resourceStream = Assembly.GetExecutingAssembly()
				.GetManifestResourceStream("Steganography.IO.Bmp.UnitTests.Resources.Good.rgb16.bmp"))
			using (BmpCarrierStream bmpCarrierStream = new BmpCarrierStream())
			{
				bmpCarrierStream.Initialize(resourceStream);
				Assert.AreEqual(1, bmpCarrierStream.Length);
			}
		}

		[Test]
		public void Successfully_Parse_rgb24()
		{
			using (Stream resourceStream = Assembly.GetExecutingAssembly()
				.GetManifestResourceStream("Steganography.IO.Bmp.UnitTests.Resources.Good.rgb24.bmp"))
			using (BmpCarrierStream bmpCarrierStream = new BmpCarrierStream())
			{
				bmpCarrierStream.Initialize(resourceStream);
				Assert.AreEqual(1, bmpCarrierStream.Length);
			}
		}

		[Test]
		public void Successfully_Parse_rgb24pal()
		{
			using (Stream resourceStream = Assembly.GetExecutingAssembly()
				.GetManifestResourceStream("Steganography.IO.Bmp.UnitTests.Resources.Good.rgb24pal.bmp"))
			using (BmpCarrierStream bmpCarrierStream = new BmpCarrierStream())
			{
				bmpCarrierStream.Initialize(resourceStream);
				Assert.AreEqual(1, bmpCarrierStream.Length);
			}
		}

		[Test]
		public void Successfully_Parse_rgb32()
		{
			using (Stream resourceStream = Assembly.GetExecutingAssembly()
				.GetManifestResourceStream("Steganography.IO.Bmp.UnitTests.Resources.Good.rgb32.bmp"))
			using (BmpCarrierStream bmpCarrierStream = new BmpCarrierStream())
			{
				bmpCarrierStream.Initialize(resourceStream);
				Assert.AreEqual(1, bmpCarrierStream.Length);
			}
		}

		[Test]
		public void Successfully_Parse_rgb32bf()
		{
			using (Stream resourceStream = Assembly.GetExecutingAssembly()
				.GetManifestResourceStream("Steganography.IO.Bmp.UnitTests.Resources.Good.rgb32bf.bmp"))
			using (BmpCarrierStream bmpCarrierStream = new BmpCarrierStream())
			{
				bmpCarrierStream.Initialize(resourceStream);
				Assert.AreEqual(1, bmpCarrierStream.Length);
			}
		}
	}
}
