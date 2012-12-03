namespace Steganography.IO.Implementation
{
	/// <summary>
	/// Stream segment
	/// </summary>
	internal class StreamSegment
	{
		/// <summary>
		/// Stream segment start value
		/// </summary>
		public long Start { get; set; }
		/// <summary>
		/// Stream segment end value
		/// </summary>
		public long End { get; set; }
		/// <summary>
		/// Carrier stream instance
		/// </summary>
		public ICarrierStream CarrierStream { get; set; }

		/// <summary>
		/// Determine if the carrier stream intersects with the given start and end
		/// </summary>
		/// <param name="start">Beginning of the line segment</param>
		/// <param name="end">End of the line segment</param>
		/// <returns>True if the carrier stream intersects with the given start and end</returns>
		public bool Intersects(long start, long end)
		{
			if (start >= Start && start <= End)
				return true;

			if (end >= Start && end <= End)
				return true;

			if (start <= Start && end >= End)
				return true;

			return false;
		}
	}
}
