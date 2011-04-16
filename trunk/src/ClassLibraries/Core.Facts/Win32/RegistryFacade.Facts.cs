namespace Cavity.Win32
{
    using System;
    using Xunit;

    public sealed class RegistryFacadeFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(RegistryFacade).IsStatic());
        }

        [Fact]
        public void op_GetValue_string_string()
        {
            const string keyName = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\ComputerName\ActiveComputerName";
            const string valueName = "ComputerName";

            var expected = Environment.MachineName;
            var actual = RegistryFacade.GetValue(keyName, valueName);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_GetValue_stringMissing_string_object()
        {
            var keyName = @"HKEY_LOCAL_MACHINE\" + Guid.NewGuid();

            string expected = null;
            var actual = RegistryFacade.GetValue(keyName, "Example", "abc");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_GetValue_stringMissing_string_object_whenFaked()
        {
            try
            {
                var keyName = @"HKEY_LOCAL_MACHINE\" + Guid.NewGuid();
                const string valueName = "ComputerName";

                RegistryFacade.Fake["foo"][valueName] = "example";

                string expected = null;
                var actual = RegistryFacade.GetValue(keyName, valueName, "abc");

                Assert.Equal(expected, actual);
            }
            finally
            {
                RegistryFacade.Reset();
            }
        }

        [Fact]
        public void op_GetValue_stringNull_string_object()
        {
            string keyName = null;
            const string valueName = "ComputerName";

            Assert.Throws<ArgumentNullException>(() => RegistryFacade.GetValue(keyName, valueName, null));
        }

        [Fact]
        public void op_GetValue_stringNull_string_object_whenFaked()
        {
            try
            {
                RegistryFacade.Fake["foo"]["bar"] = "example";

                Assert.Throws<ArgumentNullException>(() => RegistryFacade.GetValue(null, "Example", null));
            }
            finally
            {
                RegistryFacade.Reset();
            }
        }

        [Fact]
        public void op_GetValue_string_stringMissing_object()
        {
            const string keyName = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\ComputerName\ActiveComputerName";
            var valueName = Guid.NewGuid().ToString();

            const string expected = "abc";
            var actual = RegistryFacade.GetValue(keyName, valueName, expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_GetValue_string_stringMissing_object_whenFaked()
        {
            try
            {
                const string keyName = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\ComputerName\ActiveComputerName";
                var valueName = Guid.NewGuid().ToString();

                RegistryFacade.Fake[keyName]["bar"] = "example";

                const string expected = "abc";
                var actual = RegistryFacade.GetValue(keyName, valueName, expected);

                Assert.Equal(expected, actual);
            }
            finally
            {
                RegistryFacade.Reset();
            }
        }

        [Fact]
        public void op_GetValue_string_stringNull_object()
        {
            const string keyName = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\ComputerName\ActiveComputerName";
            string valueName = null;

            const string expected = "abc";
            var actual = RegistryFacade.GetValue(keyName, valueName, expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_GetValue_string_stringNull_object_whenFaked()
        {
            try
            {
                const string keyName = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\ComputerName\ActiveComputerName";
                string valueName = null;

                RegistryFacade.Fake[keyName]["bar"] = "example";

                const string expected = "abc";
                var actual = RegistryFacade.GetValue(keyName, valueName, expected);

                Assert.Equal(expected, actual);
            }
            finally
            {
                RegistryFacade.Reset();
            }
        }

        [Fact]
        public void op_GetValue_string_string_object()
        {
            const string keyName = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\ComputerName\ActiveComputerName";
            const string valueName = "ComputerName";

            var expected = Environment.MachineName;
            var actual = RegistryFacade.GetValue(keyName, valueName, null);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_GetValue_string_string_object_whenFaked()
        {
            try
            {
                const string keyName = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\ComputerName\ActiveComputerName";
                const string valueName = "ComputerName";

                const string expected = "FAKE";

                RegistryFacade.Fake[keyName][valueName] = expected;

                var actual = RegistryFacade.GetValue(keyName, valueName, null);

                Assert.Equal(expected, actual);
            }
            finally
            {
                RegistryFacade.Reset();
            }
        }

        [Fact]
        public void op_Reset()
        {
            const string keyName = @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\ComputerName\ActiveComputerName";
            const string valueName = "ComputerName";

            var expected = "FAKE";

            RegistryFacade.Fake[keyName][valueName] = expected;
            var actual = RegistryFacade.GetValue(keyName, valueName, null);

            Assert.Equal(expected, actual);

            RegistryFacade.Reset();

            expected = Environment.MachineName;
            actual = RegistryFacade.GetValue(keyName, valueName, null);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Fake()
        {
            try
            {
                Assert.NotNull(RegistryFacade.Fake);
            }
            finally
            {
                RegistryFacade.Reset();
            }
        }
    }
}