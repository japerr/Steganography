using NUnit.Framework;
using Steganography.IO;

namespace Steganography.IO.Tests
{
	[TestFixture]
	public class EulersNumberKeySequenceTests : BaseKeySequenceTest
	{
		[Test]
		public void KeySequence_Set_Correctly()
		{
			EulersNumberKeySequence keySequence = new EulersNumberKeySequence();

			Assert.AreEqual(72, keySequence.Length);
			Assert.AreEqual(366, keySequence.TotalStepSum);

			GetSequenceTest(keySequence, 0, 0, new byte[0]);
			GetSequenceTest(keySequence, 0, 1, new byte[] { 0x02 });
			GetSequenceTest(keySequence, 0, 5, new byte[] { 0x02, 0x07, 0x01, 0x08, 0x02 });
		}
	}
}
