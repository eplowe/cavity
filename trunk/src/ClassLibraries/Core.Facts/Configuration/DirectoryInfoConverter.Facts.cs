namespace Cavity.Configuration
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using Cavity.IO;
    using Moq;
    using Xunit;

    public sealed class DirectoryInfoConverterFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<DirectoryInfoConverter>()
                            .DerivesFrom<TypeConverter>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new DirectoryInfoConverter());
        }

        [Fact]
        public void op_CanConvertFrom_ITypeDescriptorContextNull_TypeString()
        {
            ITypeDescriptorContext context = null;
            var sourceType = typeof(string);

            Assert.True(new DirectoryInfoConverter().CanConvertFrom(context, sourceType));
        }

        [Fact]
        public void op_CanConvertFrom_ITypeDescriptorContext_TypeInt()
        {
            var context = new Mock<ITypeDescriptorContext>().Object;
            var sourceType = typeof(int);

            Assert.False(new DirectoryInfoConverter().CanConvertFrom(context, sourceType));
        }

        [Fact]
        public void op_CanConvertFrom_ITypeDescriptorContext_TypeString()
        {
            var context = new Mock<ITypeDescriptorContext>().Object;
            var sourceType = typeof(string);

            Assert.True(new DirectoryInfoConverter().CanConvertFrom(context, sourceType));
        }

        [Fact]
        public void op_ConvertFrom_ITypeDescriptorContext_CultureInfo_object()
        {
            using (var expected = new TempDirectory())
            {
                var context = new Mock<ITypeDescriptorContext>().Object;
                var culture = CultureInfo.InvariantCulture;
                var value = (object)expected.Info.FullName;

                var actual = (DirectoryInfo)new DirectoryInfoConverter().ConvertFrom(context, culture, value);
                if (null == actual)
                {
                    Assert.NotNull(actual);
                }
                else
                {
                    Assert.Equal(expected.Info.FullName, actual.FullName);
                }
            }
        }

        [Fact]
        public void op_ConvertFrom_ITypeDescriptorContext_CultureInfo_objectInt()
        {
            var context = new Mock<ITypeDescriptorContext>().Object;
            var culture = CultureInfo.InvariantCulture;
            var value = (object)123;

            Assert.Throws<NotSupportedException>(() => new DirectoryInfoConverter().ConvertFrom(context, culture, value));
        }

        [Fact]
        public void op_ConvertTo_ITypeDescriptorContext_CultureInfo_object_Type()
        {
            var context = new Mock<ITypeDescriptorContext>().Object;
            var culture = CultureInfo.InvariantCulture;
            var value = (object)123;
            var destinationType = typeof(string);

            Assert.Throws<NotSupportedException>(() => new DirectoryInfoConverter().ConvertTo(context, culture, value, destinationType));
        }
    }
}