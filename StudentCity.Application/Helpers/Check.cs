using System;
using System.Collections.Generic;

namespace StudentCity.Application.Helpers
{
    /// <summary>
    /// <para>This is a Argument Check Helper
    /// It should NOT be used for Business Logic</para>
    /// </summary>
    public class Check
    {
        #region Date Checkers
        /// <summary>
        /// check to see if value is min date
        /// </summary>
        /// <param name="value">value to be checked</param>
        /// <param name="parameterName">name of the parameter to be checked</param>
        public static void ThrowIfDateInvalid(DateTime value, string parameterName)
        {
            if (IsDateInvalid(value))
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }

        public static bool IsDateInvalid(DateTime value)
        {
            return (value == DateTime.MinValue);
        }
        #endregion

        #region Int Checkers
        /// <summary>
        /// check to see if value is less than or equal to zero
        /// </summary>
        /// <param name="value">value to be checked</param>
        /// <param name="parameterName">name of the parameter to be checked</param>
        public static void ThrowIfLessEqualZero(int value, string parameterName)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }
        /// <summary>
        /// check to see if value is less zero
        /// </summary>
        /// <param name="value">value to be checked</param>
        /// <param name="parameterName">name of the parameter to be checked</param>
        public static void ThrowIfLessZero(int value, string parameterName)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }

        /// <summary>
        /// check to see if value is less than or equal to zero
        /// </summary>
        /// <param name="value">value to be checked</param>
        /// <param name="minRange"></param>
        /// <param name="maxRange"></param>
        /// <param name="parameterName">name of the parameter to be checked</param>
        public static void ThrowIfIntOutOfRange(int value, int minRange, int maxRange, string parameterName)
        {
            if (value < minRange || value > maxRange)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }
        #endregion

        /// <summary>
        /// check to see if value is less than or equal to zero
        /// </summary>
        /// <param name="value">value to be checked</param>
        /// <param name="parameterName">name of the parameter to be checked</param>
        public static void ThrowIfLessEqualZero(double value, string parameterName)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }
        /// <summary>
        /// check to see if value is less than or equal to zero
        /// </summary>
        /// <param name="value">value to be checked</param>
        /// <param name="parameterName">name of the parameter to be checked</param>
        public static void ThrowIfLessEqualZero(decimal value, string parameterName)
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }
        /// <summary>
        /// check to see if String value is empty
        /// </summary>
        /// <param name="value">value to be checked</param>
        /// <param name="parameterName">name of the parameter to be checked</param>
        public static void ThrowIfStringIsNullOrEmptyOrWhiteSpace(string value, string parameterName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(parameterName);
            }
        }
        /// <summary>
        /// check to see if object is empty or not
        /// </summary>
        /// <param name="value">value to be checked</param>
        /// <param name="parameterName">name of the parameter to be checked</param>
        public static void ThrowIfNull(object value, string parameterName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }
        /// <summary>
        /// check to see if Guid value is empty
        /// </summary>
        /// <param name="value">value to be checked</param>
        /// <param name="parameterName">name of the parameter to be checked</param>
        public static void ThrowIfGuidIsNullOrEmpty(Guid? value, string parameterName)
        {
            if (value == Guid.Empty || value == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }

        /// <summary>
        /// check to see if List value is empty
        /// </summary>
        /// <param name="value">value to be checked</param>
        /// <param name="parameterName">name of the parameter to be checked</param>
        public static void ThrowIfListIsNullOrEmpty<T>(IList<T> value, string parameterName)
        {
            if (value == null || value.Count == 0)
            {
                throw new ArgumentNullException(parameterName);
            }
        }
        /// <summary>
        /// check to see if expression is true
        /// </summary>
        /// <param name="checkFunction">function to evaluate</param>
        /// <param name="parameterName">name of the parameter to be checked</param>
        public static void ThrowIfFalse(Func<bool> checkFunction, string parameterName)
        {
            ThrowIfFalse(checkFunction(), parameterName);
        }

        public static void ThrowIfFalse(bool valid, string parameterName)
        {
            if (!valid)
            {
                throw new ArgumentException(parameterName);
            }
        }
    }
}
