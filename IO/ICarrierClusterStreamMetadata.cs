namespace Steganography.IO
{
	/// <summary>
	/// Metadata type for ICarrierClusterStream
	/// </summary>
	public interface ICarrierClusterStreamMetadata
	{
		/// <summary>
		/// Carrier cluster stream name, used to identify different implementations of ICarrierClusterStream
		/// </summary>
		string Name { get; set; }
	}
}
