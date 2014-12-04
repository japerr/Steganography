using System;

namespace Steganography
{
	/// <summary>
	/// Exception thrown when no carrier cluster streams are found
	/// </summary>
	public class NoCarrierClusterStreamsFoundException : Exception
	{
		/// <summary>
		/// Constructor initialized with search path that result in no carrer cluster streams being found
		/// </summary>
		/// <param name="searchPath">Carrier cluster stream search path</param>
		public NoCarrierClusterStreamsFoundException(string searchPath)
			: base(string.Format("No carrier cluster streams were found in \"{0}\"", searchPath))
		{
		}
	}
}
