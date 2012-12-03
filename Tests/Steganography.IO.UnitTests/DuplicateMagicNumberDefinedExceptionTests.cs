using NUnit.Framework;
using Steganography.IO;

namespace Steganography.IO.Tests
{
	[TestFixture]
	public class DuplicateMagicNumberDefinedExceptionTests
	{
		[Test]
		public void Exception_Message_Property_Set_Correctly()
		{
			DuplicateMagicNumberDefinedException attribute =
				new DuplicateMagicNumberDefinedException(new byte[] { 0x04, 0x14 }, string.Empty.GetType(), int.MinValue.GetType());

			Assert.AreEqual("The magic number \"0414\" key already exists for type String, but is defined again for type Int32", attribute.Message);
		}
	}
}
