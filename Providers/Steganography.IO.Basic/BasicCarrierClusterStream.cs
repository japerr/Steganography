using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Logging;

namespace Steganography.IO.Basic
{
	/// <summary>
	/// Public carrier stream implementation
	/// </summary>
	[CarrierClusterStream("Basic")]
	public class BasicCarrierClusterStream : BaseCarrierClusterStream
	{
		/// <summary>
		/// Logger instance
		/// </summary>
		private ILog _logger = LogManager.GetCurrentClassLogger();
		/// <summary>
		/// Key sequence
		/// </summary>
		private IKeySequence _keySequence = null;
		/// <summary>
		/// Segment/Stream bag
		/// </summary>
		private ConcurrentBag<StreamSegment> _carrierStreams = null;
		/// <summary>
		/// Indicates the stream can be read
		/// </summary>
		private bool _canRead = false;
		/// <summary>
		/// Indicates the stream allows seeking
		/// </summary>
		private bool _canSeek = false;
		/// <summary>
		/// Indicates the stream is writable
		/// </summary>
		private bool _canWrite = false;
		/// <summary>
		/// Max Length of the stream
		/// </summary>
		private long _maxLength = 0;
		/// <summary>
		/// Length of the stream
		/// </summary>
		private long _length = 0;
		/// <summary>
		/// Current stream position
		/// </summary>
		private long _position = 0;

		/// <summary>
		/// Indicates the stream can be read
		/// </summary>
		/// <remarks>true if the stream supports reading; otherwise, false</remarks>
		public override bool CanRead
		{
			get
			{
				return _canRead; 
			}
		}
		/// <summary>
		/// Indicates the stream allows seeking
		/// </summary>
		/// <remarks>true if the stream supports seeking; otherwise, false</remarks>
		public override bool CanSeek
		{
			get
			{
				return _canSeek; 
			}
		}
		/// <summary>
		/// Indicates the stream is writable
		/// </summary>
		/// <remarks>A long value representing the length of the stream in bytes</remarks>
		public override bool CanWrite
		{
			get
			{
				return _canWrite; 
			}
		}
		/// <summary>
		/// Gets the length in bytes of the stream
		/// </summary>
		public override long Length 
		{
			get
			{
				return _length;
			}
		}
		/// <summary>
		/// Gets or sets the position within the current stream
		/// </summary>
		public override long Position
		{
			get
			{
				return _position;
			}

			set
			{
				if (value > _length)
					throw new Exception();

				if (value < 0)
					throw new Exception();
								
				_position = value;
			}
		}

		/// <summary>
		/// Initialize the cluster stream
		/// </summary>
		/// <param name="keySequence">Key sequence instance</param>
		/// <param name="carrierStreams">Carrier streams to initialized with</param>
		/// <remarks>
		/// Key Step Lengths(KSL)
		///	Key Step Length Size(KSLS)
		///	Writable Carrier Stream Length(WCSL)
		/// WCSL / Sum(KSL) * KSLS + WCSL % Sum(KSL) = Length
		/// </remarks>
		public override void Initialize(IKeySequence keySequence, IEnumerable<ICarrierStream> carrierStreams)
		{
			_keySequence = keySequence;

			long currentStart = 0;

			_carrierStreams = new ConcurrentBag<StreamSegment>(
				carrierStreams.Select(cs => 
				{
					long closureCurrentStart = currentStart;
					long end = closureCurrentStart + cs.Length - 1;
					currentStart = end + 1;
					return new StreamSegment
					{
						Start = closureCurrentStart,
						End = end,
						CarrierStream = cs
					};
				}));

			_canRead = _carrierStreams.All(s => s.CarrierStream.CanRead);
			_canSeek = _carrierStreams.All(s => s.CarrierStream.CanSeek);
			_canWrite = _carrierStreams.All(s => s.CarrierStream.CanWrite);

			long carrierSumLength = _carrierStreams.Sum(s => s.CarrierStream.Length);

			_maxLength = carrierSumLength / _keySequence.TotalStepSum * _keySequence.Length
				+ carrierSumLength % _keySequence.TotalStepSum;

			_length = _maxLength;

			_logger.Info(h => h("Initialize: KeySequence Length: {0}, Stream Lengh = {1}",
				_keySequence.Length, _length));
		}
		/// <summary>
		/// Move the current postion based on the origin and the offset
		/// </summary>
		/// <param name="offset">Distance from the origin to set the position</param>
		/// <param name="origin">The initial position to calculate the new position from</param>
		/// <returns>The new position</returns>
		public override long Seek(long offset, SeekOrigin origin)
		{
			_logger.Debug(h => h("Seek: Offset = {0}, Origin = {1}", offset, origin.ToString()));

			switch (origin)
			{
				case SeekOrigin.Begin:
					Position = 0 + offset;
					break;

				case SeekOrigin.Current:
					Position += offset;
					break;

				default: //SeekOrigin.End
					Position = Length - 1 + offset;
					break;
			}

			return Position;
		}
		/// <summary>
		/// Sets the length of the current stream
		/// </summary>
		/// <param name="value">The desired length of the current stream in bytes</param>
		public override void SetLength(long value)
		{
			if (value > _maxLength)
				throw new IOException(
					string.Format("New length: {0} is longer than the stream maximum length: {1}",
						value, _maxLength));

			_logger.Debug(h => h("SetLength: Value = {0}", value));
			_length = value;
		}
		/// <summary>
		/// Reads the carrier stream cluster given the offset and count
		/// </summary>
		/// <param name="buffer">Byte array to populate while reading stream</param>
		/// <param name="offset">Zero-based byte offset in buffer at which to begin storing the data</param>
		/// <param name="count">The maximum number of bytes to be read from the current stream</param>
		/// <returns>The total number of bytes read into the buffer</returns>
		public override int Read(byte[] buffer, int offset, int count)
		{
			int bytesRead = GetIntersectingStreams(count)
				.Aggregate(0, (s, v) =>
				{
					int carrierOffset = (int)Math.Max(v.Start - Position, 0) + offset;
					int carrierCount = (int)Math.Min(v.CarrierStream.Length + Math.Min(v.Start - Position, 0), 
						count + offset - carrierOffset);
					long carrierPosition = Math.Abs(Math.Min(v.Start - Position, 0));

					_logger.Debug(h => h("Read[{0} - {1}]: Offset = {2}, Count = {3}, CarrierOffset = {4}, "
						+ "CarrierCount = {5}, CarrierPosition = {6}",
						v.Start, v.End, offset, count, carrierOffset, carrierCount, carrierPosition));

					s += v.CarrierStream.Read(carrierPosition, buffer, carrierOffset, carrierCount);
					return s;
				});

			Position += bytesRead;
			return bytesRead;
		}
		/// <summary>
		/// Writes to the carrier stream cluster given the offset and count
		/// </summary>
		/// <param name="buffer">An array of bytes. This method copies count bytes from buffer to the current stream</param>
		/// <param name="offset">The zero-based byte offset in buffer at which to begin copying bytes to the current stream</param>
		/// <param name="count">The number of bytes to be written to the current stream</param>
		public override void Write(byte[] buffer, int offset, int count)
		{
			GetIntersectingStreams(count)
				.ForAll(i =>
				{
					int carrierOffset = (int)Math.Max(i.Start - Position, 0) + offset;
					int carrierCount = (int)Math.Min(i.CarrierStream.Length + Math.Min(i.Start - Position, 0), 
						count + offset - carrierOffset);
					long carrierPosition = Math.Abs(Math.Min(i.Start - Position, 0));

					_logger.Debug(h => h("Write[{0} - {1}]: Offset = {2}, Count = {3}, CarrierOffset = {4}, "
						+ "CarrierCount = {5}, CarrierPosition = {6}",
						i.Start, i.End, offset, count, carrierOffset, carrierCount, carrierPosition));

					i.CarrierStream.Write(carrierPosition, buffer, carrierOffset, carrierCount);
				});

			Position += count;
		}
		/// <summary>
		/// clears all streams and forces the underlying carrier streams to flush
		/// </summary>
		public override void Flush()
		{
			_logger.Debug("Flushing");
			_carrierStreams
				.AsParallel()
				.ForAll(s => s.CarrierStream.Flush());
		}		
		/// <summary>
		/// Dispose all the carrier streams
		/// </summary>
		public new void Dispose()
		{
			if(_carrierStreams == null)
				return;

			_logger.Debug("Dispose");
			_carrierStreams.AsParallel()
				.ForAll(s => s.CarrierStream.Dispose());
		}
		/// <summary>
		/// Get all stream segments which intersect with the current position -> position + count
		/// </summary>
		/// <param name="count">Number of bytes from the current position</param>
		/// <returns>Parallel query of stream segments which intersect</returns>
		private ParallelQuery<StreamSegment> GetIntersectingStreams(int count)
		{
			ParallelQuery<StreamSegment> intersections = _carrierStreams
				.AsParallel()
				.Where(i => i.Intersects(Position, Position + count));
			
			_logger.Debug(h => h("GetIntersectingStreams: Count = {0}, Intersection Count = {1}",
				count, intersections.Count()));

			return intersections;
		}
	}
}
