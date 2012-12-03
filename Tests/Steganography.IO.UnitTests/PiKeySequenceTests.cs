using NUnit.Framework;
using Steganography.IO;

namespace Steganography.IO.Tests
{
	[TestFixture]
	public class PiKeySequenceTests : BaseKeySequenceTest
	{
		[Test]
		public void KeySequence_Set_Correctly()
		{
			PiKeySequence keySequence = new PiKeySequence();

			Assert.AreEqual(72, keySequence.Length);
			Assert.AreEqual(344, keySequence.TotalStepSum);

			GetSequenceTest(keySequence, 0, 0, new byte[0]);
			GetSequenceTest(keySequence, 0, 1, new byte[] { 0x03 });
			GetSequenceTest(keySequence, 0, 5, new byte[] { 0x03, 0x01, 0x04, 0x01, 0x05 });
		}
	}
}
