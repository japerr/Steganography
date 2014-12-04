using NUnit.Framework;

namespace Steganography.IO.Basic.UnitTests
{
	[TestFixture]
	public class StreamSegmentTests
	{
		private StreamSegment _streamSegment;

		[SetUp]
		public void Setup()
		{
			_streamSegment = new StreamSegment();
			_streamSegment.Start = 10;
			_streamSegment.End = 20;
		}

		[Test]
		public void IsFalse_When_Intersect_End_Before_Segment_Start()
		{
			Assert.IsFalse(_streamSegment.Intersects(0, 9));
		}

		[Test]
		public void IsTrue_When_Intersect_The_Start()
		{
			Assert.IsTrue(_streamSegment.Intersects(0, 10));
		}

		[Test]
		public void IsTrue_When_Intersect_Is_Between_Start_End()
		{
			Assert.IsTrue(_streamSegment.Intersects(11, 19));
		}

		[Test]
		public void IsTrue_When_Intersect_The_End()
		{
			Assert.IsTrue(_streamSegment.Intersects(20, 30));
		}

		[Test]
		public void IsFalse_When_Intersect_Start_Before_Segment_End()
		{
			Assert.IsFalse(_streamSegment.Intersects(21, 30));
		}

		[Test]
		public void IsTrue_When_Intersect_Includes_Segment()
		{
			Assert.IsTrue(_streamSegment.Intersects(9, 21));
		}
	}
}
