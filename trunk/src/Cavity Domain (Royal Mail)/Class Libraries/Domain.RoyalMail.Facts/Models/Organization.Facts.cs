namespace Cavity.Models
{
    using System;
    using Xunit;

    public sealed class OrganizationFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Organization>()
                            .DerivesFrom<ComparableObject>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new Organization());
        }

        [Fact]
        public void op_ToString()
        {
            var expected = string.Empty;
            var actual = new Organization().ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString_whenDepartment()
        {
            var obj = new Organization
                          {
                              Department = "Accounts"
                          };

            var expected = "{0}Accounts".FormatWith(Environment.NewLine);
            var actual = obj.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString_whenName()
        {
            var obj = new Organization
                          {
                              Name = "Example"
                          };

            const string expected = "Example";
            var actual = obj.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString_whenNameAndDepartment()
        {
            var obj = new Organization
                          {
                              Name = "Example",
                              Department = "Accounts"
                          };

            var expected = "Example{0}Accounts".FormatWith(Environment.NewLine);
            var actual = obj.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Department()
        {
            Assert.NotNull(new PropertyExpectations<Organization>(x => x.Department)
                               .IsAutoProperty<string>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_Name()
        {
            Assert.NotNull(new PropertyExpectations<Organization>(x => x.Name)
                               .IsAutoProperty<string>()
                               .IsNotDecorated()
                               .Result);
        }
    }
}