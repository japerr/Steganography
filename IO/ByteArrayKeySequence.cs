using System.Collections.Generic;
using System.Linq;
using CuttingEdge.Conditions;

namespace Steganography.IO
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
		/// Key buffer length
		/// </summary>
		private readonly int _length;
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
				return _length;
			}
		}

		/// <summary>
		/// Constructor initialized with the key byte array
		/// </summary>
		/// <param name="key">Key byte array</param>
		public ByteArrayKeySequence(byte[] key)
		{
			Condition.Requires(key).IsNotNull().IsNotEmpty();

			_keyBuffer = key;

			_totalStepSum = _keyBuffer.Sum(b => (int)b);

			_length = _keyBuffer.Length;
		}

		/// <summary>
		/// Get a sub set of the infinite sequence
		/// </summary>
		/// <param name="startAt">Index to start from</param>
		/// <param name="count">Size of the sub sequence</param>
		/// <returns>Enumerable of bytes</returns>
		public IEnumerable<byte> GetSequence(int startAt, int count)
		{
			if (startAt % Length + count < Length)
				return _keyBuffer.Skip(startAt % Length).Take(count).ToList();

			List<byte> result = _keyBuffer.Skip(startAt % Length).ToList();
			count -= result.Count;
			for (int i = 0; i < count / Length; ++i)
				result.AddRange(_keyBuffer);

			result.AddRange(_keyBuffer.Take(count - count / Length * Length));

			return result;
		}
	}
}
