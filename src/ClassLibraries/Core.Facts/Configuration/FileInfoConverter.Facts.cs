namespace Cavity.Configuration
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.IO;
    using Cavity.IO;
    using Moq;
    using Xunit;

    public sealed class FileInfoConverterFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<FileInfoConverter>()
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
            Assert.NotNull(new FileInfoConverter());
        }

        [Fact]
        public void op_CanConvertFrom_ITypeDescriptorContextNull_TypeString()
        {
            ITypeDescriptorContext context = null;
            var sourceType = typeof(string);

            Assert.True(new FileInfoConverter().CanConvertFrom(context, sourceType));
        }

        [Fact]
        public void op_CanConvertFrom_ITypeDescriptorContext_TypeInt()
        {
            var context = new Mock<ITypeDescriptorContext>().Object;
            var sourceType = typeof(int);

            Assert.False(new FileInfoConverter().CanConvertFrom(context, sourceType));
        }

        [Fact]
        public void op_CanConvertFrom_ITypeDescriptorContext_TypeString()
        {
            var context = new Mock<ITypeDescriptorContext>().Object;
            var sourceType = typeof(string);

            Assert.True(new FileInfoConverter().CanConvertFrom(context, sourceType));
        }

        [Fact]
        public void op_ConvertFrom_ITypeDescriptorContext_CultureInfo_object()
        {
            using (var expected = new TempFile())
            {
                var context = new Mock<ITypeDescriptorContext>().Object;
                var culture = CultureInfo.InvariantCulture;
                var value = (object)expected.Info.FullName;

                var actual = (FileInfo)new FileInfoConverter().ConvertFrom(context, culture, value);
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

            Assert.Throws<NotSupportedException>(() => new FileInfoConverter().ConvertFrom(context, culture, value));
        }

        [Fact]
        public void op_ConvertTo_ITypeDescriptorContext_CultureInfo_object_Type()
        {
            var context = new Mock<ITypeDescriptorContext>().Object;
            var culture = CultureInfo.InvariantCulture;
            var value = (object)123;
            var destinationType = typeof(string);

            var actual = new FileInfoConverter().ConvertTo(context, culture, value, destinationType);
            
            Assert.Equal("123", actual);
        }
    }
}