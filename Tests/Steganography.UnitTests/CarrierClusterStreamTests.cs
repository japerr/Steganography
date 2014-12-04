using NUnit.Framework;
using Steganography.IO;
using System.IO;

namespace Steganography.UnitTests
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
