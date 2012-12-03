using System.Collections.Generic;

namespace Steganography.IO
{
	/// <summary>
	/// Interface which describes key sequences
	/// </summary>
	public interface IKeySequence
	{
		/// <summary>
		/// Total step sum accessor
		/// </summary>
		int TotalStepSum { get; }
		/// <summary>
		/// Key buffer length accessor
		/// </summary>
		int Length { get; }

		/// <summary>
		/// Get a sub set of the infinite sequence
		/// </summary>
		/// <param name="startAt">Index to start from</param>
		/// <param name="count">Size of the sub sequence</param>
		/// <returns>Enumerable of bytes</returns>
		IEnumerable<byte> GetSequence(int index, int count);
	}
}
