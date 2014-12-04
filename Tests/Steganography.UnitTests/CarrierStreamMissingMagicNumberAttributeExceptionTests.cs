using NUnit.Framework;
using Steganography.IO;

namespace Steganography.UnitTests
{
	[TestFixture]
	public class CarrierStreamMissingMagicNumberAttributeExceptionTests
	{
		[Test]
		public void Exception_Message_Property_Set_Correctly()
		{
			NoCarrierStreamsFoundException attribute = 
				new NoCarrierStreamsFoundException("Path");

			Assert.IsTrue(attribute.Message.StartsWith("CarrierStream subclass path"));
		}
	}
}
