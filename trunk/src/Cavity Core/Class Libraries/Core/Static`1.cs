namespace Cavity
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Static", Justification = "This name is intentional.")]
    public static class Static<T>
    {
        [ThreadStatic]
        private static T _instance;

        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "This design is intentional.")]
        public static T Instance
        {
            get
            {
                return _instance;
            }

            set
            {
                _instance = value;
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "This design is intentional.")]
        public static void Reset()
        {
            _instance = default(T);
        }
    }
}