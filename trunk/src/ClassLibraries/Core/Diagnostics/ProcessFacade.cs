namespace Cavity.Diagnostics
{
    using System;

    public static class ProcessFacade
    {
        [ThreadStatic]
        private static IProcess _mock;

        public static IProcess Current
        {
            get
            {
                return _mock ?? new StandardProcess();
            }
        }

        public static IProcess Mock
        {
            get
            {
                return _mock;
            }

            set
            {
                _mock = value;
            }
        }

        public static void Reset()
        {
            _mock = null;
        }
    }
}