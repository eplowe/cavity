namespace Cavity.Threading
{
    using System;

    public sealed class DerivedStandardTask : StandardTask
    {
        public DerivedStandardTask()
        {
        }

        public DerivedStandardTask(IThreadedObject instance)
        {
            Instance = instance;
        }

        public bool ThrowException { get; set; }

        private IThreadedObject Instance { get; set; }

        public override IThreadedObject CreateInstance()
        {
            if (ThrowException)
            {
                throw new NotSupportedException();
            }

            return Instance;
        }
    }
}