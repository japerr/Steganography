using System;

namespace Steganography
{
	/// <summary>
	/// Exception thrown when a carrier streams are not found
	/// </summary>
	public class NoCarrierStreamsFoundException : Exception
	{
		/// <summary>
		/// Constructor initialized with the search path that resulted in 
		///	no carrier streams found
		/// </summary>
		/// <param name="searchPath">Carrier Stream search path</param>
		public NoCarrierStreamsFoundException(string searchPath)
			: base(string.Format("No carrier streams were found in \"{0}\"", searchPath))
		{
		}
	}
}
