namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class UserCategory
    {
        private static readonly HashSet<IUserCategory> _categories = new HashSet<IUserCategory>(LoadUserCategories());

        public static IUserCategory Resolve(char code)
        {
            return _categories
                .Where(x => x.Code.Equals(code))
                .FirstOrDefault();
        }

        private static IEnumerable<IUserCategory> LoadUserCategories()
        {
            var category = typeof(IUserCategory);
            return AppDomain
                .CurrentDomain
                .GetAssemblies()
                .ToList()
                .SelectMany(x => x.GetTypes())
                .Where(x => !x.IsInterface)
                .Where(category.IsAssignableFrom).Select(type => (IUserCategory)Activator.CreateInstance(type));
        }
    }
}