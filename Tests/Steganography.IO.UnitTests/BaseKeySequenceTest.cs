using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Steganography.IO.UnitTests
{
	public abstract class BaseKeySequenceTest
	{
		protected void GetSequenceTest(IKeySequence keySequence, int startAt, int count, IEnumerable<byte> expected)
		{
			IEnumerable<byte> results = keySequence.GetSequence(startAt, count);

			Assert.IsNotNull(results);
			Assert.AreEqual(count, results.Count());
			Assert.AreEqual(expected, results);
		}
	}
}
