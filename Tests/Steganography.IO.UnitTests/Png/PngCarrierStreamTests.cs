using System;
using NUnit.Framework;
using Steganography.IO.Png;

namespace Steganography.IO.UnitTests.Png
{
	[TestFixture]
	public class PngCarrierStreamTests
	{
		[Test]
		public void Constructor_Throws_Exception_When_Stream_Is_Null()
		{
			Assert.Throws<ArgumentNullException>(() => new PngCarrierStream(null));
		}
	}
}
