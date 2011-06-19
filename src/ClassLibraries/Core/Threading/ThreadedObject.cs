namespace Cavity.Threading
{
    using System.Threading;

    public abstract class ThreadedObject : DisposableObject, IThreadedObject
    {
        private CancellationToken _token;

        public CancellationToken CancellationToken
        {
            get
            {
                return _token;
            }

            set
            {
                _token = value;
                _token.Register(Dispose);
            }
        }
    }
}