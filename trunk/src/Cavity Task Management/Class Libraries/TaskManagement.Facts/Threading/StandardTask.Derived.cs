namespace Cavity.Threading
{
    using System;

    public sealed class DerivedStandardTask : StandardTask
    {
        public override void Run()
        {
        }

        protected override void OnDispose()
        {
        }
    }
}