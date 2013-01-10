namespace Cavity
{
    using System;

    using Xunit;

    public sealed class UserStoryAttributeFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<UserStoryAttribute>().DerivesFrom<Attribute>()
                                                                  .IsConcreteClass()
                                                                  .IsSealed()
                                                                  .NoDefaultConstructor()
                                                                  .AttributeUsage(AttributeTargets.Method, false, false)
                                                                  .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new UserStoryAttribute());
        }

        [Fact]
        public void op_ToString()
        {
            var story = new UserStoryAttribute
                            {
                                AsA = "tester", 
                                IWant = "to add an attribute", 
                                SoThat = "I can see the user story"
                            };
            const string expected = "As a tester, I want to add an attribute so that I can see the user story.";
            var actual = story.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_AsA()
        {
            Assert.True(new PropertyExpectations<UserStoryAttribute>(x => x.AsA)
                            .IsAutoProperty<string>()
                            .Result);
        }

        [Fact]
        public void prop_IWant()
        {
            Assert.True(new PropertyExpectations<UserStoryAttribute>(x => x.IWant)
                            .IsAutoProperty<string>()
                            .Result);
        }

        [Fact]
        public void prop_SoThat()
        {
            Assert.True(new PropertyExpectations<UserStoryAttribute>(x => x.SoThat)
                            .IsAutoProperty<string>()
                            .Result);
        }

        [UserStory(AsA = "tester", IWant = "to add an attribute", SoThat = "I can see the user story")]
        public void usage()
        {
        }
    }
}