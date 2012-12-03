using System;

namespace Steganography.IO
{
	/// <summary>
	/// Magic number attribute, used to define a carrier stream's magic number
	/// </summary>
	/// <remarks>
	/// http://en.wikipedia.org/wiki/Magic_number_(programming)
	/// </remarks>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public class MagicNumberAttribute : Attribute
	{
		/// <summary>
		/// Magic number byte array
		/// </summary>
		public byte[] MagicNumber { get; set; }

		/// <summary>
		/// Constructor initialized with the magic number byte array
		/// </summary>
		/// <param name="magicNumber">Magic number byte array</param>
		public MagicNumberAttribute(byte[] magicNumber)
		{
			MagicNumber = magicNumber;
		}
	}
}
