using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using Steganography.IO;
using Steganography.IO.Implementation;

namespace Steganography.IO.IntegrationTests
{
	[TestFixture]
	public class CarrierClusterStreamTests
	{
		private string _fileName = Path.GetTempFileName();

		[SetUp]
		public void Setup()
		{
			byte[] emptyBinaryFile = new byte[1003];
			emptyBinaryFile[0] = 0xEF;
			emptyBinaryFile[1] = 0xBB;
			emptyBinaryFile[2] = 0xBF;

			File.WriteAllBytes(_fileName, emptyBinaryFile);
		}

		[Test]
		public void CarrierClusterStream_Loads_Binary_Carrier_Stream_Successfully()
		{
			ICarrierStreamFactory factory = new CarrierStreamFactory();
			factory.RegisterCarrierStreams();
	
			using (Stream carrierClusterStream = factory.BuildClusterStream<CarrierClusterStream>(
				new OneKeySequence(), new List<Stream> { File.Open(_fileName, FileMode.Open) }))
			{
				Assert.AreEqual(1000, carrierClusterStream.Length);
			}
		}

		[Test]
		public void CarrierClusterStream_Write_Read_Successfully()
		{
			ICarrierStreamFactory factory = new CarrierStreamFactory();
			factory.RegisterCarrierStreams();

			using (Stream carrierClusterStream = factory.BuildClusterStream<CarrierClusterStream>(
				new OneKeySequence(), new List<Stream> { File.Open(_fileName, FileMode.Open) }))
			{
				byte[] writeHelloWorld = Encoding.ASCII.GetBytes("Hello World");

				carrierClusterStream.Write(writeHelloWorld, 0, writeHelloWorld.Length);

				byte[] readHelloWorld = new byte[writeHelloWorld.Length];

				carrierClusterStream.Seek(0, SeekOrigin.Begin);
				int readLength = carrierClusterStream.Read(readHelloWorld, 0, readHelloWorld.Length);

				Assert.AreEqual(writeHelloWorld.Length, readLength); 
				Assert.AreEqual(writeHelloWorld, readHelloWorld);
			}
		}

		[TearDown]
		public void TearDown()
		{
			if(File.Exists(_fileName))
				File.Delete(_fileName);
		}
	}
}
