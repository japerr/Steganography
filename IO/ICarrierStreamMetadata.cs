namespace Steganography.IO
{
	/// <summary>
	/// Metadata type for ICarrierStream
	/// </summary>
	public interface ICarrierStreamMetadata
	{
		/// <summary>
		/// Magic number which defines the file type a CarrierStream can handle
		/// </summary>
		byte[] MagicNumber { get; }
	}
}
