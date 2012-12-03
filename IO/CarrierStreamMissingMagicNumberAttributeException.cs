using System;

namespace Steganography.IO
{
	/// <summary>
	/// Exception thrown when a carrier stream does not have a magic number attribute defined
	/// </summary>
	public class CarrierStreamMissingMagicNumberAttributeException : Exception
	{
		/// <summary>
		/// Constructor initialized with the type which is missing a magic number attribute definition
		/// </summary>
		/// <param name="type"></param>
		public CarrierStreamMissingMagicNumberAttributeException(Type type)
			: base(string.Format("CarrierStream subclass {0} does not have a MagicNumberAttribute definition", type.Name))
		{
		}
	}
}
