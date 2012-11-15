namespace Cavity
{
    using System;
    using System.Collections.Generic;

    public static class Serial
    {
        public static void ForEach<T>(IEnumerable<T> source,
                                      Action<T> body)
        {
            if (null == source)
            {
                throw new ArgumentNullException("source");
            }

            if (null == body)
            {
                throw new ArgumentNullException("body");
            }

            foreach (var item in source)
            {
                body.Invoke(item);
            }
        }

        public static void Invoke(params Action[] actions)
        {
            if (null == actions)
            {
                throw new ArgumentNullException("actions");
            }

            foreach (var action in actions)
            {
                action.Invoke();
            }
        }
    }
}