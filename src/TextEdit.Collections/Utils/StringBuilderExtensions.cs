using System.Text;

namespace TextEdit.Utils
{
    public static class StringBuilderExtensions
    {
        #region IndexOf

        public static int IndexOf(this StringBuilder stringBuilder, char value, int startIndex)
        {
            ThrowHelper.ThrowIfNull(stringBuilder);
            ThrowHelper.ThrowIfOutOfRange(startIndex, stringBuilder.Length, maxValueParamName: null);

            for (int i = startIndex; i < stringBuilder.Length; i++)
            {
                if (stringBuilder[i] == value)
                {
                    return i;
                }
            }

            return -1;
        }

        public static int IndexOf(this StringBuilder stringBuilder, ReadOnlySpan<char> value, int startIndex)
        {
            ThrowHelper.ThrowIfNull(stringBuilder);
            ThrowHelper.ThrowIfOutOfRange(startIndex, stringBuilder.Length, maxValueParamName: null);

            for (int i = startIndex; i <= stringBuilder.Length - value.Length; i++)
            {
                if (Match(stringBuilder, i, value))
                {
                    return i;
                }
            }

            return -1;
        }

        #endregion

        #region LastIndexOf

        public static int LastIndexOf(this StringBuilder stringBuilder, char value, int startIndex)
        {
            ThrowHelper.ThrowIfNull(stringBuilder);
            ThrowHelper.ThrowIfNull(value);

            for (int i = startIndex; i >= 0; i--)
            {
                if (stringBuilder[i] == value)
                {
                    return i;
                }
            }

            return -1;
        }

        public static int LastIndexOf(this StringBuilder stringBuilder, ReadOnlySpan<char> value, int startIndex)
        {
            ThrowHelper.ThrowIfNull(stringBuilder);
            ThrowHelper.ThrowIfOutOfRange(startIndex, stringBuilder.Length, maxValueParamName: null);

            for (int i = Math.Min(startIndex, stringBuilder.Length - value.Length); i >= 0; i--)
            {
                if (Match(stringBuilder, i, value))
                {
                    return i;
                }
            }

            return -1;
        }

        #endregion

        #region IndexOfAny

        public static int IndexOfAny(this StringBuilder stringBuilder, char value0, char value1, int startIndex)
        {
            ThrowHelper.ThrowIfNull(stringBuilder);
            ThrowHelper.ThrowIfOutOfRange(startIndex, stringBuilder.Length, maxValueParamName: null);

            for (int i = startIndex; i < stringBuilder.Length; i++)
            {
                char c = stringBuilder[i];

                if (c == value0 || c == value1)
                {
                    return i;
                }
            }

            return -1;
        }

        public static int IndexOfAny(this StringBuilder stringBuilder, char value0, char value1, char value2, int startIndex)
        {
            ThrowHelper.ThrowIfNull(stringBuilder);
            ThrowHelper.ThrowIfOutOfRange(startIndex, stringBuilder.Length, maxValueParamName: null);

            for (int i = startIndex; i < stringBuilder.Length; i++)
            {
                char c = stringBuilder[i];

                if (c == value0 || c == value1 || c == value2)
                {
                    return i;
                }
            }

            return -1;
        }

        public static int IndexOfAny(this StringBuilder stringBuilder, ReadOnlySpan<char> items, int startIndex)
        {
            ThrowHelper.ThrowIfNull(stringBuilder);
            ThrowHelper.ThrowIfOutOfRange(startIndex, stringBuilder.Length, maxValueParamName: null);

            for (int i = startIndex; i <= stringBuilder.Length; i++)
            {
                for (int j = 0; j < items.Length; j++)
                {
                    if (stringBuilder[i] == items[j])
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        public static int IndexOfAny(this StringBuilder stringBuilder, IEnumerable<char> items, int startIndex)
        {
            ThrowHelper.ThrowIfNull(stringBuilder);
            ThrowHelper.ThrowIfOutOfRange(startIndex, stringBuilder.Length, maxValueParamName: null);

            for (int i = startIndex; i <= stringBuilder.Length; i++)
            {
                foreach (var item in items)
                {
                    if (stringBuilder[i] == item)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        #endregion

        #region LastIndexOf

        public static int LastIndexOfAny(this StringBuilder stringBuilder, char value0, char value1, int startIndex)
        {
            ThrowHelper.ThrowIfNull(stringBuilder);
            ThrowHelper.ThrowIfOutOfRange(startIndex, stringBuilder.Length);

            for (int i = startIndex; i >= 0; i--)
            {
                char c = stringBuilder[i];

                if (c == value0 || c == value1)
                {
                    return i;
                }
            }

            return -1;
        }

        public static int LastIndexOfAny(this StringBuilder stringBuilder, char value0, char value1, char value2, int startIndex)
        {
            ThrowHelper.ThrowIfNull(stringBuilder);
            ThrowHelper.ThrowIfOutOfRange(startIndex, stringBuilder.Length);

            for (int i = startIndex; i >= 0; i--)
            {
                char c = stringBuilder[i];

                if (c == value0 || c == value1 || c == value2)
                {
                    return i;
                }
            }

            return -1;
        }

        public static int LastIndexOfAny(this StringBuilder stringBuilder, ReadOnlySpan<char> items, int startIndex)
        {
            ThrowHelper.ThrowIfNull(stringBuilder);
            ThrowHelper.ThrowIfOutOfRange(startIndex, stringBuilder.Length);

            for (int i = Math.Min(startIndex, stringBuilder.Length); i >= 0; i--)
            {
                for (int j = 0; j < items.Length; j++)
                {
                    if (stringBuilder[i] == items[j])
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        public static int LastIndexOfAny(this StringBuilder stringBuilder, IEnumerable<char> items, int startIndex)
        {
            ThrowHelper.ThrowIfNull(stringBuilder);
            ThrowHelper.ThrowIfOutOfRange(startIndex, stringBuilder.Length);

            for (int i = Math.Min(startIndex, stringBuilder.Length); i >= 0; i--)
            {
                foreach (var item in items)
                {
                    if (stringBuilder[i] == item)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        #endregion

        private static bool Match(StringBuilder stringBuilder, int startIndex, ReadOnlySpan<char> value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                if (stringBuilder[startIndex + i] != value[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}