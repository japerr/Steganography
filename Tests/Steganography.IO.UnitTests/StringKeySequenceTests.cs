using NUnit.Framework;
using Steganography.IO;

namespace Steganography.IO.UnitTests
{
	[TestFixture]
	public class StringKeySequenceTests : BaseKeySequenceTest
	{
		[Test]
		public void KeySequence_Set_Correctly()
		{
			StringKeySequence keySequence = new StringKeySequence("password");

			Assert.AreEqual(8, keySequence.Length);
			Assert.AreEqual(883, keySequence.TotalStepSum);

			GetSequenceTest(keySequence, 0, 0, new byte[0]);
			GetSequenceTest(keySequence, 0, 1, new byte[] { 0x70 });
			GetSequenceTest(keySequence, 0, 5, new byte[] { 0x70, 0x61, 0x73, 0x73, 0x77 });
		}
	}
}
