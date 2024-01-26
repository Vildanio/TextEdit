using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace TextEdit.Collections
{
    public class LimitedDictionary<TKey, TValue> : IDictionary<TKey, TValue>
        where TKey : notnull
    {
        private readonly int maxCount;
        private readonly Dictionary<TKey, TValue> dictionary;

		/// <summary>
		/// Gets the max <see cref="Count"/> value
		/// </summary>
		public int MaxCount => maxCount;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="LimitedDictionary{TKey, TValue}"/> class
		/// </summary>
		/// <param name="maxCount"></param>
		public LimitedDictionary(int maxCount)
        {
            if (maxCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxCount), "Value cannot be equals or less than 0");

            this.maxCount = maxCount;
            this.dictionary = new Dictionary<TKey, TValue>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LimitedDictionary{TKey, TValue}"/> class with unlimited cache size
        /// </summary>
        public LimitedDictionary()
            : this(int.MaxValue)
        {

        }

        #endregion

        #region IDictionary

        public ICollection<TKey> Keys => dictionary.Keys;

        public ICollection<TValue> Values => dictionary.Values;

        public int Count => dictionary.Count;

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => ((ICollection<KeyValuePair<TKey, TValue>>)dictionary).IsReadOnly;

        public TValue this[TKey key]
        {
            get
            {
                return dictionary[key];
            }

            set
            {
                dictionary[key] = value;
            }
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue cachedData)
        {
            return dictionary.TryGetValue(key, out cachedData);
        }

        public void Add(TKey key, TValue data)
        {
            if (!dictionary.ContainsKey(key))
            {
                // If there is no place to store the data
                if (MaxCount >= 0 && MaxCount <= dictionary.Count + 1 && dictionary.Count > 0)
                {
                    // Remove the first cachedData in cache
                    var firstItem = dictionary.First();
                    dictionary.Remove(firstItem.Key);
                }

                dictionary.Add(key, data);
            }
        }

        public bool Remove(TKey key)
        {
            return dictionary.Remove(key);
        }

        public void Clear()
        {
            dictionary.Clear();
        }

        public bool ContainsKey(TKey key)
        {
            return dictionary.ContainsKey(key);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)dictionary).Add(item);
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return dictionary.Contains(item);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>)dictionary).CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>)dictionary).Remove(item);
        }

		#region IEnumerable

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        #endregion

		#endregion
    }
}
