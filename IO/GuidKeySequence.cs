using System;

namespace Steganography.IO
{
	/// <summary>
	/// Guid based key sequence
	/// </summary>
	public class GuidKeySequence : ByteArrayKeySequence
	{
		/// <summary>
		/// Constructor initialized with the Guid which is used create a byte array
		/// </summary>
		/// <param name="key">Guid</param>
		public GuidKeySequence(Guid key)
			: base(key.ToByteArray())
		{
		}
	}
}
