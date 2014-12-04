namespace Steganography.IO.Sequences
{
	/// <summary>
	/// One step key sequence
	/// </summary>
	public class OneKeySequence : ByteArrayKeySequence
	{
		/// <summary>
		/// Constructor which initializes with the value 1
		/// </summary>
		public OneKeySequence()
			: base(new byte[] { 0x01 })
		{
		}
	}
}
