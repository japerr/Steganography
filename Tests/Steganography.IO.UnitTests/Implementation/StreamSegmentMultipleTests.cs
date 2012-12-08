using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Steganography.IO;
using Steganography.IO.Implementation;

namespace Steganography.IO.UnitTests.Implementation
{
	[TestFixture]
	public class StreamSegmentMultipleTests
	{
		private StreamSegment[] _streamSegment = new StreamSegment[6];

		[SetUp]
		public void Setup()
		{
			for (int i = 0; i < 6; ++i)
			{
				_streamSegment[i] = new StreamSegment
				{
					Start = i * 2,
					End = i * 2 + 1
				};
			}
		}

		[Test]
		public void IsFalse_When_Intersect_End_Before_Segment_Start()
		{
			IEnumerable<StreamSegment> results = _streamSegment.Where(s => s.Intersects(0, 11));

			Assert.AreEqual(6, results.Count());
		}
	}
}
