using System;
using NUnit.Framework;
using Steganography.IO;

namespace Steganography.IO.UnitTests
{
	[TestFixture]
	public class GuidKeySequenceTests : BaseKeySequenceTest
	{
		[Test]
		public void KeySequence_Set_Correctly()
		{
			GuidKeySequence keySequence = new GuidKeySequence(new Guid("17425BC9-E0C3-4321-9F4B-E7B9ED05C3A0"));

			Assert.AreEqual(16, keySequence.Length);
			Assert.AreEqual(2147, keySequence.TotalStepSum);

			GetSequenceTest(keySequence, 0, 0, new byte[0]);
			GetSequenceTest(keySequence, 0, 1, new byte[] { 0xC9 });
			GetSequenceTest(keySequence, 0, 5, new byte[] { 0xC9, 0x5B, 0x42, 0x17, 0xC3 });
		}
	}
}
