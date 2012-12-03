using NUnit.Framework;
using Steganography.IO;

namespace Steganography.IO.Tests
{
	[TestFixture]
	public class MagicNumberAttributeTests
	{
		[Test]
		public void MagicNumber_Property_Set_Correctly()
		{
			MagicNumberAttribute attribute = new MagicNumberAttribute(new byte[] { 0x04, 0x14 });

			Assert.AreEqual(new byte[] { 0x04, 0x14 }, attribute.MagicNumber);
		}
	}
}
