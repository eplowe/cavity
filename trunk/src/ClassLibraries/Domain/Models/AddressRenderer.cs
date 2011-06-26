namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Cavity.Collections;

    public sealed class AddressRenderer : IRenderAddress
    {
        private static readonly IRenderAddress _default = new AddressRenderer();

        private AddressRenderer()
        {
        }

        public static IRenderAddress Default
        {
            get
            {
                return _default;
            }
        }

        public string ToString(IEnumerable<IAddressLine> address)
        {
            if (null == address)
            {
                throw new ArgumentNullException("address");
            }

#if NET20
            if (IEnumerableExtensionMethods.IsEmpty(address))
#else
            if (address.IsEmpty())
#endif
            {
                return string.Empty;
            }

            var buffer = new StringBuilder();
#if NET20
            foreach (var line in IEnumerableExtensionMethods.ToStack(address))
#else
            foreach (var line in address.ToStack())
#endif
            {
                if (0 != buffer.Length)
                {
                    buffer.Append(", ");
                }

                buffer.Append(line.Original);
            }

            return buffer.ToString();
        }
    }
}