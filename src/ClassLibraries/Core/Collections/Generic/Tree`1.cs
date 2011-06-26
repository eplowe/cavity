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

        public Tree<T> Add(Tree<T> item)
        {
            if (null == item)
            {
                throw new ArgumentNullException("item");
            }

            item.Parent = this;
            Children.Add(item);

            return item;
        }

        public void Clear()
        {
            Children.Clear();
        }

        public bool Contains(Tree<T> item)
        {
            if (null == item)
            {
                throw new ArgumentNullException("item");
            }

            return Children.Contains(item);
        }

        public void Remove(Tree<T> item)
        {
            if (null == item)
            {
                throw new ArgumentNullException("item");
            }

            item.Parent = null;
            Children.Remove(item);
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