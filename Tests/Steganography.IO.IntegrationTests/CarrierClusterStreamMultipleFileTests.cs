using NUnit.Framework;
using Steganography.IO.Sequences;
using System.Collections.Generic;
using System.IO;

namespace Steganography.IO.IntegrationTests
{
	[TestFixture]
	public class CarrierClusterStreamMultipleFileTests
	{
		private static readonly byte[] WRITE_READ_VALUE = new byte[]
		{
			0xFF, 0xFF, 0xFF, 
			0xFF, 0xFF, 0xFF, 
			0xFF, 0xFF, 0xFF, 
			0xFF, 0xFF
		};

		private string[] _fileNames = new string[6];

		[SetUp]
		public void Setup()
		{
			for(int i = 0; i < 6; ++i)
			{
				_fileNames[i] = Path.GetTempFileName();

				byte[] emptyBinaryFile = new byte[5];
				// Add the binary carrier stream magic number to the beginning of the file
				// UTF-8 magic number
				emptyBinaryFile[0] = 0xEF;
				emptyBinaryFile[1] = 0xBB;
				emptyBinaryFile[2] = 0xBF;

				File.WriteAllBytes(_fileNames[i], emptyBinaryFile);
			}
		}

		[Test]
		public void CarrierClusterStream_Loads_Binary_Carrier_Stream_Successfully()
		{
			ICarrierStreamFactory factory = new CarrierStreamFactory();
			factory.RegisterCarrierStreams();
	
			IList<Stream> streams = new List<Stream>();
			foreach(string fileName in _fileNames)
				streams.Add(File.Open(fileName, FileMode.Open));

			using (Stream carrierClusterStream = factory.BuildClusterStream<CarrierClusterStream>(
				new OneKeySequence(), streams))
			{
				Assert.AreEqual(12, carrierClusterStream.Length);
			}
		}

		[Test]
		public void CarrierClusterStream_Write_Read_Successfully()
		{
			ICarrierStreamFactory factory = new CarrierStreamFactory();
			factory.RegisterCarrierStreams();

			IList<Stream> streams = new List<Stream>();
			foreach (string fileName in _fileNames)
				streams.Add(File.Open(fileName, FileMode.Open));

			using (Stream carrierClusterStream = factory.BuildClusterStream<CarrierClusterStream>(
				new OneKeySequence(), streams))
			{
				carrierClusterStream.Write(WRITE_READ_VALUE, 0, WRITE_READ_VALUE.Length);
			}

			streams.Clear();
			foreach (string fileName in _fileNames)
				streams.Add(File.Open(fileName, FileMode.Open));

			using (Stream carrierClusterStream = factory.BuildClusterStream<CarrierClusterStream>(
				new OneKeySequence(), streams))
			{
				byte[] readResult = new byte[WRITE_READ_VALUE.Length];

				carrierClusterStream.Seek(0, SeekOrigin.Begin);
				int readLength = carrierClusterStream.Read(readResult, 0, readResult.Length);

				Assert.AreEqual(WRITE_READ_VALUE.Length, readLength);
				Assert.AreEqual(WRITE_READ_VALUE, readResult);
			}
		}

		[TearDown]
		public void TearDown()
		{
			foreach (string fileName in _fileNames)
			{
				if (File.Exists(fileName))
					File.Delete(fileName);
			}
		}
	}
}
