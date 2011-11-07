namespace Cavity.Globalization
{
    using System;
    using System.Globalization;
    using System.Threading;

    public static class CultureInfoExtensionMethods
    {
#if NET20
        public static void SetCurrentCulture(CultureInfo language)
#else
        public static void SetCurrentCulture(this CultureInfo language)
#endif
        {
            if (null == language)
            {
                throw new ArgumentNullException("language");
            }
            
            if (CultureInfo.InvariantCulture == language)
            {
                throw new ArgumentOutOfRangeException("language");
            }

            Thread.CurrentThread.CurrentUICulture = language;
        }
    }
}