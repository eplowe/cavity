namespace Cavity
{
    using System;
#if NET20
    using System.Collections.Generic;
#endif
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
#if !NET20
    using System.Linq;
#endif
    using System.Runtime.Serialization.Formatters.Soap;
    using System.Text;
    using System.Threading;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Xml.XPath;

    public static class StringExtensionMethods
    {
#if NET20
        public static string Append(string obj, 
                                    params char[] args)
#else
        public static string Append(this string obj, 
                                    params char[] args)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            if (null == args)
            {
                return obj;
            }

            switch (args.Length)
            {
                case 0:
                    return obj;
                case 1:
                    return string.Concat(obj, args[0]);
                case 2:
                    return string.Concat(obj, args[0], args[1]);
                case 3:
                    return string.Concat(obj, args[0], args[1], args[2]);
                case 4:
                    return string.Concat(obj, args[0], args[1], args[2], args[3]);
                case 5:
                    return string.Concat(obj, args[0], args[1], args[2], args[3], args[4]);
                case 6:
                    return string.Concat(obj, args[0], args[1], args[2], args[3], args[4], args[5]);
                case 7:
                    return string.Concat(obj, args[0], args[1], args[2], args[3], args[4], args[5], args[6]);
                case 8:
                    return string.Concat(obj, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7]);
                case 9:
                    return string.Concat(obj, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8]);
                default:
                    var buffer = new StringBuilder(obj);
                    foreach (var arg in args)
                    {
                        buffer.Append(arg);
                    }

                    return buffer.ToString();
            }
        }

#if NET20
        public static string Append(string obj, 
                                    params string[] args)
#else
        public static string Append(this string obj, 
                                    params string[] args)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            if (null == args)
            {
                return obj;
            }

            switch (args.Length)
            {
                case 0:
                    return obj;
                case 1:
                    return string.Concat(obj, args[0]);
                case 2:
                    return string.Concat(obj, args[0], args[1]);
                case 3:
                    return string.Concat(obj, args[0], args[1], args[2]);
                case 4:
                    return string.Concat(obj, args[0], args[1], args[2], args[3]);
                case 5:
                    return string.Concat(obj, args[0], args[1], args[2], args[3], args[4]);
                case 6:
                    return string.Concat(obj, args[0], args[1], args[2], args[3], args[4], args[5]);
                case 7:
                    return string.Concat(obj, args[0], args[1], args[2], args[3], args[4], args[5], args[6]);
                case 8:
                    return string.Concat(obj, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7]);
                case 9:
                    return string.Concat(obj, args[0], args[1], args[2], args[3], args[4], args[5], args[6], args[7], args[8]);
                default:
                    var buffer = new StringBuilder(obj);
                    foreach (var arg in args)
                    {
                        buffer.Append(arg);
                    }

                    return buffer.ToString();
            }
        }

#if NET20
        public static string Caverphone(string obj)
#else
        public static string Caverphone(this string obj)
#endif
        {
            if (string.IsNullOrEmpty(obj))
            {
                return obj;
            }

            var buffer = new StringBuilder(CaverphoneStart(obj));

            CaverphoneEndings(buffer);
            buffer.Replace("cq", "2q");
            buffer.Replace("ci", "si");
            buffer.Replace("ce", "se");
            buffer.Replace("cy", "sy");
            buffer.Replace("tch", "2ch");
            buffer.Replace('c', 'k');
            buffer.Replace('q', 'k');
            buffer.Replace('x', 'k');
            buffer.Replace('v', 'f');
            buffer.Replace("dg", "2g");
            buffer.Replace("tio", "sio");
            buffer.Replace("tia", "sia");
            buffer.Replace('d', 't');
            buffer.Replace("ph", "fh");
            buffer.Replace('b', 'p');
            buffer.Replace("sh", "s2");
            buffer.Replace('z', 's');
#if NET20
            if (GenericExtensionMethods.In(buffer[0], 'a', 'e', 'i', 'o', 'u'))
#else
            if (buffer[0].In('a', 'e', 'i', 'o', 'u'))
#endif
            {
                buffer[0] = 'a';
            }

            for (var i = 1; i < buffer.Length; i++)
            {
#if NET20
                if (GenericExtensionMethods.In(buffer[i], 'a', 'e', 'i', 'o', 'u'))
#else
                if (buffer[i].In('a', 'e', 'i', 'o', 'u'))
#endif
                {
                    buffer[i] = '3';
                }
            }

            buffer.Replace("3gh3", "3kh3");
            buffer.Replace("gh", "22");
            buffer.Replace('g', 'k');

            for (var i = buffer.Length - 1; i > 0; i--)
            {
#if NET20
                if (!GenericExtensionMethods.In(buffer[i], 's', 't', 'p', 'k', 'f', 'm', 'n'))
#else
                if (!buffer[i].In('s', 't', 'p', 'k', 'f', 'm', 'n'))
#endif
                {
                    continue;
                }

                if (buffer[i] != buffer[i - 1])
                {
                    continue;
                }

                buffer.Remove(i, 1);
            }

            for (var i = 0; i < buffer.Length; i++)
            {
                switch (buffer[i])
                {
                    case 's':
                        buffer[i] = 'S';
                        break;
                    case 't':
                        buffer[i] = 'T';
                        break;
                    case 'p':
                        buffer[i] = 'P';
                        break;
                    case 'k':
                        buffer[i] = 'K';
                        break;
                    case 'f':
                        buffer[i] = 'F';
                        break;
                    case 'm':
                        buffer[i] = 'M';
                        break;
                    case 'n':
                        buffer[i] = 'N';
                        break;
                }
            }

            buffer.Replace("w3", "W3");
            buffer.Replace("wy", "Wy");
            buffer.Replace("wh3", "Wh3");
            buffer.Replace("why", "Why");
            buffer.Replace('w', '2');

            if ('h' == buffer[0])
            {
                buffer[0] = 'a';
            }

            for (var i = 1; i < buffer.Length; i++)
            {
                if ('h' == buffer[i])
                {
                    buffer[i] = '2';
                }
            }

            buffer.Replace("r3", "R3");
            buffer.Replace("ry", "Ry");
            buffer.Replace('r', '2');
            buffer.Replace("l3", "L3");
            buffer.Replace("ly", "Ly");
            buffer.Replace('l', '2');
            buffer.Replace('j', 'y');
            buffer.Replace("y3", "Y3");
            buffer.Replace('y', '2');

            for (var i = buffer.Length - 1; i > -1; i--)
            {
#if NET20
                if (GenericExtensionMethods.In(buffer[i], '2', '3'))
#else
                if (buffer[i].In('2', '3'))
#endif
                {
                    buffer.Remove(i, 1);
                }
            }

            return Append(buffer.ToString(), "111111").Substring(0, 6);
        }

#if NET20
        public static bool Contains(string obj, 
                                    string value, 
                                    StringComparison comparisonType)
#else
        public static bool Contains(this string obj, 
                                    string value, 
                                    StringComparison comparisonType)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            return -1 != obj.IndexOf(value, comparisonType);
        }

#if NET20
        public static bool ContainsAny(string obj, 
                                       params char[] args)
#else
        public static bool ContainsAny(this string obj, 
                                       params char[] args)
#endif
        {
            if (null == obj)
            {
                return false;
            }

            if (0 == obj.Length)
            {
                return false;
            }

            if (null == args)
            {
                return false;
            }

            if (0 == args.Length)
            {
                return false;
            }

#if NET20
            foreach (var arg in args)
            {
                if (-1 == obj.IndexOf(arg))
                {
                    continue;
                }

                return true;
            }

            return false;
#else
            return args.Any(arg => -1 != obj.IndexOf(arg));
#endif
        }

#if NET20
        public static bool ContainsAny(string obj, 
                                       StringComparison comparison, 
                                       params string[] args)
#else
        public static bool ContainsAny(this string obj, 
                                       StringComparison comparison, 
                                       params string[] args)
#endif
        {
            if (null == obj)
            {
                return false;
            }

            if (0 == obj.Length)
            {
                return false;
            }

            if (null == args)
            {
                return false;
            }

            if (0 == args.Length)
            {
                return false;
            }

#if NET20
            foreach (var arg in args)
            {
                if (-1 == obj.IndexOf(arg, comparison))
                {
                    continue;
                }

                return true;
            }

            return false;
#else
            return args.Any(arg => -1 != obj.IndexOf(arg, comparison));
#endif
        }

#if !NET20
        public static T DefaultOrFromString<T>(this string value, 
                                               Func<string, T> fromString)
        {
            if (null == fromString)
            {
                throw new ArgumentNullException("fromString");
            }

            return ReferenceEquals(null, value)
                       ? default(T)
                       : fromString(value);
        }

#endif

#if !NET20
        public static bool IsNullOrEmpty(this string obj)
        {
            return string.IsNullOrEmpty(obj);
        }

#endif

#if NET20
        public static bool IsNullOrWhiteSpace(string obj)
#else
        public static bool IsNullOrWhiteSpace(this string obj)
#endif
        {
#if NET20
            if (string.IsNullOrEmpty(obj))
            {
                return true;
            }

            foreach (var c in obj)
            {
                if (!' '.Equals(c))
                {
                    return false;
                }
            }

            return true;
#else
            return string.IsNullOrEmpty(obj) || obj.All(c => ' '.Equals(c));

#endif
        }

#if NET20
        public static bool EndsWithAny(string obj, 
                                       StringComparison comparison, 
                                       params string[] args)
#else
        public static bool EndsWithAny(this string obj, 
                                       StringComparison comparison, 
                                       params string[] args)
#endif
        {
            if (null == obj)
            {
                return false;
            }

            if (0 == obj.Length)
            {
                return false;
            }

            if (null == args)
            {
                return false;
            }

            if (0 == args.Length)
            {
                return false;
            }

#if NET20
            foreach (var arg in args)
            {
                if (string.IsNullOrEmpty(arg))
                {
                    continue;
                }

                if (obj.EndsWith(arg, comparison))
                {
                    return true;
                }
            }

            return false;
#else
            return args
                .Where(arg => !string.IsNullOrEmpty(arg))
                .Any(arg => obj.EndsWith(arg, comparison));
#endif
        }

#if NET20
        public static bool EqualsAny(string obj, 
                                     StringComparison comparison, 
                                     params string[] args)
#else
        public static bool EqualsAny(this string obj, 
                                     StringComparison comparison, 
                                     params string[] args)
#endif
        {
            if (null == obj)
            {
                return false;
            }

            if (0 == obj.Length)
            {
                return false;
            }

            if (null == args)
            {
                return false;
            }

            if (0 == args.Length)
            {
                return false;
            }

#if NET20
            foreach (var arg in args)
            {
                if (string.IsNullOrEmpty(arg))
                {
                    continue;
                }

                if (obj.Equals(arg, comparison))
                {
                    return true;
                }
            }

            return false;
#else
            return args
                .Where(arg => !string.IsNullOrEmpty(arg))
                .Any(arg => obj.Equals(arg, comparison));
#endif
        }

#if NET20
        public static string FormatWith(string obj, 
                                        params object[] args)
#else
        public static string FormatWith(this string obj, 
                                        params object[] args)
#endif
        {
            return string.Format(CultureInfo.InvariantCulture, obj, args);
        }

        [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", MessageId = "Body", Justification = "Space is not wasted.")]
#if NET20
        public static int LevenshteinDistance(string obj, 
                                              string comparand)
#else
        public static int LevenshteinDistance(this string obj, 
                                              string comparand)
#endif
        {
            if (string.IsNullOrEmpty(obj))
            {
                return null == comparand ? 0 : comparand.Length;
            }

            if (string.IsNullOrEmpty(comparand))
            {
                return obj.Length;
            }

            var n = obj.Length;
            var m = comparand.Length;
            var d = new int[n + 1, m + 1];

            for (var i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (var j = 0; j <= m; d[0, j] = j++)
            {
            }

            for (var i = 1; i <= n; i++)
            {
                for (var j = 1; j <= m; j++)
                {
                    var cost = (comparand[j - 1] == obj[i - 1]) ? 0 : 1;

                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), 
                        d[i - 1, j - 1] + cost);
                }
            }

            return d[n, m];
        }

        [Comment("See http://aspell.net/metaphone/")]
        [Comment("See http://aspell.net/metaphone/metaphone-kuhn.txt")]
#if NET20
        public static string Metaphone(string obj)
#else
        public static string Metaphone(this string obj)
#endif
        {
            if (string.IsNullOrEmpty(obj))
            {
                return obj;
            }

            var buffer = MetaphoneAlphabet(obj);

            MetaphoneFirstLetters(buffer);

            for (var i = 0; i < buffer.Length; i++)
            {
                MetaphoneDropDuplicates(i, buffer);
                MetaphoneLetterB(i, buffer);
                MetaphoneLetterC(i, buffer);
                MetaphoneLetterD(i, buffer);
                MetaphoneLetterG(i, buffer);
                MetaphoneLetterH(i, buffer);
                MetaphoneLetterS(i, buffer);
                MetaphoneLetterT(i, buffer);
                MetaphoneLetterW(i, buffer);
                MetaphoneLetterY(i, buffer);
                MetaphoneLetterVowel(i, buffer);
            }

            return MetaphoneEnd(buffer);
        }

#if NET20
        public static string NormalizeWhiteSpace(string obj)
#else
        public static string NormalizeWhiteSpace(this string obj)
#endif
        {
            if (null == obj)
            {
                return null;
            }

            var buffer = new StringBuilder();

            foreach (var c in obj)
            {
#if NET20
                buffer.Append(CharExtensionMethods.IsWhiteSpace(c) ? ' ' : c);
#else
                buffer.Append(c.IsWhiteSpace() ? ' ' : c);
#endif
            }

            return buffer.ToString();
        }

#if NET20
        public static string RemoveAny(string obj, 
                                       params char[] args)
#else
        public static string RemoveAny(this string obj, 
                                       params char[] args)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            if (null == args)
            {
                throw new ArgumentNullException("args");
            }

            if (0 == args.Length)
            {
                throw new ArgumentOutOfRangeException("args");
            }

#if NET20
            if (0 == obj.Length)
            {
                return string.Empty;
            }

            foreach (var arg in args)
            {
                obj = obj.Replace(arg.ToString(), string.Empty);
            }

            return obj;
#else
            return 0 == obj.Length
                       ? string.Empty
                       : args.Aggregate(obj, 
                                        (current, 
                                         arg) => current.Replace(arg.ToString(CultureInfo.InvariantCulture), string.Empty));
#endif
        }

#if NET20
        public static string RemoveAnyCurrencySymbols(string obj)
#else
        public static string RemoveAnyCurrencySymbols(this string obj)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            if (0 == obj.Length)
            {
                return string.Empty;
            }

            const string symbols = "¤₳฿₵¢₡₢₠$₫৳₯€ƒ₣₲₴₭ℳ₥₦₧₱₰£₹₨₪₸₮₩¥៛";
            var buffer = new StringBuilder(obj.Length);
#if NET20
            foreach (var c in obj)
            {
                if (-1 != symbols.IndexOf(c))
                {
                    continue;
                }
#else
            foreach (var c in obj.Where(c => -1 == symbols.IndexOf(c)))
            {
#endif
                buffer.Append(c);
            }

            return buffer.ToString();
        }

#if NET20
        public static string RemoveAnyDigits(string obj)
#else
        public static string RemoveAnyDigits(this string obj)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            if (0 == obj.Length)
            {
                return string.Empty;
            }

            var buffer = new StringBuilder(obj.Length);

#if NET20
            foreach (var c in obj)
            {
                if (char.IsDigit(c))
                {
                    continue;
                }
#else
            foreach (var c in obj.Where(c => !char.IsDigit(c)))
            {
#endif
                buffer.Append(c);
            }

            return buffer.ToString();
        }

#if NET20
        public static string RemoveAnyFractions(string obj)
#else
        public static string RemoveAnyFractions(this string obj)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            if (0 == obj.Length)
            {
                return string.Empty;
            }

            const string fractions = "½⅓¼⅕⅙⅛⅔⅖¾⅗⅜⅘⅚⅝⅞⁄";
            var buffer = new StringBuilder(obj.Length);
#if NET20
            foreach (var c in obj)
            {
                if (-1 != fractions.IndexOf(c))
                {
                    continue;
                }
#else
            foreach (var c in obj.Where(c => -1 == fractions.IndexOf(c)))
            {
#endif
                buffer.Append(c);
            }

            return buffer.ToString();
        }

#if NET20
        public static string RemoveAnyMathematicalSymbols(string obj)
#else
        public static string RemoveAnyMathematicalSymbols(this string obj)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            if (0 == obj.Length)
            {
                return string.Empty;
            }

            const string symbols = "∞%‰‱+−-±∓×÷⁄∣∤=≠<>≪≫≺∝⊗′″‴√#ℵℶ𝔠:!~≈≀◅▻⋉⋈∴∵■□∎▮‣⇒→⊃⇔↔¬˜∧∨⊕⊻∀∃≜≝≐≅≡{}∅∈∉⊆⊂⊇⊃∪∩∆∖→↦∘ℕNℤZℙPℚQℝRℂCℍHO∑∏∐Δδ∂∇′•∫∮πσ†T⊤⊥⊧⊢o";
            var buffer = new StringBuilder(obj.Length);
#if NET20
            foreach (var c in obj)
            {
                if (-1 != symbols.IndexOf(c))
                {
                    continue;
                }
#else
            foreach (var c in obj.Where(c => -1 == symbols.IndexOf(c)))
            {
#endif
                buffer.Append(c);
            }

            return buffer.ToString();
        }

#if NET20
        public static string RemoveAnyPunctuation(string obj)
#else
        public static string RemoveAnyPunctuation(this string obj)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            if (0 == obj.Length)
            {
                return string.Empty;
            }

            const string punctuation = "„“”‘’'\"‐‒–—―…!?.:;,[](){}⟨⟩«»/⁄";
            var buffer = new StringBuilder(obj.Length);
#if NET20
            foreach (var c in obj)
            {
                if (-1 != punctuation.IndexOf(c))
                {
                    continue;
                }
#else
            foreach (var c in obj.Where(c => -1 == punctuation.IndexOf(c)))
            {
#endif
                buffer.Append(c);
            }

            return buffer.ToString();
        }

#if NET20
        public static string RemoveAnyTypography(string obj)
#else
        public static string RemoveAnyTypography(this string obj)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            if (0 == obj.Length)
            {
                return string.Empty;
            }

            const string typography = "&@*\\•^†‡°〃¡¿#№ºª¶§~_¦|©®℗℠™⁂⊤⊥☞∴∵‽؟◊※⁀";
            var buffer = new StringBuilder(obj.Length);
#if NET20
            foreach (var c in obj)
            {
                if (-1 != typography.IndexOf(c))
                {
                    continue;
                }
#else
            foreach (var c in obj.Where(c => -1 == typography.IndexOf(c)))
            {
#endif
                buffer.Append(c);
            }

            return buffer.ToString();
        }

#if NET20
        public static string RemoveAnyWhiteSpace(string obj)
#else
        public static string RemoveAnyWhiteSpace(this string obj)
#endif
        {
            if (null == obj)
            {
                return null;
            }

            var buffer = new StringBuilder();

#if NET20
            foreach (var c in obj)
            {
                if (CharExtensionMethods.IsWhiteSpace(c))
                {
                    continue;
                }
#else
            foreach (var c in obj.Where(c => !c.IsWhiteSpace()))
            {
#endif
                buffer.Append(c);
            }

            return buffer.ToString();
        }

#if NET20
        public static string RemoveDefiniteArticle(string obj)
#else
        public static string RemoveDefiniteArticle(this string obj)
#endif
        {
            if (null == obj)
            {
                return null;
            }

            if (!obj.StartsWith("THE ", StringComparison.OrdinalIgnoreCase))
            {
                return obj;
            }

            return " " + RemoveFromStart(obj, "THE ", StringComparison.OrdinalIgnoreCase);
        }

#if NET20
        public static string RemoveFromEnd(string obj, 
                                           string value, 
                                           StringComparison comparisonType)
#else
        public static string RemoveFromEnd(this string obj, 
                                           string value, 
                                           StringComparison comparisonType)
#endif
        {
            if (string.IsNullOrEmpty(obj))
            {
                return obj;
            }

            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (0 == value.Length)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            return obj.EndsWith(value, comparisonType)
                       ? obj.Substring(0, obj.Length - value.Length)
                       : obj;
        }

#if NET20
        public static string RemoveFromStart(string obj, 
                                             string value, 
                                             StringComparison comparisonType)
#else
        public static string RemoveFromStart(this string obj, 
                                             string value, 
                                             StringComparison comparisonType)
#endif
        {
            if (string.IsNullOrEmpty(obj))
            {
                return obj;
            }

            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            if (0 == value.Length)
            {
                throw new ArgumentOutOfRangeException("value");
            }

            return obj.StartsWith(value, comparisonType)
                       ? obj.Substring(value.Length)
                       : obj;
        }

#if NET20
        public static string RemoveIllegalFileCharacters(string obj)
#else
        public static string RemoveIllegalFileCharacters(this string obj)
#endif
        {
            if (string.IsNullOrEmpty(obj))
            {
                return obj;
            }

            var buffer = new StringBuilder();
            foreach (int c in obj)
            {
                if (32 > c)
                {
                    // Control characters
                    continue;
                }

                switch (c)
                {
                    case 34: // "
                    case 42: // *
                    case 47: // /
                    case 58: // :
                    case 60: // <
                    case 62: // >
                    case 63: // ?
                    case 92: // \
                    case 124: // |
                    case 127: // DEL
                        break;

                    default:
                        buffer.Append((char)c);
                        break;
                }
            }

            return buffer.ToString();
        }

#if NET20
        public static string Replace(string obj, 
                                     string oldValue, 
                                     string newValue, 
                                     StringComparison comparisonType)
#else
        public static string Replace(this string obj, 
                                     string oldValue, 
                                     string newValue, 
                                     StringComparison comparisonType)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            if (null == oldValue)
            {
                throw new ArgumentNullException("oldValue");
            }

            if (0 == obj.Length)
            {
                return obj;
            }

            if (0 == oldValue.Length)
            {
                return obj;
            }

            var buffer = new StringBuilder();
            for (var i = 0; i < obj.Length; i++)
            {
                if (obj.Substring(i).StartsWith(oldValue, comparisonType))
                {
                    buffer.Append(newValue);
                    i += oldValue.Length - 1;
                    continue;
                }

                buffer.Append(obj[i]);
            }

            return buffer.ToString();
        }

#if NET20
        public static string ReplaceAllWith(string obj, 
                                            string newValue, 
                                            StringComparison comparisonType, 
                                            params string[] args)
#else
        public static string ReplaceAllWith(this string obj, 
                                            string newValue, 
                                            StringComparison comparisonType, 
                                            params string[] args)
#endif
        {
            if (string.IsNullOrEmpty(obj))
            {
                return obj;
            }

            if (null == newValue)
            {
                throw new ArgumentNullException("newValue");
            }

            if (null == args)
            {
                throw new ArgumentNullException("args");
            }

#if NET20
            foreach (var arg in args)
            {
                obj = Replace(obj, arg, newValue, comparisonType);
            }

            return obj;
#else
            return args.Aggregate(obj, 
                                  (x, 
                                   arg) => x.Replace(arg, newValue, comparisonType));
#endif
        }

#if NET20
        public static string ReplaceBeginning(string obj, 
                                              string newValue, 
                                              StringComparison comparisonType, 
                                              params string[] beginnings)
#else
        public static string ReplaceBeginning(this string obj, 
                                              string newValue, 
                                              StringComparison comparisonType, 
                                              params string[] beginnings)
#endif
        {
            if (string.IsNullOrEmpty(obj))
            {
                return obj;
            }

            if (null == newValue)
            {
                throw new ArgumentNullException("newValue");
            }

            if (null == beginnings)
            {
                throw new ArgumentNullException("beginnings");
            }

#if NET20
            foreach (var beginning in beginnings)
            {
                if (string.IsNullOrEmpty(beginning))
                {
                    continue;
                }

                if (!obj.StartsWith(beginning, comparisonType))
                {
                    continue;
                }
#else
            foreach (var beginning in beginnings
                .Where(beginning => !string.IsNullOrEmpty(beginning))
                .Where(beginning => obj.StartsWith(beginning, comparisonType)))
            {
#endif
                return newValue + RemoveFromStart(obj, beginning, comparisonType);
            }

            return obj;
        }

#if NET20
        public static string ReplaceEnding(string obj, 
                                           string newValue, 
                                           StringComparison comparisonType, 
                                           params string[] endings)
#else
        public static string ReplaceEnding(this string obj, 
                                           string newValue, 
                                           StringComparison comparisonType, 
                                           params string[] endings)
#endif
        {
            if (string.IsNullOrEmpty(obj))
            {
                return obj;
            }

            if (null == newValue)
            {
                throw new ArgumentNullException("newValue");
            }

            if (null == endings)
            {
                throw new ArgumentNullException("endings");
            }

#if NET20
            foreach (var ending in endings)
            {
                if (string.IsNullOrEmpty(ending))
                {
                    continue;
                }

                if (!obj.EndsWith(ending, comparisonType))
                {
                    continue;
                }
#else
            foreach (var ending in endings
                .Where(ending => !string.IsNullOrEmpty(ending))
                .Where(ending => obj.EndsWith(ending, comparisonType)))
            {
#endif
                return RemoveFromEnd(obj, ending, comparisonType) + newValue;
            }

            return obj;
        }

#if NET20
        public static bool SameLengthAs(string obj, 
                                        string value)
#else
        public static bool SameLengthAs(this string obj, 
                                        string value)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            if (null == value)
            {
                throw new ArgumentNullException("value");
            }

            return obj.Length == value.Length;
        }

#if NET20
        public static bool SameIndexesOfEach(string obj, 
                                             params char[] args)
#else
        public static bool SameIndexesOfEach(this string obj, 
                                             params char[] args)
#endif
        {
            if (null == args)
            {
                throw new ArgumentNullException("args");
            }

            if (0 == args.Length)
            {
                throw new ArgumentOutOfRangeException("args");
            }

#if NET20
            if (string.IsNullOrEmpty(obj))
            {
                return true;
            }

            foreach (var arg in args)
            {
                if (obj.IndexOf(arg) != obj.LastIndexOf(arg))
                {
                    return false;
                }
            }

            return true;
#else
            return string.IsNullOrEmpty(obj) || args.All(arg => obj.IndexOf(arg) == obj.LastIndexOf(arg));
#endif
        }

#if NET20
        public static string Soundex(string obj)
#else
        public static string Soundex(this string obj)
#endif
        {
            if (string.IsNullOrEmpty(obj))
            {
                return obj;
            }

            obj = RemoveAnyDigits(ToEnglishAlphabet(obj)).ToUpperInvariant();
            if (0 == obj.Length)
            {
                return string.Empty;
            }

            if (1 == obj.Length)
            {
                return Append(obj, "000");
            }

            var buffer = new StringBuilder();
            buffer.Append(obj[0]);

#if NET20
            var list = new List<char>(RemoveAny(obj, 'Y', 'H', 'W').ToCharArray());
#else
            var list = RemoveAny(obj, 'Y', 'H', 'W').ToList();
#endif
            for (var i = 0; i < list.Count; i++)
            {
                switch (list[i])
                {
                    case 'B':
                    case 'F':
                    case 'P':
                    case 'V':
                        list[i] = '1';
                        break;
                    case 'C':
                    case 'G':
                    case 'J':
                    case 'K':
                    case 'Q':
                    case 'S':
                    case 'X':
                    case 'Z':
                        list[i] = '2';
                        break;
                    case 'D':
                    case 'T':
                        list[i] = '3';
                        break;
                    case 'L':
                        list[i] = '4';
                        break;
                    case 'M':
                    case 'N':
                        list[i] = '5';
                        break;
                    case 'R':
                        list[i] = '6';
                        break;
                    default:
                        list[i] = ' ';
                        break;
                }
            }

            var current = ' ';
            for (var i = 0; i < list.Count; i++)
            {
                var c = list[i];
                if (current == c)
                {
                    continue;
                }

                current = c;
                if (' ' == c)
                {
                    continue;
                }

                if (0 == i)
                {
                    continue;
                }

                buffer.Append(c);
                if (4 == buffer.Length)
                {
                    return buffer.ToString();
                }
            }

            return Append(buffer.ToString(), new string('0', 4 - buffer.ToString().Length));
        }

#if NET20
        public static string[] Split(string obj, 
                                     char separator, 
                                     StringSplitOptions options)
#else
        public static string[] Split(this string obj, 
                                     char separator, 
                                     StringSplitOptions options)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            return obj.Split(
                new[]
                    {
                        separator
                    }, 
                options);
        }

#if NET20
        public static string[] Split(string obj, 
                                     string separator, 
                                     StringSplitOptions options)
#else
        public static string[] Split(this string obj, 
                                     string separator, 
                                     StringSplitOptions options)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            return obj.Split(
                new[]
                    {
                        separator
                    }, 
                options);
        }

#if NET20
        public static bool StartsOrEndsWith(string obj, 
                                            params char[] args)
#else
        public static bool StartsOrEndsWith(this string obj, 
                                            params char[] args)
#endif
        {
            if (null == args)
            {
                throw new ArgumentNullException("args");
            }

            if (0 == args.Length)
            {
                throw new ArgumentOutOfRangeException("args");
            }

#if NET20
            if (string.IsNullOrEmpty(obj))
            {
                return false;
            }

            foreach (var arg in args)
            {
                if (arg.Equals(obj[0]))
                {
                    return true;
                }

                if (arg.Equals(obj[obj.Length - 1]))
                {
                    return true;
                }
            }

            return false;
#else
            return !string.IsNullOrEmpty(obj) && args.Any(arg => arg.Equals(obj[0]) || arg.Equals(obj[obj.Length - 1]));
#endif
        }

#if NET20
        public static bool StartsWithAny(string obj, 
                                         StringComparison comparison, 
                                         params string[] args)
#else
        public static bool StartsWithAny(this string obj, 
                                         StringComparison comparison, 
                                         params string[] args)
#endif
        {
            if (null == obj)
            {
                return false;
            }

            if (0 == obj.Length)
            {
                return false;
            }

            if (null == args)
            {
                return false;
            }

            if (0 == args.Length)
            {
                return false;
            }

#if NET20
            foreach (var arg in args)
            {
                if (string.IsNullOrEmpty(arg))
                {
                    continue;
                }

                if (obj.StartsWith(arg, comparison))
                {
                    return true;
                }
            }

            return false;
#else
            return args
                .Where(arg => !string.IsNullOrEmpty(arg))
                .Any(arg => obj.StartsWith(arg, comparison));
#endif
        }

#if NET20
        public static T To<T>(string obj)
#else
        public static T To<T>(this string obj)
#endif
        {
            var type = typeof(T);
            if (type.IsGenericType &&
                type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return string.IsNullOrEmpty(obj)
                           ? default(T)
                           : To<T>(Nullable.GetUnderlyingType(type), obj);
            }

            return To<T>(type, obj);
        }

#if NET20
        public static T TryTo<T>(string obj)
#else
        public static T TryTo<T>(this string obj)
#endif
        {
            var type = typeof(T);
            if (type.IsGenericType &&
                type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return string.IsNullOrEmpty(obj)
                           ? default(T)
                           : TryTo<T>(Nullable.GetUnderlyingType(type), obj);
            }

            return TryTo<T>(type, obj);
        }

        [Comment("TODO: Extend alphabet, see http://en.wikipedia.org/wiki/Latin_alphabets")]
#if NET20
        public static string ToEnglishAlphabet(string obj)
#else
        public static string ToEnglishAlphabet(this string obj)
#endif
        {
            if (string.IsNullOrEmpty(obj))
            {
                return obj;
            }

            var buffer = new StringBuilder();
            foreach (var c in obj)
            {
#if NET20
                var value = CharExtensionMethods.ToEnglishAlphabet(c);
#else
                var value = c.ToEnglishAlphabet();
#endif
                buffer.Append(value.HasValue ? value.Value : c);
            }

            return buffer.ToString();
        }

#if NET20
        public static string ToEnglishSpacedAlphanumeric(string obj)
#else
        public static string ToEnglishSpacedAlphanumeric(this string obj)
#endif
        {
            if (null == obj)
            {
                return null;
            }

            if (0 == obj.Length)
            {
                return string.Empty;
            }

            obj = ToEnglishAlphabet(NormalizeWhiteSpace(obj));
            var buffer = new StringBuilder();

#if NET20
            foreach (var c in obj)
            {
                if (' ' != c && !char.IsLetterOrDigit(c))
                {
                    continue;
                }
#else
            foreach (var c in obj.Where(c => ' ' == c || char.IsLetterOrDigit(c)))
            {
#endif
                buffer.Append(c);
            }

            return buffer.ToString();
        }

        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "Title casing only works from lower case strings.")]
#if NET20
        public static string ToTitleCase(string obj)
#else
        public static string ToTitleCase(this string obj)
#endif
        {
            if (string.IsNullOrEmpty(obj))
            {
                return obj;
            }

            var info = Thread.CurrentThread.CurrentUICulture.TextInfo;

            return info.ToTitleCase(obj.ToLowerInvariant());
        }

#if NET20
        public static IXPathNavigable XmlDeserialize(string xml)
#else
        public static IXPathNavigable XmlDeserialize(this string xml)
#endif
        {
            var result = new XmlDocument();
            result.LoadXml(xml);

            return result;
        }

#if NET20
        public static T XmlDeserialize<T>(string xml)
#else
        public static T XmlDeserialize<T>(this string xml)
#endif
        {
            return (T)XmlDeserialize(xml, typeof(T));
        }

#if NET20
        public static object XmlDeserialize(string xml, 
                                            Type type)
#else
        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "This is an odd rule that seems to be impossible to actually pass.")]
        public static object XmlDeserialize(this string xml, 
                                            Type type)
#endif
        {
            if (null == xml)
            {
                throw new ArgumentNullException("xml");
            }

            if (0 == xml.Length)
            {
                throw new ArgumentOutOfRangeException("xml");
            }

            if (null == type)
            {
                throw new ArgumentNullException("type");
            }

            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(xml);
                    writer.Flush();
                    stream.Position = 0;
                    return typeof(Exception).IsAssignableFrom(type)
                               ? new SoapFormatter().Deserialize(stream)
                               : new XmlSerializer(type).Deserialize(stream);
                }
            }
        }

        [SuppressMessage("Microsoft.Globalization", "CA1308:NormalizeStringsToUppercase", Justification = "The algorithm specifies lower case.")]
        [Comment("If the name starts with cough make it cou2f")]
        [Comment("If the name starts with rough make it rou2f")]
        [Comment("If the name starts with tough make it tou2f")]
        [Comment("If the name starts with enough make it enou2f")]
        [Comment("If the name starts with gn make it 2n")]
        public static string CaverphoneStart(string value)
        {
            if (null == value)
            {
                return null;
            }

            if (0 == value.Length)
            {
                return value;
            }

            value = ToEnglishAlphabet(value).ToLowerInvariant();

            if (StartsWithAny(value, StringComparison.Ordinal, "cough", "rough", "tough"))
            {
                return string.Concat(value.Substring(0, 3), "2f", 5 == value.Length ? string.Empty : value.Substring(5));
            }

            if (value.StartsWith("enough", StringComparison.Ordinal))
            {
                return string.Concat(value.Substring(0, 4), "2f", 6 == value.Length ? string.Empty : value.Substring(6));
            }

            if (value.StartsWith("gn", StringComparison.Ordinal))
            {
                return string.Concat("2n", 2 == value.Length ? string.Empty : value.Substring(2));
            }

            return value;
        }

        [Comment("If the name ends with mb make it m2")]
        public static void CaverphoneEndings(StringBuilder buffer)
        {
            if (null == buffer)
            {
                throw new ArgumentNullException("buffer");
            }

            if (2 > buffer.Length)
            {
                return;
            }

            var last = buffer.Length - 1;
            var penultimate = last - 1;

            if ('m' != buffer[penultimate])
            {
                return;
            }

            if ('b' != buffer[last])
            {
                return;
            }

            buffer[last] = '2';
        }

        public static string MetaphoneEnd(StringBuilder buffer)
        {
            if (null == buffer)
            {
                throw new ArgumentNullException("buffer");
            }

            if (0 == buffer.Length)
            {
                return string.Empty;
            }

            buffer.Replace("CK", "K");
            buffer.Replace("PH", "F");
            buffer.Replace("X", "KS");

            for (var i = buffer.Length; i > 0; i--)
            {
                switch (buffer[i - 1])
                {
                    case ' ':
                        buffer.Remove(i - 1, 1);
                        break;

                    case 'j':
                        buffer[i - 1] = 'J';
                        break;
                    case 'k':
                        buffer[i - 1] = 'K';
                        break;
                    case 's':
                        buffer[i - 1] = 'S';
                        break;
                    case 't':
                        buffer[i - 1] = 'T';
                        break;
                    case 'x':
                        buffer[i - 1] = 'X';
                        break;

                    case 'V':
                        buffer[i - 1] = 'F';
                        break;
                    case 'Q':
                        buffer[i - 1] = 'K';
                        break;
                    case 'Z':
                        buffer[i - 1] = 'S';
                        break;
                }
            }

            return buffer.ToString();
        }

        public static void MetaphoneFirstLetters(StringBuilder buffer)
        {
            if (null == buffer)
            {
                throw new ArgumentNullException("buffer");
            }

            if (0 == buffer.Length)
            {
                return;
            }

            if ('X' == buffer[0])
            {
                //// X-
                buffer[0] = 'S';
                return;
            }

            if (1 == buffer.Length)
            {
                return;
            }

            if ('A' == buffer[0] && 'E' == buffer[1])
            {
                //// AE-
                buffer.Remove(0, 1);
                return;
            }

            if ('G' == buffer[0] && 'N' == buffer[1])
            {
                //// GN-
                buffer.Remove(0, 1);
                return;
            }

            if ('K' == buffer[0] && 'N' == buffer[1])
            {
                //// KN-
                buffer.Remove(0, 1);
                return;
            }

            if ('P' == buffer[0] && 'N' == buffer[1])
            {
                //// PN-
                buffer.Remove(0, 1);
                return;
            }

            if ('W' == buffer[0] && 'R' == buffer[1])
            {
                //// WR-
                buffer.Remove(0, 1);
            }
        }

        [Comment("Drop 'B' if after 'M' and if it is at the end of the word.")]
        public static void MetaphoneLetterB(int index, 
                                            StringBuilder buffer)
        {
            if (null == buffer)
            {
                throw new ArgumentNullException("buffer");
            }

            if (0 == index)
            {
                return;
            }

            if (int.MaxValue == index)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            if (int.MinValue == index)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            var previous = int.MinValue == index ? index : index - 1;
            var next = int.MaxValue == index ? index : index + 1;
            var last = buffer.Length - 1;

            if ('M' != buffer[previous])
            {
                return;
            }

            if ('B' != buffer[index])
            {
                return;
            }

            if (index == last)
            {
                //// -MB
                buffer[index] = ' ';
                return;
            }

            if (next != last)
            {
                return;
            }

            if ('E' != buffer[next])
            {
                return;
            }

            //// -MBE
            buffer[index] = ' ';
        }

        [Comment("'C' transforms to 'X' if followed by 'IA' or 'H' (unless in latter case, it is part of '-SCH-', in which case it transforms to 'K').")]
        [Comment("'C' transforms to 'S' if followed by 'I', 'E', or 'Y'.")]
        [Comment("Otherwise, 'C' transforms to 'K'.")]
        [Comment("'CK' transforms to 'K'.")]
        public static void MetaphoneLetterC(int index, 
                                            StringBuilder buffer)
        {
            if (null == buffer)
            {
                throw new ArgumentNullException("buffer");
            }

            if (int.MaxValue == index)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            if (int.MinValue == index)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            if ('C' != buffer[index])
            {
                return;
            }

            var previous = int.MinValue == index ? index : index - 1;
            var next = int.MaxValue == index ? index : index + 1;

            if (next == buffer.Length)
            {
                buffer[index] = 'k';
                return;
            }

            if ('I' == buffer[next])
            {
                if (next + 1 != buffer.Length &&
                    'A' == buffer[next + 1])
                {
                    //// -CIA-
                    buffer[index] = 'x';
                    buffer[next] = ' ';
                    buffer[next + 1] = ' ';
                    return;
                }

                //// -CI-
                buffer[index] = 's';
                buffer[next] = ' ';
                return;
            }

#if NET20
            if (GenericExtensionMethods.In(buffer[next], 'E', 'Y'))
#else
            if (buffer[next].In('E', 'Y'))
#endif
            {
                //// -CE-, -CY-
                buffer[index] = 's';
                buffer[next] = ' ';
                return;
            }

            if ('K' == buffer[next])
            {
                //// -CK-
                buffer[index] = 'k';
                buffer[next] = ' ';
                return;
            }

            if ('H' == buffer[next])
            {
                if (index == 0 || 'S' != buffer[previous])
                {
                    //// -CH-
                    buffer[index] = 'x';
                    buffer[next] = ' ';
                    return;
                }

                if (index != 0 || 'S' == buffer[previous])
                {
                    //// -SCH-
                    buffer[next] = ' ';
                }
            }

            buffer[index] = 'k';
        }

        [Comment("Drop 'G' if followed by 'H' and 'H' is not at the end or before a vowel.")]
        [Comment("Drop 'G' if followed by 'N' or 'NED' and is at the end.")]
        [Comment("'G' transforms to 'J' if before 'I', 'E', or 'Y', and it is not in 'GG'.")]
        [Comment("Otherwise, 'G' transforms to 'K'.")]
        public static void MetaphoneLetterG(int index, 
                                            StringBuilder buffer)
        {
            if (null == buffer)
            {
                throw new ArgumentNullException("buffer");
            }

            if (int.MaxValue == index)
            {
                throw new ArgumentOutOfRangeException("index");
            }

            if ('G' != buffer[index])
            {
                return;
            }

            var next = int.MaxValue == index ? index : index + 1;
            var last = buffer.Length - 1;

            if (next == last)
            {
                if ('N' == buffer[next])
                {
                    //// -GN
                    buffer[index] = ' ';
                    return;
                }
            }
            else if (next + 2 == last && 'N' == buffer[next] && 'E' == buffer[next + 1] && 'D' == buffer[next + 2])
            {
                //// -GNED
                buffer[index] = ' ';
                return;
            }

            if (next != buffer.Length)
            {
#if NET20
                if (GenericExtensionMethods.In(buffer[next], 'E', 'I', 'Y'))
#else
                if (buffer[next].In('E', 'I', 'Y'))
#endif
                {
                    //// -GE-, -GY-, -GI-
                    buffer[index] = 'j';
                    buffer[next] = ' ';
                    return;
                }

                if ('H' == buffer[next])
                {
                    if (next + 1 != buffer.Length &&
#if NET20
                        GenericExtensionMethods.In(buffer[next + 1], 'A', 'E', 'I', 'O', 'U'))
#else
                        buffer[next + 1].In('A', 'E', 'I', 'O', 'U'))
#endif
                    {
                        buffer[index] = 'k';
                        return;
                    }

                    //// -GH-
                    buffer[index] = ' ';
                    return;
                }
            }

            buffer[index] = 'k';
        }

        private static StringBuilder MetaphoneAlphabet(string value)
        {
            var buffer = new StringBuilder();
            const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ ";
            value = ToEnglishAlphabet(NormalizeWhiteSpace(value)).ToUpperInvariant();
#if NET20
            foreach (var c in value)
            {
                if (-1 == alphabet.IndexOf(c))
                {
                    continue;
                }
#else
            foreach (var c in value.Where(c => alphabet.Contains(c)))
            {
#endif
                buffer.Append(c);
            }

            return buffer;
        }

        [Comment("Drop duplicate adjacent letters, except for C.")]
        private static void MetaphoneDropDuplicates(int i, 
                                                    StringBuilder buffer)
        {
            if ('C' == buffer[i])
            {
                return;
            }

            var next = i + 1;

            if (next == buffer.Length)
            {
                return;
            }

            if (buffer[i] != buffer[next])
            {
                return;
            }

            buffer[next] = ' ';
        }

        [Comment("Drop all vowels unless it is the beginning.")]
        private static void MetaphoneLetterVowel(int i, 
                                                 StringBuilder buffer)
        {
            if (0 == i)
            {
                return;
            }

#if NET20
            if (GenericExtensionMethods.In(buffer[i], 'A', 'E', 'I', 'O', 'U'))
#else
            if (buffer[i].In('A', 'E', 'I', 'O', 'U'))
#endif
            {
                buffer[i] = ' ';
            }
        }

        [Comment("'D' transforms to 'J' if followed by 'GE', 'GY', or 'GI'.")]
        [Comment("Otherwise, 'D' transforms to 'T'.")]
        private static void MetaphoneLetterD(int i, 
                                             StringBuilder buffer)
        {
            if ('D' != buffer[i])
            {
                return;
            }

            var next = i + 1;
            var last = buffer.Length - 1;

            if (next + 1 <= last
                && 'G' == buffer[next])
            {
#if NET20
                if (GenericExtensionMethods.In(buffer[next + 1], 'E', 'I', 'Y'))
#else
                if (buffer[next + 1].In('E', 'I', 'Y'))
#endif
                {
                    //// -DGE-, -DGY-, -DGI-
                    buffer[i] = 'j';
                    buffer[next] = ' ';
                    buffer[next + 1] = ' ';
                    return;
                }
            }

            buffer[i] = 't';
        }

        [Comment("Drop 'H' if after vowel and not before a vowel.")]
        private static void MetaphoneLetterH(int i, 
                                             StringBuilder buffer)
        {
            if (0 == i)
            {
                return;
            }

            if ('H' != buffer[i])
            {
                return;
            }

            var previous = i - 1;
            var next = i + 1;

#if NET20
            if (!GenericExtensionMethods.In(buffer[previous], 'A', 'E', 'I', 'O', 'U'))
#else
            if (!buffer[previous].In('A', 'E', 'I', 'O', 'U'))
#endif
            {
                return;
            }

            if (next != buffer.Length
#if NET20
                && GenericExtensionMethods.In(buffer[next], 'A', 'E', 'I', 'O', 'U'))
#else
                && buffer[next].In('A', 'E', 'I', 'O', 'U'))
#endif
            {
                return;
            }

            buffer[i] = ' ';
        }

        [Comment("'S' transforms to 'X' if followed by 'H', 'IO', or 'IA'.")]
        private static void MetaphoneLetterS(int i, 
                                             StringBuilder buffer)
        {
            if ('S' != buffer[i])
            {
                return;
            }

            var next = i + 1;
            var last = buffer.Length - 1;

            if (next == buffer.Length)
            {
                return;
            }

            if ('H' == buffer[next])
            {
                //// -SH-
                buffer[i] = 'x';
                buffer[next] = ' ';
            }

            if (next == last)
            {
                return;
            }

            if ('I' != buffer[next])
            {
                return;
            }

#if NET20
            if (GenericExtensionMethods.In(buffer[next + 1], 'A', 'O'))
#else
            if (buffer[next + 1].In('A', 'O'))
#endif
            {
                //// -SIA-, -SIO-
                buffer[i] = 'x';
                buffer[next] = ' ';
                buffer[next + 1] = ' ';
            }
        }

        [Comment("'T' transforms to 'X' if followed by 'IA' or 'IO'.")]
        [Comment("'TH' transforms to '0'")]
        [Comment("Drop 'T' if followed by 'CH'.")]
        private static void MetaphoneLetterT(int i, 
                                             StringBuilder buffer)
        {
            if ('T' != buffer[i])
            {
                return;
            }

            var next = i + 1;
            var last = buffer.Length - 1;

            if (next == buffer.Length)
            {
                return;
            }

            if ('H' == buffer[next])
            {
                //// -TH-
                buffer[i] = '0';
                buffer[next] = ' ';
                return;
            }

            if (next == last)
            {
                return;
            }

            if ('C' == buffer[next] && 'H' == buffer[next + 1])
            {
                //// -TCH-
                buffer[i] = ' ';
                return;
            }

            if ('I' != buffer[next])
            {
                return;
            }

#if NET20
            if (GenericExtensionMethods.In(buffer[next + 1], 'A', 'O'))
#else
            if (buffer[next + 1].In('A', 'O'))
#endif
            {
                //// -TIA-, -TIO-
                buffer[i] = 'x';
                buffer[next] = ' ';
                buffer[next + 1] = ' ';
            }
        }

        [Comment("'WH' transforms to 'W' if at the beginning.")]
        [Comment("Drop 'W' if not followed by a vowel.")]
        private static void MetaphoneLetterW(int i, 
                                             StringBuilder buffer)
        {
            if ('W' != buffer[i])
            {
                return;
            }

            var next = i + 1;

            if (next != buffer.Length)
            {
                if (0 == i && 'H' == buffer[next])
                {
                    //// WH-
                    buffer[i] = 'W';
                    buffer[next] = ' ';
                    return;
                }

#if NET20
                if (GenericExtensionMethods.In(buffer[next], 'A', 'E', 'I', 'O', 'U'))
#else
                if (buffer[next].In('A', 'E', 'I', 'O', 'U'))
#endif
                {
                    return;
                }
            }

            buffer[i] = ' ';
        }

        [Comment("Drop 'Y' if not followed by a vowel.")]
        private static void MetaphoneLetterY(int i, 
                                             StringBuilder buffer)
        {
            if ('Y' != buffer[i])
            {
                return;
            }

            var next = i + 1;

            if (next != buffer.Length &&
#if NET20
                GenericExtensionMethods.In(buffer[next], 'A', 'E', 'I', 'O', 'U'))
#else
                buffer[next].In('A', 'E', 'I', 'O', 'U'))
#endif
            {
                return;
            }

            buffer[i] = ' ';
        }

#if NET20
        private static T To<T>(Type type, string obj)
#else
        private static T To<T>(this Type type, 
                               string obj)
#endif
        {
            object value;
            if (typeof(bool) == type)
            {
                value = XmlConvert.ToBoolean(obj);
            }
            else if (typeof(byte) == type)
            {
                value = XmlConvert.ToByte(obj);
            }
            else if (typeof(char) == type)
            {
                value = XmlConvert.ToChar(obj);
            }
            else if (typeof(DateTime) == type)
            {
                value = XmlConvert.ToDateTime(obj, XmlDateTimeSerializationMode.Utc);
            }

#if !NET20
            else if (typeof(DateTimeOffset) == type)
            {
                value = XmlConvert.ToDateTimeOffset(obj);
            }

#endif
            else if (typeof(decimal) == type)
            {
                value = XmlConvert.ToDecimal(obj);
            }
            else if (typeof(double) == type)
            {
                value = XmlConvert.ToDouble(obj);
            }
            else if (typeof(Guid) == type)
            {
                value = XmlConvert.ToGuid(obj);
            }
            else if (typeof(short) == type)
            {
                value = XmlConvert.ToInt16(obj);
            }
            else if (typeof(int) == type)
            {
                value = XmlConvert.ToInt32(obj);
            }
            else if (typeof(long) == type)
            {
                value = XmlConvert.ToInt64(obj);
            }
            else if (typeof(sbyte) == type)
            {
                value = XmlConvert.ToSByte(obj);
            }
            else if (typeof(float) == type)
            {
                value = XmlConvert.ToSingle(obj);
            }
            else if (typeof(TimeSpan) == type)
            {
                value = XmlConvert.ToTimeSpan(obj);
            }
            else if (typeof(ushort) == type)
            {
                value = XmlConvert.ToUInt16(obj);
            }
            else if (typeof(uint) == type)
            {
                value = XmlConvert.ToUInt32(obj);
            }
            else if (typeof(ulong) == type)
            {
                value = XmlConvert.ToUInt32(obj);
            }
            else
            {
                value = obj;
            }

            return (T)Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
        }

        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "This is due to the type-specific nature of parsing.")]
#if NET20
        private static T TryTo<T>(Type type, string obj)
#else
        private static T TryTo<T>(this Type type, 
                                  string obj)
#endif
        {
            if (typeof(bool) == type)
            {
                bool boolResult;
                return bool.TryParse(obj, out boolResult)
                           ? (T)Convert.ChangeType(boolResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

            if (typeof(byte) == type)
            {
                byte byteResult;
                return byte.TryParse(obj, NumberStyles.Any, CultureInfo.InvariantCulture, out byteResult)
                           ? (T)Convert.ChangeType(byteResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

            if (typeof(char) == type)
            {
                char charResult;
                return char.TryParse(obj, out charResult)
                           ? (T)Convert.ChangeType(charResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

            if (typeof(DateTime) == type)
            {
                DateTime dateTimeResult;
                return DateTime.TryParse(obj, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out dateTimeResult)
                           ? (T)Convert.ChangeType(dateTimeResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

#if !NET20
            if (typeof(DateTimeOffset) == type)
            {
                DateTimeOffset dateTimeOffsetResult;
                return DateTimeOffset.TryParse(obj, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTimeOffsetResult)
                           ? (T)Convert.ChangeType(dateTimeOffsetResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

#endif

            if (typeof(decimal) == type)
            {
                decimal decimalResult;
                return decimal.TryParse(obj, NumberStyles.Any, CultureInfo.InvariantCulture, out decimalResult)
                           ? (T)Convert.ChangeType(decimalResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

            if (typeof(double) == type)
            {
                double doubleResult;
                return double.TryParse(obj, NumberStyles.Any, CultureInfo.InvariantCulture, out doubleResult)
                           ? (T)Convert.ChangeType(doubleResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

#if !NET20 && !NET35
            if (typeof(Guid) == type)
            {
                Guid guidResult;
                return Guid.TryParse(obj, out guidResult)
                           ? (T)Convert.ChangeType(guidResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

#endif
            if (typeof(short) == type)
            {
                short shortResult;
                return short.TryParse(obj, NumberStyles.Any, CultureInfo.InvariantCulture, out shortResult)
                           ? (T)Convert.ChangeType(shortResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

            if (typeof(int) == type)
            {
                int intResult;
                return int.TryParse(obj, NumberStyles.Any, CultureInfo.InvariantCulture, out intResult)
                           ? (T)Convert.ChangeType(intResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

            if (typeof(long) == type)
            {
                long longResult;
                return long.TryParse(obj, NumberStyles.Any, CultureInfo.InvariantCulture, out longResult)
                           ? (T)Convert.ChangeType(longResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

            if (typeof(sbyte) == type)
            {
                sbyte sbyteResult;
                return sbyte.TryParse(obj, NumberStyles.Any, CultureInfo.InvariantCulture, out sbyteResult)
                           ? (T)Convert.ChangeType(sbyteResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

            if (typeof(float) == type)
            {
                float floatResult;
                return float.TryParse(obj, NumberStyles.Any, CultureInfo.InvariantCulture, out floatResult)
                           ? (T)Convert.ChangeType(floatResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

            if (typeof(string) == type)
            {
                return (T)Convert.ChangeType(obj, type, CultureInfo.InvariantCulture);
            }

#if !NET20 && !NET35
            if (typeof(TimeSpan) == type)
            {
                TimeSpan timeSpanResult;
                return TimeSpan.TryParse(obj, CultureInfo.InvariantCulture, out timeSpanResult)
                           ? (T)Convert.ChangeType(timeSpanResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

#endif
            if (typeof(ushort) == type)
            {
                ushort ushortResult;
                return ushort.TryParse(obj, NumberStyles.Any, CultureInfo.InvariantCulture, out ushortResult)
                           ? (T)Convert.ChangeType(ushortResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

            if (typeof(uint) == type)
            {
                uint uintResult;
                return uint.TryParse(obj, NumberStyles.Any, CultureInfo.InvariantCulture, out uintResult)
                           ? (T)Convert.ChangeType(uintResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

            if (typeof(ulong) == type)
            {
                ulong ulongResult;
                return ulong.TryParse(obj, NumberStyles.Any, CultureInfo.InvariantCulture, out ulongResult)
                           ? (T)Convert.ChangeType(ulongResult, type, CultureInfo.InvariantCulture)
                           : default(T);
            }

            return default(T);
        }
    }
}