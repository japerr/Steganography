using System;
using System.Collections.Generic;
using System.Linq;

namespace Steganography.IO.Sequences
{
	/// <summary>
	/// Base byte array key sequence
	/// </summary>
	public abstract class ByteArrayKeySequence : IKeySequence
	{
		/// <summary>
		/// Key buffer
		/// </summary>
		private readonly byte[] _keyBuffer;
		/// <summary>
		/// Total step sum
		/// </summary>
		private readonly int _totalStepSum;
		/// <summary>
		/// Total step sum accessor
		/// </summary>
		public int TotalStepSum
		{
			get
			{
				return _totalStepSum;
			}
		}
		/// <summary>
		/// Key buffer length accessor
		/// </summary>
		public int Length
		{
			get
			{
				return _keyBuffer.Length;
			}
		}

		/// <summary>
		/// Constructor initialized with the key byte array
		/// </summary>
		/// <param name="key">Key byte array</param>
		public ByteArrayKeySequence(byte[] key)
		{
            if (key == null)
                throw new ArgumentNullException("key");

            if (key.Length == 0)
                throw new ArgumentOutOfRangeException("key", "key cannot be empty");

			_keyBuffer = key;

			_totalStepSum = _keyBuffer.Sum(b => (int)b);
		}

		/// <summary>
		/// Get a sub set of the infinite sequence
		/// </summary>
		/// <param name="index">Index to start from</param>
		/// <param name="count">Size of the sub sequence</param>
		/// <returns>Enumerable of bytes</returns>
		public IEnumerable<byte> GetSequence(long index, int count)
		{
			int skip = (int)(index % (long)Length);

			if (index % Length + count < Length)
				return _keyBuffer.Skip(skip).Take(count).ToList();

			List<byte> result = _keyBuffer.Skip(skip).ToList();
			count -= result.Count;
			for (int i = 0; i < count / Length; ++i)
				result.AddRange(_keyBuffer);

			result.AddRange(_keyBuffer.Take(count - count / Length * Length));

			return result;
		}
	}
}
