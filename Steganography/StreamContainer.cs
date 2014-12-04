using Steganography.IO;
using System.ComponentModel.Composition;

namespace Steganography
{
	/// <summary>
	/// Simple container class for MEF imported
	/// </summary>
	/// <typeparam name="T">Carrier type, either ICarrierClusterStream or ICarrierStream</typeparam>
	/// <typeparam name="TMetadata">Carrier meta type, either ICarrierClusterStreamMetadata or ICarrierStreamMetadata</typeparam>
	internal class StreamContainer<T, TMetadata>
	{
		/// <summary>
		/// imported Carriers and thier metadata
		/// </summary>
		[ImportMany(RequiredCreationPolicy = CreationPolicy.NonShared)]
		public ExportFactory<T, TMetadata>[] Streams { get; set; }
	}
}
