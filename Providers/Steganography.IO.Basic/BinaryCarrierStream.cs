using System.IO;

namespace Steganography.IO.Basic
{
	/// <summary>
	/// Carrier stream implementiation with a binary backing stream
	/// </summary>
	[CarrierStream(new byte[] { 0xEF, 0xBB, 0xBF })] // UTF-8 magic number
	public class BinaryCarrierStream : BaseCarrierStream
	{
		/// <summary>
		/// Header length
		/// </summary>
		private const int HEADER_LENGTH = 3;

		/// <summary>
		/// Initialization method which sets the backing stream
		/// </summary>
		/// <param name="stream">Backing stream</param>
		public override void Initialize(Stream stream)
		{
			base.Initialize(stream);

			Length = BackingStream.Length - HEADER_LENGTH;

			Logger.Info(h => h("Constructor: Stream Lengh = {0}, Carrier Length = {1}",
				BackingStream.Length, Length));
		}
		/// <summary>
		/// Read a given number of bytes from the current stream position
		/// </summary>
		/// <param name="position">Position in the carrier stream</param>
		/// <param name="buffer">Byte array to populate while reading stream</param>
		/// <param name="offset">Zero-based byte offset in buffer at which to begin storing the data</param>
		/// <param name="count">The maximum number of bytes to be read from the current stream</param>
		/// <returns>The total number of bytes read into the buffer</returns>
		public override int Read(long position, byte[] buffer, int offset, int count)
		{
			BackingStream.Position = position + HEADER_LENGTH;
			Logger.Debug(h => h("Read: Position = {0}, Offset = {1}, Count = {2}",
				BackingStream.Position, offset, count));

			return BackingStream.Read(buffer, offset, count);
		}
		/// <summary>
		/// Write a given number of bytes from the current stream position
		/// </summary>
		/// <param name="position">Position in the carrier stream</param>
		/// <param name="buffer">Byte array to write to the current stream position</param>
		/// <param name="offset">Zero-based byte offset in buffer at which to begin reading the data</param>
		/// <param name="count">The maximum number of bytes to be read from the buffer</param>
		public override void Write(long position, byte[] buffer, int offset, int count)
		{
			BackingStream.Position = position + HEADER_LENGTH;
			Logger.Debug(h => h("Write: Position = {0}, Offset = {1}, Count = {2}",
				BackingStream.Position, offset, count));

			BackingStream.Write(buffer, offset, count);
		}
	}
}
