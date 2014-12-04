using System;
using System.ComponentModel.Composition;

namespace Steganography.IO
{
	/// <summary>
	/// Carrier stream attribute, used to define a carrier stream's magic number
	/// </summary>
	/// <remarks>
	/// http://en.wikipedia.org/wiki/Magic_number_(programming)
	/// </remarks>
	[MetadataAttribute]
	[PartCreationPolicy(CreationPolicy.NonShared)]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public class CarrierStreamAttribute : ExportAttribute
	{
		/// <summary>
		/// Magic number byte array
		/// </summary>
		public byte[] MagicNumber { get; set; }

		/// <summary>
		/// Constructor initialized with the magic number byte array
		/// </summary>
		/// <param name="magicNumber">Magic number byte array</param>
		public CarrierStreamAttribute(byte[] magicNumber)
			:base(typeof(ICarrierStream))
		{
			MagicNumber = magicNumber;
		}
	}
}
