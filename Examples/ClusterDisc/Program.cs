using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using DiscUtils;
using DiscUtils.Fat;
using Steganography;
using Steganography.IO;
using Steganography.IO.Sequences;

namespace ClusterDisc
{
	class Program
	{
		private static string CLUSTER_DISC_DIRECTORY = Path.Combine(
			Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ClusterFiles");

		private static string CARRIER_STREAM_PROVIDER_DIRECTORY = Path.Combine(
			Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Providers");

		private static string CLUSTER_FILENAME = "File";
		
		static void Main(string[] args)
		{
			ICarrierStreamFactory factory = new CarrierStreamFactory(CARRIER_STREAM_PROVIDER_DIRECTORY);
			using (Stream carrierClusterStream = factory.BuildClusterStream("Basic",
				new OneKeySequence(), GetClusterFiles()))
			{
				DiscUtils.FileSystemInfo[] fsInfos = FileSystemManager.DetectDefaultFileSystems(carrierClusterStream);
				if (fsInfos != null && fsInfos.Count() > 0)
				{
					using (FatFileSystem fs = (FatFileSystem)fsInfos[0].Open(carrierClusterStream))
					{
						foreach (DiscDirectoryInfo info in fs.Root.GetDirectories())
							Console.WriteLine(info.FullName);

						DisplayDiskSize(fs);
					}
				}
				else
				{
					carrierClusterStream.Position = 0;

					Geometry geometry = Geometry.FromCapacity(carrierClusterStream.Length - 512);
					using (FatFileSystem fs = FatFileSystem.FormatPartition(carrierClusterStream,
						string.Empty, geometry, 0, geometry.TotalSectors, 13))
					{
						DateTime start = DateTime.Now;
						for (int i = 0; i < 511; ++i)
							fs.CreateDirectory(@"D" + i);

						Console.WriteLine("511 Directories created in {0}", DateTime.Now - start);
						foreach (DiscDirectoryInfo info in fs.Root.GetDirectories())
							Console.WriteLine(info.FullName);

						DisplayDiskSize(fs);
					}
				}
			}

			Console.ReadLine();
		}

		private static IList<Stream> GetClusterFiles()
		{
			if (!Directory.Exists(CLUSTER_DISC_DIRECTORY))
				Directory.CreateDirectory(CLUSTER_DISC_DIRECTORY);

			IList<Stream> streams = new List<Stream>();

			string rgb16 = Path.Combine(CLUSTER_DISC_DIRECTORY, "rgb16-{0}.bmp");
			for (int i = 0; i < 4500; ++i)
			{
				string file = string.Format(rgb16, i);
				if (!File.Exists(file))
				{
					using (Stream resourceStream = Assembly.GetExecutingAssembly()
						.GetManifestResourceStream("ClusterDisc.CarrierFiles.rgb16.bmp"))
					using (FileStream fileStream = File.Create(file))
					{
						resourceStream.CopyTo(fileStream);
					}
				}

				streams.Add(File.Open(file, FileMode.Open));
			}

			//for (int i = 0; i < 3; ++i)
			//{
			//	string fileName = Path.Combine(CLUSTER_DISC_DIRECTORY, CLUSTER_FILENAME + i + ".bin");
			//	if (!File.Exists(fileName))
			//	{
			//		byte[] fileData = new byte[1439747];
			//		//byte[] fileData = new byte[11184811];
			//		fileData[0] = 0xEF;
			//		fileData[1] = 0xBB;
			//		fileData[2] = 0xBF;

			//		File.WriteAllBytes(fileName, fileData);
			//	}

			//	streams.Add(File.Open(fileName, FileMode.Open));
			//}

			return streams;
		}

		private static void DisplayDiskSize(FatFileSystem fileSystem)
		{
			var asdf = fileSystem.GetDirectoryInfo(fileSystem.Root.FullName);

			Console.WriteLine("Heads: {0}", fileSystem.Heads);
			Console.WriteLine("ActiveFat: {0}", fileSystem.ActiveFat);
			Console.WriteLine("FATType: {0}", fileSystem.FatVariant);
			Console.WriteLine("FatSize: {0}", fileSystem.FatSize);
			Console.WriteLine("SectorsPerCluster: {0}", fileSystem.SectorsPerCluster);
			Console.WriteLine("SectorsPerTrack: {0}", fileSystem.SectorsPerTrack);
			Console.WriteLine("TotalSectors: {0}", fileSystem.TotalSectors);
			Console.WriteLine("FatCount: {0}", fileSystem.FatCount);

			long bytes = fileSystem.TotalSectors * fileSystem.BytesPerSector;
			string[] suf = { "B", "KB", "MB", "GB", "TB", "PB" };
			int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
			double num = Math.Round(bytes / Math.Pow(1024, place), 1);
			string readable = num.ToString() + suf[place];

			Console.WriteLine("Size: {0}", readable);
		}
	}
}
