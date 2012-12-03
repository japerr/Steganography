using System;
using System.Linq;

namespace Steganography.IO
{
	/// <summary>
	/// Exception thrown when a duplicate magic number is found
	/// </summary>
	public class DuplicateMagicNumberDefinedException : Exception
	{
		/// <summary>
		/// Constructor initialized with the duplication magic number, the existing type defined for the magic number 
		/// and the type redefining the magic number
		/// </summary>
		/// <param name="magicNumber">Duplication magic number</param>
		/// <param name="existingDefinition">Existing type defined for the magic number</param>
		/// <param name="redefinition">Type redefining the magic number</param>
		public DuplicateMagicNumberDefinedException(byte[] magicNumber, Type existingDefinition, Type redefinition)
			: base(string.Format("The magic number \"{0}\" key already exists for type {1}, but is defined again for type {2}",
				magicNumber.Aggregate(string.Empty, (s, v) => s += v.ToString("X2")), existingDefinition.Name, redefinition.Name))
		{
		}
	}
}
