namespace Cavity.Data
{
    using System;
    using System.Linq;
    using System.Reflection;

    public sealed class RepositoryExpectations<T> where T : new()
    {
        public void VerifyAll<TRepository>() where TRepository : IRepository<T>
        {
            VerifyAll<TRepository>(GetType().Assembly);
        }

        private static void VerifyAll<TRepository>(Assembly assembly) where TRepository : IRepository<T>
        {
            foreach (var type in assembly.GetTypes())
            {
                if (type.IsInterface)
                {
                    continue;
                }

                if (null == type.GetInterface(typeof(IVerifyRepository<T>).Name))
                {
                    continue;
                }

                var obj = Activator.CreateInstance(type.MakeGenericType(typeof(T))) as IVerifyRepository<T>;

                if (null == obj)
                {
                    continue;
                }

                obj.Verify(Activator.CreateInstance<TRepository>());
            }
        }
    }
}