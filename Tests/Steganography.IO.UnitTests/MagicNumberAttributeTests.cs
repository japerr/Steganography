using NUnit.Framework;
using Steganography.IO;

namespace Steganography.IO.UnitTests
{
	[TestFixture]
	public class MagicNumberAttributeTests
	{
		[Test]
		public void MagicNumber_Property_Set_Correctly()
		{
			CarrierStreamAttribute attribute = new CarrierStreamAttribute(new byte[] { 0x04, 0x14 });

			Assert.AreEqual(new byte[] { 0x04, 0x14 }, attribute.MagicNumber);
		}
	}
}
