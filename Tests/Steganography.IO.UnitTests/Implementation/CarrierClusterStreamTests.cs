using System.IO;
using NUnit.Framework;
using Steganography.IO;
using Steganography.IO.Implementation;

namespace Steganography.IO.UnitTests.Implementation
{
	[TestFixture]
	public class CarrierClusterStreamTests
	{
		[Test]
		public void Successful_Constructor()
		{
			CarrierClusterStream clusterStream = new CarrierClusterStream();

			Assert.IsInstanceOf<ICarrierClusterStream>(clusterStream);
			Assert.IsInstanceOf<Stream>(clusterStream);
		}
	}
}
