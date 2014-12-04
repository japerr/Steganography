using NUnit.Framework;

namespace Steganography.IO.Sequences.UnitTests
{
	[TestFixture]
	public class OneKeySequenceTests : BaseKeySequenceTest
	{
		[Test]
		public void KeySequence_Set_Correctly()
		{
			OneKeySequence keySequence = new OneKeySequence();

			Assert.AreEqual(1, keySequence.Length);
			Assert.AreEqual(1, keySequence.TotalStepSum);

			GetSequenceTest(keySequence, 0, 0, new byte[0]);
			GetSequenceTest(keySequence, 0, 1, new byte[] { 0x01 });
			GetSequenceTest(keySequence, 0, 5, new byte[] { 0x01, 0x01, 0x01, 0x01, 0x01 });
		}
	}
}
