﻿namespace Cavity.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
#if !NET20
    using System.Linq;
#endif

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This naming is intentional.")]
    public class Address : IEnumerable<IAddressLine>
    {
        public Address()
        {
            Lines = new Collection<IAddressLine>();
        }

        protected Collection<IAddressLine> Lines { get; private set; }

        public virtual void Add(IAddressLine item)
        {
            if (null == item)
            {
                throw new ArgumentNullException("item");
            }

            Lines.Add(item);
        }

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "This design is intentional.")]
        public virtual IAddressLine Line<T>()
        {
#if NET20
            foreach (var line in Lines)
            {
                if (typeof(T).Equals(line.GetType()))
                {
                    return line;
                }
            }

            return null;
#else
            return Lines.FirstOrDefault(line => typeof(T).Equals(line.GetType()));
#endif
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<IAddressLine> GetEnumerator()
        {
            return Lines.GetEnumerator();
        }
    }
}