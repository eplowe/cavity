namespace Cavity.Collections.Generic
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This naming is intentional.")]
    public class Tree<T> : IEnumerable<Tree<T>>
    {
        public Tree()
        {
            Children = new Collection<Tree<T>>();
        }

        public Tree(T value)
            : this()
        {
            Value = value;
        }

        public int Count
        {
            get
            {
                return Children.Count;
            }
        }

        public Tree<T> Parent { get; protected set; }

        public T Value { get; set; }

        private Collection<Tree<T>> Children { get; set; }

        public virtual Tree<T> Add(T child)
        {
            return Add(new Tree<T>(child));
        }

        public virtual Tree<T> Add(Tree<T> child)
        {
            if (null == child)
            {
                throw new ArgumentNullException("child");
            }

            child.Parent = this;
            Children.Add(child);

            return child;
        }

        public virtual void Clear()
        {
            Children.Clear();
        }

        public virtual bool Contains(Tree<T> child)
        {
            if (null == child)
            {
                throw new ArgumentNullException("child");
            }

            return Children.Contains(child);
        }

        public virtual void Remove(Tree<T> child)
        {
            if (null == child)
            {
                throw new ArgumentNullException("child");
            }

            child.Parent = null;
            Children.Remove(child);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Tree<T>> GetEnumerator()
        {
            return Children.GetEnumerator();
        }
    }
}