using NUnit.Framework;
using Steganography.IO;

namespace Steganography.IO.UnitTests
{
	[TestFixture]
	public class CarrierStreamMissingMagicNumberAttributeExceptionTests
	{
		[Test]
		public void Exception_Message_Property_Set_Correctly()
		{
			CarrierStreamMissingMagicNumberAttributeException attribute = 
				new CarrierStreamMissingMagicNumberAttributeException(string.Empty.GetType());

			Assert.IsTrue(attribute.Message.StartsWith("CarrierStream subclass String"));
		}
	}
}
