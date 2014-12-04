using System;
using NUnit.Framework;

namespace Steganography.IO.Png.UnitTests
{
	[TestFixture]
	public class PngCarrierStreamTests
	{
		[Test]
		public void Constructor_Throws_Exception_When_Stream_Is_Null()
		{
			Assert.Throws<ArgumentNullException>(() => new PngCarrierStream().Initialize(null));
		}
	}
}
