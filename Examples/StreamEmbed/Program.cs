using System;
using System.IO;
using System.Reflection;
using Steganography;
using Steganography.IO;

namespace StreamEmbed
{
	class Program
	{
		private static string TEST_DIRECTORY = Path.Combine(
			Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Rgb16");

		private static string TEST_FILE = Path.Combine(TEST_DIRECTORY, "rgb16.bmp");

		private static CarrierStreamFactory FACTORY = new CarrierStreamFactory(
			Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

		static void Main(string[] args)
		{
			CopyResource();

			Random random = new Random(DateTime.UtcNow.Millisecond);
			byte[] bytes = new byte[1000];
			random.NextBytes(bytes);

			using (BaseCarrierStream carrier = FACTORY.BuildCarrierStream(
				File.Open(TEST_FILE, FileMode.Open)))
			using (Stream stream = new MemoryStream(bytes))
			{
				carrier.Write(0, bytes, 0, bytes.Length);

				byte[] read = new byte[bytes.Length];

				int readLength = carrier.Read(0, read, 0, read.Length);

				if(bytes.Length == readLength)
					Console.WriteLine("Successfully read the correct number of bytes");
				else
					Console.WriteLine("Failed read the correct number of bytes, read {0} but expexted {1}", 
						readLength, bytes.Length);

				bool mismatched = false;
				for (int i = 0; i < bytes.Length; ++i)
				{
					if (read[i] == bytes[i])
						continue; 

					Console.WriteLine("Byte mismatch at index {0}: read - {1}, write - {2}",
						i, Convert.ToInt32(read[i]), Convert.ToInt32(bytes[i]));
					mismatched = true;
				}

				if (mismatched)
					Console.WriteLine("Read bytes don't equal written bytes");
				else
					Console.WriteLine("Read bytes equal written bytes");
			}

			Console.ReadLine();
		}

		private static void CopyResource()
		{
			if (!Directory.Exists(TEST_DIRECTORY))
				Directory.CreateDirectory(TEST_DIRECTORY);

			if (!File.Exists(TEST_FILE))
			{
				using (Stream resourceStream = Assembly.GetExecutingAssembly()
					.GetManifestResourceStream("StreamEmbed.CarrierFiles.rgb16.bmp"))
				using (FileStream fileStream = File.Create(TEST_FILE))
				{
					resourceStream.CopyTo(fileStream);
				}
			}
		}
	}
}
