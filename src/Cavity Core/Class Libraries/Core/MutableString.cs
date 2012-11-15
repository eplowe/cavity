namespace Cavity
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
#if !NET20
    using System.Linq;
#endif
    using System.Runtime.Serialization;
#if NET20 || NET35
    using System.Security.Permissions;
#endif
    using System.Text;

    using Cavity.Collections;

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This is not a collection.")]
    [Serializable]
    public sealed class MutableString : ICloneable, 
                                        IComparable, 
                                        IComparable<MutableString>, 
                                        IEnumerable<char>, 
                                        IEquatable<MutableString>, 
                                        ISerializable
    {
        private readonly StringBuilder _value;

        public MutableString()
        {
            _value = new StringBuilder();
        }

        public MutableString(char seed, 
                             int length)
        {
            _value = new StringBuilder(new string(seed, length));
        }

        public MutableString(params string[] values)
            : this()
        {
            Append(values);
        }

        public MutableString(StringBuilder value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            _value = new StringBuilder(value.ToString());
        }

        private MutableString(SerializationInfo info, 
                              StreamingContext context)
        {
            if (null == info)
            {
                return;
            }

            _value = new StringBuilder(info.GetString("_value"));
        }

        public int Capacity
        {
            get
            {
                return _value.Capacity;
            }

            set
            {
                _value.Capacity = value;
            }
        }

        public int Length
        {
            get
            {
                return _value.Length;
            }
        }

        public char this[int index]
        {
            get
            {
                return _value[index];
            }

            set
            {
                _value[index] = value;
            }
        }

        public static implicit operator MutableString(StringBuilder value)
        {
            return null == value
                       ? null
                       : new MutableString(value);
        }

        public static implicit operator MutableString(string value)
        {
            return null == value
                       ? null
                       : new MutableString(value);
        }

        public static implicit operator string(MutableString value)
        {
            return null == value
                       ? null
                       : value.ToString();
        }

        public static bool operator >(MutableString obj, 
                                      MutableString comparand)
        {
            return 0 < string.CompareOrdinal(obj, comparand);
        }

        public static bool operator <(MutableString obj, 
                                      MutableString comparand)
        {
            return 0 > string.CompareOrdinal(obj, comparand);
        }

        public static bool operator ==(MutableString obj, 
                                       MutableString comparand)
        {
            if (ReferenceEquals(null, obj))
            {
                return ReferenceEquals(null, comparand);
            }

            return obj.Equals(comparand);
        }

        public static bool operator !=(MutableString obj, 
                                       MutableString comparand)
        {
            if (ReferenceEquals(null, obj))
            {
                return !ReferenceEquals(null, comparand);
            }

            return !obj.Equals(comparand);
        }

        public static int Compare(MutableString comparand1, 
                                  MutableString comparand2)
        {
            var x = ReferenceEquals(null, comparand1) ? null : comparand1;
            var y = ReferenceEquals(null, comparand2) ? null : comparand2;
            return string.CompareOrdinal(x, y);
        }

        public static bool IsNullOrEmpty(MutableString obj)
        {
            if (null == obj)
            {
                return true;
            }

            return 0 == obj.Length;
        }

        public static bool IsNullOrWhiteSpace(MutableString obj)
        {
            if (null == obj)
            {
                return true;
            }

#if NET20
            if (0 == obj.Length)
            {
                return true;
            }

            for (var i = 0; i < obj.Length; i++)
            {
                if (CharExtensionMethods.IsWhiteSpace(obj[i]))
                {
                    continue;
                }

                return false;
            }

            return true;
#else
            return 0 == obj.Length || obj.All(x => obj[0].IsWhiteSpace());
#endif
        }

        public MutableString Append(char value)
        {
            _value.Append(value);

            return this;
        }

        public MutableString Append(IEnumerable<string> values)
        {
            if (null == values)
            {
                return this;
            }

            foreach (var value in values)
            {
                Append(value);
            }

            return this;
        }

        public MutableString Append(IEnumerable<string> values, 
                                    char separator)
        {
            if (null == values)
            {
                return this;
            }

            foreach (var value in values)
            {
                if (0 != _value.Length)
                {
                    Append(separator);
                }

                Append(value);
            }

            return this;
        }

        public MutableString Append(string value)
        {
            _value.Append(value);

            return this;
        }

        public MutableString AppendLine(string value)
        {
            _value.AppendLine(value);

            return this;
        }

        public MutableString Clear()
        {
#if NET20 || NET35
            _value.Remove(0, _value.Length);
#else
            _value.Clear();
#endif

            return this;
        }

        public MutableString Clone()
        {
            return new MutableString(_value);
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        int IComparable<MutableString>.CompareTo(MutableString other)
        {
            return Compare(this, other);
        }

        int IComparable.CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return 1;
            }

            return (this as IComparable<MutableString>).CompareTo((MutableString)obj);
        }

        public bool Contains(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (0 == value.Length)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            Queue<char> letters = null;
            for (var i = 0; i < _value.Length; i++)
            {
                if (null == letters)
                {
#if NET20
                    letters = new Queue<char>();
                    foreach (var letter in value)
                    {
                        letters.Enqueue(letter);
                    }
#else
                    letters = value.ToQueue();
#endif
                }

                if (this[i] ==
                    letters.Peek())
                {
                    if (1 == letters.Count)
                    {
                        return true;
                    }

                    letters.Dequeue();
                    continue;
                }

                letters = null;
            }

            return false;
        }

        public bool ContainsAny(params char[] values)
        {
            if (null == values)
            {
                throw new ArgumentNullException("values");
            }

#if NET20
            foreach (var value in values)
            {
                if (-1 != IndexOf(value))
                {
                    return true;
                }
            }

            return false;
#else
            return values.Any(value => -1 != IndexOf(value));
#endif
        }

        public bool ContainsAny(params string[] values)
        {
            if (null == values)
            {
                throw new ArgumentNullException("values");
            }

#if NET20
            foreach (var value in values)
            {
                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }

                if (Contains(value))
                {
                    return true;
                }
            }

            return false;
#else
            return values.Where(value => !string.IsNullOrEmpty(value)).Any(Contains);
#endif
        }

        public bool ContainsText()
        {
            return 0 != Length;
        }

        public MutableString Crop(int start, 
                                  int length)
        {
            if (1 > length)
            {
                throw new ArgumentOutOfRangeException("length");
            }

            if (0 == start &&
                length == _value.Length)
            {
                return Clear();
            }

            var end = start + length;
            _value.Remove(end, _value.Length - end);

            _value.Remove(0, start);

            return this;
        }

        public IEnumerable<char> Digits()
        {
#if NET20
            if (0 == _value.Length)
            {
                yield break;
            }

            for (var i = 0; i < _value.Length; i++)
            {
                if (!char.IsDigit(_value[i]))
                {
                    continue;
                }

                yield return _value[i];
            }
#else
            return EnumerateCharacters(char.IsDigit);
#endif
        }

        public bool EndsWith(char value)
        {
            if (0 == _value.Length)
            {
                return false;
            }

            return value == _value[_value.Length - 1];
        }

        public bool EndsWith(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (0 == value.Length)
            {
                return false;
            }

            if (0 == _value.Length)
            {
                return false;
            }

            if (value.Length >
                _value.Length)
            {
                return false;
            }

            if (value.Length ==
                _value.Length)
            {
                return Equals(value);
            }

            if (1 == value.Length)
            {
                return EndsWith(value[0]);
            }

            var start = _value.Length - value.Length;

            return -1 != IndexOf(value, start, value.Length);
        }

        public bool EndsWithAny(params string[] values)
        {
            if (null == values)
            {
                throw new ArgumentNullException("values");
            }

#if NET20
            foreach (var value in values)
            {
                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }

                if (EndsWith(value))
                {
                    return true;
                }
            }

            return false;
#else
            return values.Where(value => !string.IsNullOrEmpty(value)).Any(EndsWith);
#endif
        }

        public override bool Equals(object obj)
        {
            return !ReferenceEquals(null, obj) && Equals((MutableString)obj);
        }

        public bool Equals(string other)
        {
            return null != other && Equals(other.ToCharArray());
        }

        public bool Equals(MutableString other)
        {
            return null != other && Equals(other.ToCharArray());
        }

        public bool EqualsAny(params string[] values)
        {
            if (null == values)
            {
                throw new ArgumentNullException("values");
            }

#if NET20
            foreach (var value in values)
            {
                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }

                if (Equals(value))
                {
                    return true;
                }
            }

            return false;
#else
            return values.Where(value => !string.IsNullOrEmpty(value)).Any(Equals);
#endif
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<char> GetEnumerator()
        {
            for (var i = 0; i < _value.Length; i++)
            {
                yield return _value[i];
            }
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public int IndexOf(char value)
        {
            return IndexOf(value, 0);
        }

        public int IndexOf(char value, 
                           int start)
        {
            return IndexOf(value, start, _value.Length - start);
        }

        public int IndexOf(char value, 
                           int start, 
                           int length)
        {
            if (0 > start)
            {
                throw new ArgumentOutOfRangeException("start", ExceptionMessage.NegativeStartIndex(start));
            }

            if (0 > length)
            {
                throw new ArgumentOutOfRangeException("length", ExceptionMessage.NegativeLength(length));
            }

            if (start > _value.Length)
            {
                throw new ArgumentOutOfRangeException("start", ExceptionMessage.StartIndexAfterValueLength(start, ToString()));
            }

            if (start + length >
                _value.Length)
            {
                throw new ArgumentOutOfRangeException("length", ExceptionMessage.StartIndexAndLengthAfterValueLength(start, length, ToString()));
            }

            if (0 == _value.Length)
            {
                return -1;
            }

            for (var i = start; i < start + length; i++)
            {
                if (this[i] == value)
                {
                    return i;
                }
            }

            return -1;
        }

        public int IndexOf(string value)
        {
            return IndexOf(value, 0);
        }

        public int IndexOf(string value, 
                           int start)
        {
            return IndexOf(value, start, _value.Length - start);
        }

        public int IndexOf(string value, 
                           int start, 
                           int length)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (0 > start)
            {
                throw new ArgumentOutOfRangeException("start", ExceptionMessage.NegativeStartIndex(start));
            }

            if (0 > length)
            {
                throw new ArgumentOutOfRangeException("length", ExceptionMessage.NegativeLength(length));
            }

            if (start > _value.Length)
            {
                throw new ArgumentOutOfRangeException("start", ExceptionMessage.StartIndexAfterValueLength(start, ToString()));
            }

            if (start + length >
                _value.Length)
            {
                throw new ArgumentOutOfRangeException("length", ExceptionMessage.StartIndexAndLengthAfterValueLength(start, length, ToString()));
            }

            if (value.Length > length)
            {
                throw new ArgumentOutOfRangeException("length", ExceptionMessage.LengthShorterThanValueLength(length, value));
            }

            if (0 == _value.Length)
            {
                return -1;
            }

            if (0 == value.Length)
            {
                return -1;
            }

            if (1 == value.Length)
            {
                return IndexOf(value[0]);
            }

            var max = start + length;
            for (var i = start; i < max; i++)
            {
                if (_value[i] !=
                    value[0])
                {
                    continue;
                }

                if (i + value.Length > max)
                {
                    return -1;
                }

                for (var j = 1; j < value.Length; j++)
                {
                    if (_value[i + j] !=
                        value[j])
                    {
                        break;
                    }

                    if (j == value.Length - 1)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        public IEnumerable<int> IndexesOf(char value)
        {
            for (var i = 0; i < _value.Length; i++)
            {
                if (_value[i] == value)
                {
                    yield return i;
                }
            }
        }

        public MutableString Insert(int index, 
                                    char value)
        {
            _value.Insert(index, value);

            return this;
        }

        public MutableString Insert(int index, 
                                    string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            return Insert(index, value.ToCharArray());
        }

        public MutableString Insert(int index, 
                                    IEnumerable<char> value)
        {
            if (0 > index)
            {
                throw new ArgumentOutOfRangeException("index", ExceptionMessage.NegativeIndex(index));
            }

            if (index > _value.Length)
            {
                throw new ArgumentOutOfRangeException("index", ExceptionMessage.IndexAfterValueLength(index, ToString()));
            }

            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            foreach (var c in value)
            {
                _value.Insert(index++, c);
            }

            return this;
        }

        public int LastIndexOf(string value, 
                               int start, 
                               int length)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (0 > start)
            {
                throw new ArgumentOutOfRangeException("start", ExceptionMessage.NegativeStartIndex(start));
            }

            if (0 > length)
            {
                throw new ArgumentOutOfRangeException("length", ExceptionMessage.NegativeLength(length));
            }

            if (start > _value.Length)
            {
                throw new ArgumentOutOfRangeException("start", ExceptionMessage.StartIndexAfterValueLength(start, ToString()));
            }

            if (start + length >
                _value.Length)
            {
                throw new ArgumentOutOfRangeException("length", ExceptionMessage.StartIndexAndLengthAfterValueLength(start, length, ToString()));
            }

            if (value.Length > length)
            {
                throw new ArgumentOutOfRangeException("length", ExceptionMessage.LengthShorterThanValueLength(length, value));
            }

            if (0 == _value.Length)
            {
                return -1;
            }

            if (0 == value.Length)
            {
                return -1;
            }

            if (1 == value.Length)
            {
                return IndexOf(value[0]);
            }

            var result = -1;
            var max = _value.Length - value.Length + 1;
            for (var i = start; i < start + max; i++)
            {
                if (_value[i] !=
                    value[0])
                {
                    continue;
                }

                for (var j = 1; j < value.Length; j++)
                {
                    if (_value[i + j] !=
                        value[j])
                    {
                        break;
                    }

                    if (j != value.Length - 1)
                    {
                        continue;
                    }

                    result = i;
                    break;
                }
            }

            return result;
        }

        public IEnumerable<char> Letters()
        {
#if NET20
            if (0 == _value.Length)
            {
                yield break;
            }

            for (var i = 0; i < _value.Length; i++)
            {
                if (!char.IsLetter(_value[i]))
                {
                    continue;
                }

                yield return _value[i];
            }
#else
            return EnumerateCharacters(char.IsLetter);
#endif
        }

        public IEnumerable<char> LettersAndDigits()
        {
#if NET20
            if (0 == _value.Length)
            {
                yield break;
            }

            for (var i = 0; i < _value.Length; i++)
            {
                if (!char.IsLetterOrDigit(_value[i]))
                {
                    continue;
                }

                yield return _value[i];
            }
#else
            return EnumerateCharacters(char.IsLetterOrDigit);
#endif
        }

        public MutableString NormalizeToEnglishAlphabet()
        {
            if (0 == _value.Length)
            {
                return this;
            }

            for (var i = _value.Length - 1; i > -1; i--)
            {
                var c = _value[i];
                if (' ' == c)
                {
                    continue;
                }

                if (char.IsDigit(c))
                {
                    continue;
                }

                if (Characters.EnglishUppercase.Contains(c))
                {
                    continue;
                }

                if (Characters.EnglishLowercase.Contains(c))
                {
                    continue;
                }

#if NET20
                var english = CharExtensionMethods.ToEnglishAlphabet(c);
#else
                var english = c.ToEnglishAlphabet();
#endif
                if (!english.HasValue)
                {
                    continue;
                }

                _value[i] = english.Value;
            }

            return this;
        }

        public MutableString NormalizeToLower(CultureInfo culture)
        {
            if (null == culture)
            {
                throw new ArgumentNullException("culture");
            }

            if (0 == _value.Length)
            {
                return this;
            }

            var value = _value.ToString().ToLower(culture);

            Clear();
            Append(value);

            return this;
        }

        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "The lower case implementation is appropriate here.")]
        public MutableString NormalizeToLowerInvariant()
        {
            if (0 == _value.Length)
            {
                return this;
            }

            var value = _value.ToString().ToLowerInvariant();

            Clear();
            Append(value);

            return this;
        }

        public MutableString NormalizeToUpper(CultureInfo culture)
        {
            if (null == culture)
            {
                throw new ArgumentNullException("culture");
            }

            if (0 == _value.Length)
            {
                return this;
            }

            var value = _value.ToString().ToUpper(culture);

            Clear();
            Append(value);

            return this;
        }

        public MutableString NormalizeToUpperInvariant()
        {
            if (0 == _value.Length)
            {
                return this;
            }

            var value = _value.ToString().ToUpperInvariant();

            Clear();
            Append(value);

            return this;
        }

        public MutableString NormalizeWhiteSpace()
        {
            for (var i = 0; i < _value.Length; i++)
            {
                var c = _value[i];
                if (' ' == c)
                {
                    continue;
                }

#if NET20
                if (CharExtensionMethods.IsWhiteSpace(c))
#else
                if (c.IsWhiteSpace())
#endif
                {
                    _value[i] = ' ';
                }
            }

            return this;
        }

        public MutableString PadLeft(int width)
        {
            return PadLeft(width, ' ');
        }

        public MutableString PadLeft(int width, 
                                     char padding)
        {
            if (0 > width)
            {
                throw new ArgumentOutOfRangeException("width");
            }

            if (0 == width)
            {
                return this;
            }

            while (width > _value.Length)
            {
                Insert(0, padding);
            }

            return this;
        }

        public MutableString PadRight(int width)
        {
            return PadRight(width, ' ');
        }

        public MutableString PadRight(int width, 
                                      char padding)
        {
            if (0 > width)
            {
                throw new ArgumentOutOfRangeException("width");
            }

            if (0 == width)
            {
                return this;
            }

            while (width > _value.Length)
            {
                Append(padding);
            }

            return this;
        }

        public string Prefix(string value)
        {
            return Prefix(value, false);
        }

        public string Prefix(string value, 
                             bool trim)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            var index = IndexOf(value);
            if (-1 == index)
            {
                return string.Empty;
            }

            var substring = Substring(0, index);
            return trim
                       ? substring.Trim()
                       : substring;
        }

        public MutableString Prepend(char value)
        {
            return Insert(0, value);
        }

        public MutableString Prepend(string value)
        {
            return Insert(0, value);
        }

        public MutableString Remove(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (0 == _value.Length)
            {
                return this;
            }

            if (value.Length >
                _value.Length)
            {
                return this;
            }

            var index = IndexOf(value);
            if (-1 == index)
            {
                return this;
            }

            Remove(index, value.Length);

            return this;
        }

        public MutableString Remove(char value)
        {
            if (0 == _value.Length)
            {
                return this;
            }

            for (var i = _value.Length - 1; i > -1; i--)
            {
                if (value != _value[i])
                {
                    continue;
                }

                Remove(i, 1);
            }

            return this;
        }

        public MutableString Remove(int start, 
                                    int length)
        {
            _value.Remove(start, length);

            return this;
        }

        public MutableString RemoveAdjacentDuplicates()
        {
            if (2 > _value.Length)
            {
                return this;
            }

            for (var i = _value.Length - 1; i > 0; i--)
            {
                if (_value[i] !=
                    _value[i - 1])
                {
                    continue;
                }

                Remove(i, 1);
            }

            return this;
        }

        public MutableString RemoveAdjacentDuplicates(char value)
        {
            if (2 > _value.Length)
            {
                return this;
            }

            for (var i = _value.Length - 1; i > 0; i--)
            {
                if (_value[i] != value)
                {
                    continue;
                }

                if (_value[i] !=
                    _value[i - 1])
                {
                    continue;
                }

                Remove(i, 1);
            }

            return this;
        }

        public MutableString RemoveAllExcept(IEnumerable<char> values)
        {
            if (null == values)
            {
                throw new ArgumentNullException("values");
            }

            if (0 == _value.Length)
            {
                return this;
            }

#if NET20
            var list = IEnumerableExtensionMethods.ToList(values);
#else
            var list = values.ToList();
#endif

            if (0 == list.Count)
            {
                return Clear();
            }

            for (var i = _value.Length - 1; i > -1; i--)
            {
                if (list.Contains(_value[i]))
                {
                    continue;
                }

                Remove(i, 1);
            }

            return this;
        }

        public MutableString RemoveAny(IEnumerable<char> values)
        {
            if (null == values)
            {
                throw new ArgumentNullException("values");
            }

            if (0 == _value.Length)
            {
                return this;
            }

            foreach (var value in values)
            {
                Remove(value);
            }

            return this;
        }

        public MutableString RemoveAny(params string[] values)
        {
            if (null == values)
            {
                throw new ArgumentNullException("values");
            }

#if NET20
            foreach (var value in values)
            {
                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }
#else
            foreach (var value in values.Where(value => !string.IsNullOrEmpty(value)))
            {
#endif
                Remove(value);
            }

            return this;
        }

        public MutableString RemoveAnyFromEnd(params string[] values)
        {
            if (null == values)
            {
                throw new ArgumentNullException("values");
            }

#if NET20
            foreach (var value in values)
            {
                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }
#else
            foreach (var value in values.Where(value => !string.IsNullOrEmpty(value)))
            {
#endif
                RemoveFromEnd(value);
            }

            return this;
        }

        public MutableString RemoveAnyFromStart(params string[] values)
        {
            if (null == values)
            {
                throw new ArgumentNullException("values");
            }

#if NET20
            foreach (var value in values)
            {
                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }
#else
            foreach (var value in values.Where(value => !string.IsNullOrEmpty(value)))
            {
#endif
                RemoveFromStart(value);
            }

            return this;
        }

        public MutableString RemoveDigits()
        {
#if NET20
            if (0 == _value.Length)
            {
                return this;
            }

            for (var i = _value.Length - 1; i > -1; i--)
            {
                if (!char.IsDigit(_value[i]))
                {
                    continue;
                }

                Remove(i, 1);
            }

            return this;
#else
            return RemoveCharacters(char.IsDigit);
#endif
        }

        public MutableString RemoveFromEnd(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (0 == value.Length)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            return EndsWith(value)
                       ? Remove(_value.Length - value.Length, value.Length)
                       : this;
        }

        public MutableString RemoveFromStart(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (0 == value.Length)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            return StartsWith(value)
                       ? Remove(0, value.Length)
                       : this;
        }

        public MutableString RemoveLetters()
        {
#if NET20
            if (0 == _value.Length)
            {
                return this;
            }

            for (var i = _value.Length - 1; i > -1; i--)
            {
                if (!char.IsLetter(_value[i]))
                {
                    continue;
                }

                Remove(i, 1);
            }

            return this;
#else
            return RemoveCharacters(char.IsLetter);
#endif
        }

        public MutableString RemoveLettersAndDigits()
        {
#if NET20
            if (0 == _value.Length)
            {
                return this;
            }

            for (var i = _value.Length - 1; i > -1; i--)
            {
                if (!char.IsLetterOrDigit(_value[i]))
                {
                    continue;
                }

                Remove(i, 1);
            }

            return this;
#else
            return RemoveCharacters(char.IsLetterOrDigit);
#endif
        }

        public MutableString RemoveWhiteSpace()
        {
#if NET20
            if (0 == _value.Length)
            {
                return this;
            }

            for (var i = _value.Length - 1; i > -1; i--)
            {
                if (!CharExtensionMethods.IsWhiteSpace(_value[i]))
                {
                    continue;
                }

                Remove(i, 1);
            }

            return this;
#else
            return RemoveCharacters(CharExtensionMethods.IsWhiteSpace);
#endif
        }

        public MutableString Replace(char old, 
                                     char replacement)
        {
            _value.Replace(old, replacement);

            return this;
        }

        public MutableString Replace(char old, 
                                     char replacement, 
                                     int start)
        {
            return Replace(old, replacement, start, _value.Length - start);
        }

        public MutableString Replace(char old, 
                                     char replacement, 
                                     int start, 
                                     int count)
        {
            _value.Replace(old, replacement, start, count);

            return this;
        }

        public MutableString Replace(string old, 
                                     string replacement)
        {
            _value.Replace(old, replacement);

            return this;
        }

        public MutableString Replace(string old, 
                                     string replacement, 
                                     int start)
        {
            return Replace(old, replacement, start, _value.Length - start);
        }

        public MutableString Replace(string old, 
                                     string replacement, 
                                     int start, 
                                     int count)
        {
            _value.Replace(old, replacement, start, count);

            return this;
        }

        public MutableString ReplaceStart(string value, 
                                          string replacement)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (0 == value.Length)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            if (null == replacement)
            {
                throw new ArgumentNullException("replacement");
            }

            if (0 == replacement.Length)
            {
                throw new ArgumentOutOfRangeException("replacement");
            }

            return !StartsWith(value)
                       ? this
                       : RemoveFromStart(value).Prepend(replacement);
        }

        public MutableString ReplaceEnd(string value, 
                                        string replacement)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (0 == value.Length)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            if (null == replacement)
            {
                throw new ArgumentNullException("replacement");
            }

            if (0 == replacement.Length)
            {
                throw new ArgumentOutOfRangeException("replacement");
            }

            return !EndsWith(value)
                       ? this
                       : RemoveFromEnd(value).Append(replacement);
        }

        public IEnumerable<string> Split(char value, 
                                         StringSplitOptions options)
        {
            var buffer = new StringBuilder();
            for (var i = 0; i < _value.Length; i++)
            {
                if (_value[i] != value)
                {
                    buffer.Append(_value[i]);
                    continue;
                }

                if (0 == buffer.Length &&
                    StringSplitOptions.RemoveEmptyEntries == options)
                {
                    continue;
                }

                yield return buffer.ToString();
#if NET20 || NET35
                buffer.Remove(0, buffer.Length);
#else
                buffer.Clear();
#endif
            }

            if (0 == buffer.Length &&
                StringSplitOptions.RemoveEmptyEntries == options)
            {
                yield break;
            }

            yield return buffer.ToString();
        }

        public bool StartsWith(char value)
        {
            if (0 == _value.Length)
            {
                return false;
            }

            return value == _value[0];
        }

        public bool StartsWith(string value)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (0 == value.Length)
            {
                return false;
            }

            if (0 == _value.Length)
            {
                return false;
            }

            if (value.Length >
                _value.Length)
            {
                return false;
            }

            if (value.Length ==
                _value.Length)
            {
                return Equals(value);
            }

            if (1 == value.Length)
            {
                return StartsWith(value[0]);
            }

            return 0 == IndexOf(value, 0, value.Length);
        }

        public bool StartsWithAny(params string[] values)
        {
            if (null == values)
            {
                throw new ArgumentNullException("values");
            }

#if NET20
            foreach (var value in values)
            {
                if (string.IsNullOrEmpty(value))
                {
                    continue;
                }

                if (StartsWith(value))
                {
                    return true;
                }
            }

            return false;
#else
            return values.Where(value => !string.IsNullOrEmpty(value)).Any(StartsWith);
#endif
        }

        public string Substring(int start)
        {
            return Substring(start, _value.Length - start);
        }

        public string Substring(int start, 
                                int length)
        {
            if (0 > start)
            {
                throw new ArgumentOutOfRangeException("start", ExceptionMessage.NegativeStartIndex(start));
            }

            if (0 > length)
            {
                throw new ArgumentOutOfRangeException("length", ExceptionMessage.NegativeLength(length));
            }

            if (start > _value.Length)
            {
                throw new ArgumentOutOfRangeException("start", ExceptionMessage.StartIndexAfterValueLength(start, ToString()));
            }

            if (start + length >
                _value.Length)
            {
                throw new ArgumentOutOfRangeException("length", ExceptionMessage.StartIndexAndLengthAfterValueLength(start, length, ToString()));
            }

            var buffer = new StringBuilder();
            for (var i = start; i < start + length; i++)
            {
                buffer.Append(_value[i]);
            }

            return buffer.ToString();
        }

        public string Suffix(string value)
        {
            return Suffix(value, false);
        }

        public string Suffix(string value, 
                             bool trim)
        {
            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            var index = IndexOf(value);
            if (-1 == index)
            {
                return string.Empty;
            }

            var substring = Substring(index + value.Length);
            return trim
                       ? substring.Trim()
                       : substring;
        }

        public char[] ToCharArray()
        {
            return ToCharArray(0);
        }

        public char[] ToCharArray(int start)
        {
            return ToCharArray(start, _value.Length - start);
        }

        public char[] ToCharArray(int start, 
                                  int length)
        {
            if (0 > start)
            {
                throw new ArgumentOutOfRangeException("start", ExceptionMessage.NegativeStartIndex(start));
            }

            if (0 > length)
            {
                throw new ArgumentOutOfRangeException("length", ExceptionMessage.NegativeLength(length));
            }

            if (start > _value.Length)
            {
                throw new ArgumentOutOfRangeException("start", ExceptionMessage.StartIndexAfterValueLength(start, ToString()));
            }

            if (start + length >
                _value.Length)
            {
                throw new ArgumentOutOfRangeException("length", ExceptionMessage.StartIndexAndLengthAfterValueLength(start, length, ToString()));
            }

            var result = new List<char>();
            if (0 == length)
            {
                return result.ToArray();
            }

            for (var i = 0; i < length; i++)
            {
                result.Add(_value[start + i]);
            }

            return result.ToArray();
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public MutableString Trim()
        {
            TrimStart();
            TrimEnd();

            return this;
        }

        public MutableString TrimEnd()
        {
            var max = _value.Length - 1;
            for (var i = max; i > -1; i--)
            {
                var c = _value[i];
                if (' ' == c)
                {
                    if (0 == i)
                    {
                        Clear();
                    }

                    continue;
                }

                if (max == i)
                {
                    break;
                }

                Truncate(i + 1);
                break;
            }

            return this;
        }

        public MutableString TrimStart()
        {
            for (var i = 0; i < _value.Length; i++)
            {
                var c = _value[i];
                if (' ' == c)
                {
                    if (i == _value.Length - 1)
                    {
                        Clear();
                    }

                    continue;
                }

                if (0 == i)
                {
                    break;
                }

                _value.Remove(0, i);
                break;
            }

            return this;
        }

        public MutableString Truncate(int length)
        {
            if (0 > length)
            {
                throw new ArgumentOutOfRangeException("length", ExceptionMessage.NegativeLength(length));
            }

            return 0 == length
                       ? Clear()
                       : Crop(0, length);
        }

        public IEnumerable<string> Words()
        {
            var buffer = new StringBuilder();
            for (var i = 0; i < _value.Length; i++)
            {
                if (char.IsLetterOrDigit(_value[i]))
                {
                    buffer.Append(_value[i]);
                    continue;
                }

#if NET20
                if (!CharExtensionMethods.IsWhiteSpace(_value[i]))
#else
                if (!_value[i].IsWhiteSpace())
#endif
                {
                    continue;
                }

                if (0 == buffer.Length)
                {
                    continue;
                }

                yield return buffer.ToString();
#if NET20 || NET35
                buffer.Remove(0, buffer.Length);
#else
                buffer.Clear();
#endif
            }

            if (0 == buffer.Length)
            {
                yield break;
            }

            yield return buffer.ToString();
        }

#if NET20 || NET35
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
#endif

        void ISerializable.GetObjectData(SerializationInfo info, 
                                         StreamingContext context)
        {
            if (null == info)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("_value", ToString());
        }

        private bool Equals(IList<char> value)
        {
            if (_value.Length !=
                value.Count)
            {
                return false;
            }

            for (var i = 0; i < _value.Length; i++)
            {
                if (_value[i] !=
                    value[i])
                {
                    return false;
                }
            }

            return true;
        }

#if !NET20
        private IEnumerable<char> EnumerateCharacters(Func<char, bool> func)
        {
            if (0 == _value.Length)
            {
                yield break;
            }

            for (var i = 0; i < _value.Length; i++)
            {
                if (!func(_value[i]))
                {
                    continue;
                }

                yield return _value[i];
            }
        }

        private MutableString RemoveCharacters(Func<char, bool> func)
        {
            if (0 == _value.Length)
            {
                return this;
            }

            for (var i = _value.Length - 1; i > -1; i--)
            {
                if (!func(_value[i]))
                {
                    continue;
                }

                Remove(i, 1);
            }

            return this;
        }

#endif
    }
}