namespace Cavity.Collections.Generic
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
#if !NET20
    using System.Linq;
#endif
    using System.Text;

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This naming is intentional.")]
    public class Matrix<T> : IEnumerable<T>,
                             IEquatable<Matrix<T>>
    {
        public Matrix()
            : this(0, 0)
        {
        }

        public Matrix(int width, int height)
            : this(new Size(width, height))
        {
        }

        public Matrix(Size size)
        {
            if (!IsValid(size))
            {
                throw new ArgumentOutOfRangeException("size");
            }

            Size = size;
            Elements = new Dictionary<Point, T>();
        }

        public int Count
        {
            get
            {
                return Size.Height * Size.Width;
            }
        }

        public int Height
        {
            get
            {
                return Size.Height;
            }

            set
            {
                Resize(Size.Width, value);
            }
        }

        public bool IsEmpty
        {
            get
            {
                return 0 == Count;
            }
        }

        public bool IsSquare
        {
            get
            {
                return Size.Height == Size.Width;
            }
        }

        public int Width
        {
            get
            {
                return Size.Width;
            }

            set
            {
                Resize(value, Size.Height);
            }
        }

        private Dictionary<Point, T> Elements { get; set; }

        private Size Size { get; set; }

        [SuppressMessage("Microsoft.Design", "CA1023:IndexersShouldNotBeMultidimensional", Justification = "This design is intentional.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x", Justification = "This name is correct.")]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y", Justification = "This name is correct.")]
        public virtual T this[int x, int y]
        {
            get
            {
                return this[new Point(x, y)];
            }

            set
            {
                this[new Point(x, y)] = value;
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1043:UseIntegralOrStringArgumentForIndexers", Justification = "This design is intentional.")]
        public virtual T this[Point point]
        {
            get
            {
                if (!IsValid(point))
                {
                    throw new ArgumentOutOfRangeException("point");
                }

                return !Elements.ContainsKey(point)
                    ? default(T)
                    : Elements[point];
            }

            set
            {
                if (!IsValid(point))
                {
                    throw new ArgumentOutOfRangeException("point");
                }

                Elements[point] = value;
            }
        }

        public static bool operator ==(Matrix<T> obj,
                                       Matrix<T> comparand)
        {
            return ReferenceEquals(null, obj)
                       ? ReferenceEquals(null, comparand)
                       : obj.Equals(comparand);
        }

        public static bool operator !=(Matrix<T> obj,
                                       Matrix<T> comparand)
        {
            return ReferenceEquals(null, obj)
                       ? !ReferenceEquals(null, comparand)
                       : !obj.Equals(comparand);
        }

        public virtual void Clear()
        {
            Elements.Clear();
        }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "x", Justification = "This name is correct.")]
        public virtual IEnumerable<T> Column(int x)
        {
            if (!IsValid(new Point(x, 0)))
            {
                throw new ArgumentOutOfRangeException("x");
            }

            for (var y = 0; y < Size.Height; y++)
            {
                yield return this[x, y];
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var cast = obj as Matrix<T>;
            if (ReferenceEquals(null, cast))
            {
                return false;
            }

            return ToString() == cast.ToString();
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var y = 0; y < Size.Height; y++)
            {
                foreach (var element in Row(y))
                {
                    yield return element;
                }
            }
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public virtual void Resize(int width, int height)
        {
            Resize(new Size(width, height));
        }

        public virtual void Resize(Size size)
        {
            if (!IsValid(size))
            {
                throw new ArgumentOutOfRangeException("size");
            }

#if NET20
            var keys = new List<Point>();
            foreach (var element in Elements)
            {
                if (element.Key.Y >= size.Height ||
                    element.Key.X >= size.Width)
                {
                    keys.Add(element.Key);
                }
            }
#else
            var keys = (from element in Elements
                        where element.Key.Y >= size.Height || element.Key.X >= size.Width
                        select element.Key).ToList();
#endif

            foreach (var key in keys)
            {
                Elements.Remove(key);
            }

            Size = size;
        }

        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "y", Justification = "This name is correct.")]
        public virtual IEnumerable<T> Row(int y)
        {
            if (!IsValid(new Point(0, y)))
            {
                throw new ArgumentOutOfRangeException("y");
            }

            for (var x = 0; x < Size.Width; x++)
            {
                yield return this[x, y];
            }
        }

        public bool Equals(Matrix<T> other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return ReferenceEquals(this, other) || ToString() == other.ToString();
        }

        public override string ToString()
        {
            var lines = new List<MutableString>();
            for (var y = 0; y < Size.Height; y++)
            {
                lines.Add(new MutableString());
            }

            for (var x = 0; x < Size.Width; x++)
            {
                for (var y = 0; y < Size.Height; y++)
                {
                    if (0 != x)
                    {
                        lines[y].Append('\t');
                    }

                    var element = this[x, y];
                    lines[y].Append(ReferenceEquals(null, element) ? string.Empty : element.ToString());
                }

#if NET20
                var max = 0;
                foreach (var line in lines)
                {
                    max = Math.Max(max, line.Length);
                }

                foreach (var line in lines)
                {
                    if (line.Length != max)
                    {
                        line.Append(new string(' ', max - line.Length));
                    }
                }
#else
                var max = lines.Select(line => line.Length).Concat(new[] { 0 }).Max();
                foreach (var line in lines.Where(line => line.Length != max))
                {
                    line.Append(new string(' ', max - line.Length));
                }
#endif
            }

            var buffer = new StringBuilder();
            foreach (var line in lines)
            {
                buffer.AppendLine(line);
            }

            return buffer.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private static bool IsValid(Size size)
        {
            return 0 <= size.Height && 0 <= size.Width;
        }

        private bool IsValid(Point point)
        {
            if (0 > point.X || 0 > point.Y)
            {
                return false;
            }

            return Size.Height > point.Y && Size.Width > point.X;
        }
    }
}