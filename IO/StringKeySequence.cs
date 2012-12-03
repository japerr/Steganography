using System.Text;

namespace Steganography.IO
{
	/// <summary>
	/// String based key sequence
	/// </summary>
	public class StringKeySequence : ByteArrayKeySequence
	{
		/// <summary>
		/// Constructor initialized with the string which is used create a byte array
		/// </summary>
		/// <param name="key">String or password</param>
		public StringKeySequence(string key)
			: base(Encoding.ASCII.GetBytes(key))
		{
		}
	}
}
