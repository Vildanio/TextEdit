using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace TextEdit
{
    internal static class ThrowHelper
    {
        /// <summary>
        /// Throws <see cref="ArgumentNullException"/> if the <paramref name="obj"/> is null
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="paramName"></param>
        /// <exception cref="ArgumentNullException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfNull([NotNull] object? obj, [CallerArgumentExpression(nameof(obj))] string? paramName = null)
        {
            if (obj is null)
                throw new ArgumentNullException(paramName);
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the given <paramref name="start"/> or <paramref name="count"/> less than 0 or their sum is greater than <paramref name="maxCount"/>
        /// </summary>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <param name="maxCount"></param>
        /// <param name="startParamName"></param>
        /// <param name="countParamName"></param>
        /// <param name="maxCountParamName"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfOutOfRange(
            int start, int count, int maxCount,
            [CallerArgumentExpression(nameof(start))] string? startParamName = null,
            [CallerArgumentExpression(nameof(count))] string? countParamName = null,
            [CallerArgumentExpression(nameof(maxCount))] string? maxCountParamName = null)
        {
            if (start < 0 || start > maxCount)
                throw new ArgumentOutOfRangeException(startParamName, $"Value must be positive and less than {maxCountParamName}");

            if (count < 0 || start + count > maxCount)
                throw new ArgumentOutOfRangeException(countParamName, $"Value must be positive and less than {maxCountParamName}");
        }

        /// <summary>
        /// Throws <see cref="ArgumentOutOfRangeException"/> if the given <paramref name="value"/> is less than zero and greater than <paramref name="maxValue"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxValue"></param>
        /// <param name="valueParamName"></param>
        /// <param name="maxValueParamName"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfOutOfRange(int value, int maxValue,
            [CallerArgumentExpression(nameof(value))] string? valueParamName = null,
            [CallerArgumentExpression(nameof(maxValue))] string? maxValueParamName = null)
        {
            if (value < 0 || value > maxValue)
            {
                valueParamName = valueParamName ?? "value";

                if (maxValueParamName is null)
                {
                    throw new ArgumentOutOfRangeException(valueParamName);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(valueParamName, $"Value must be positive and less than {maxValueParamName}");
                }
            }
        }

        /// <summary>
        /// Throws <see cref="ArgumentException"/> if the given <paramref name="value"/> is less than zero
        /// </summary>
        /// <typeparam name="TNumber"></typeparam>
        /// <param name="value"></param>
        /// <param name="paramName"></param>
        /// <exception cref="ArgumentException"></exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfNegative<TNumber>(TNumber value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
            where TNumber : ISignedNumber<TNumber>
        {
            if (TNumber.IsNegative(value))
                throw new ArgumentException("Value cannot be negative", paramName);
        }
    }
}
