namespace Cavity.Data
{
    using System;
    using System.Linq;
    using System.Reflection;

    public sealed class RepositoryExpectations<T> where T : IRepository, new()
    {
        public void VerifyAll()
        {
            VerifyAll(GetType().Assembly);
        }

        private static void VerifyAll(Assembly assembly)
        {
            foreach (var expectation in from type in assembly.GetTypes()
                                        where !type.IsInterface
                                        where typeof(IExpectRepository).IsAssignableFrom(type)
                                        select (IExpectRepository)Activator.CreateInstance(type))
            {
                expectation.Verify(Activator.CreateInstance<T>());
            }
        }
    }
}