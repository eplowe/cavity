namespace Cavity.Data
{
    using System;
    using Cavity;
    using Xunit;

    public sealed class JsonSerializationSettingsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<JsonSerializationSettings>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new JsonSerializationSettings());
        }

        [Fact]
        public void prop_ItemsName()
        {
            Assert.True(new PropertyExpectations<JsonSerializationSettings>(x => x.ItemsName)
                            .IsNotDecorated()
                            .TypeIs<string>()
                            .DefaultValueIs(string.Empty)
                            .ArgumentNullException()
                            .Result);
        }

    }
}