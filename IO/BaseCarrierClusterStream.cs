using Common.Logging;
using Steganography.IO;
using System.IO;

namespace Steganography
{
	/// <summary>
	/// Base carrier cluster steam
	/// </summary>
	public abstract class BaseCarrierClusterStream : Stream, ICarrierClusterStream
	{
		/// <summary>
		/// Logger instance
		/// </summary>
		private ILog _logger = LogManager.GetCurrentClassLogger();
		/// <summary>
		/// Logger instance
		/// </summary>
		protected ILog Logger
		{
			get
			{
				return _logger;
			}
		}

		/// <summary>
		/// Initialize the cluster stream with key sequence instance and carrierStream list
		/// </summary>
		/// <param name="keySequence">Key sequence instance</param>
		/// <param name="carrierStreams">Carrier streams to initialized with</param>
		public abstract void Initialize(IKeySequence keySequence, System.Collections.Generic.IEnumerable<ICarrierStream> carrierStreams);
	}
}
