using NUnit.Framework;
using Steganography.IO;
using Steganography.IO.Sequences;
using System;
using System.Collections.Generic;
using System.IO;

namespace Steganography.UnitTests
{
	[TestFixture]
	public class CarrierStreamFactoryTests
	{
		[Test]
		public void Successful_Constructor()
		{
			CarrierStreamFactory clusterStreamFactory = new CarrierStreamFactory();

			Assert.IsInstanceOf<ICarrierStreamFactory>(clusterStreamFactory);
		}

		[Test]
		public void RegisterCarrierStreams_Successful()
		{
			CarrierStreamFactory clusterStreamFactory = new CarrierStreamFactory();

			Assert.DoesNotThrow(() => clusterStreamFactory.RegisterCarrierStreams());
		}

		[Test]
		public void BuildClusterStream_Throws_Exception_When_KeySequence_Is_Null()
		{
			CarrierStreamFactory clusterStreamFactory = new CarrierStreamFactory();

			Assert.Throws<ArgumentNullException>(() => 
				clusterStreamFactory.BuildClusterStream<CarrierClusterStream>(null, new List<Stream>()));
		}

		[Test]
		public void BuildClusterStream_Throws_Exception_When_Stream_List_Is_Null()
		{
			CarrierStreamFactory clusterStreamFactory = new CarrierStreamFactory();

			Assert.Throws<ArgumentNullException>(() => 
				clusterStreamFactory.BuildClusterStream<CarrierClusterStream>(new OneKeySequence(), null));
		}
	}
}
