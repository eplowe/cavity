namespace Cavity.Win32
{
    using System;
    using Microsoft.Win32;

    public static class RegistryFacade
    {
        [ThreadStatic]
        private static FakeRegistry _fake;

        public static FakeRegistry Fake
        {
            get
            {
                _fake = _fake ?? new FakeRegistry();

                return _fake;
            }
        }

        public static object GetValue(string keyName,
                                      string valueName)
        {
            return GetValue(keyName, valueName, null);
        }

        public static object GetValue(string keyName,
                                      string valueName,
                                      object defaultValue)
        {
            if (null == _fake)
            {
                return Registry.GetValue(keyName, valueName, defaultValue);
            }

            if (!_fake.ContainsKey(keyName))
            {
                return null;
            }

            if (null == valueName)
            {
                return defaultValue;
            }

            return _fake[keyName].ContainsKey(valueName)
                       ? _fake[keyName][valueName]
                       : defaultValue;
        }

        public static void Reset()
        {
            _fake = null;
        }
    }
}