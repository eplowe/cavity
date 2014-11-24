namespace Cavity.Caching
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
#if !NET20
    using System.Linq;
#endif
    using System.Web;
    using System.Web.Caching;

#if NET20
    public sealed class WebCacheCollection : ICacheCollection
#else
    public sealed class WebCacheCollection : ICacheCollection35
#endif
    {
        public WebCacheCollection()
        {
            Cache = HttpRuntime.Cache;
        }

        public int Count
        {
            get
            {
                return Cache.Count;
            }
        }

        private Cache Cache { get; set; }

        public object this[string key]
        {
            get
            {
                return Cache[key];
            }

            set
            {
                Cache[key] = value;
            }
        }

        public void Add(string key,
                        object value)
        {
            Cache.Add(key, value, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
        }

        public void Add(string key,
                        object value,
                        DateTime absoluteExpiration)
        {
            Cache.Add(key, value, null, absoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
        }

        public void Add(string key,
                        object value,
                        TimeSpan slidingExpiration)
        {
            Cache.Add(key, value, null, Cache.NoAbsoluteExpiration, slidingExpiration, CacheItemPriority.Default, null);
        }

        public void Clear()
        {
            var enumerator = Cache.GetEnumerator();
            var keys = new List<string>();

            while (enumerator.MoveNext())
            {
                keys.Add(enumerator.Key.ToString());
            }

            foreach (var key in keys)
            {
                Cache.Remove(key);
            }
        }

        public bool ContainsKey(string key)
        {
            var enumerator = Cache.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (key == enumerator.Key.ToString())
                {
                    return true;
                }
            }

            return false;
        }

        public object Get(string key)
        {
            return Cache.Get(key);
        }

        public T Get<T>(string key)
        {
            return (T)Cache.Get(key);
        }

        public object Remove(string key)
        {
            return Cache.Remove(key);
        }

        public T Remove<T>(string key)
        {
            return (T)Cache.Remove(key);
        }

        public void Set(string key,
                        object value)
        {
            Cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
        }

        public void Set(string key,
                        object value,
                        DateTime absoluteExpiration)
        {
            Cache.Insert(key, value, null, absoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
        }

        public void Set(string key,
                        object value,
                        TimeSpan slidingExpiration)
        {
            Cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, slidingExpiration, CacheItemPriority.Default, null);
        }

#if !NET20
        public object Get(string key,
                          Func<object> add)
        {
            if (null == add)
            {
                throw new ArgumentNullException("add");
            }

            if (ContainsKey(key))
            {
                return Cache.Get(key);
            }

            var value = add.Invoke();
            Add(key, value);

            return value;
        }

        public object Get(string key,
                          Func<ICacheCollection, object> add)
        {
            if (null == add)
            {
                throw new ArgumentNullException("add");
            }

            return ContainsKey(key)
                       ? Cache.Get(key)
                       : add.Invoke(this);
        }

        public T Get<T>(string key,
                        Func<T> add)
        {
            if (null == add)
            {
                throw new ArgumentNullException("add");
            }

            if (ContainsKey(key))
            {
                return (T)Cache.Get(key);
            }

            var value = add.Invoke();
            Add(key, value);

            return value;
        }

        public T Get<T>(string key,
                        Func<ICacheCollection, T> add)
        {
            if (null == add)
            {
                throw new ArgumentNullException("add");
            }

            if (ContainsKey(key))
            {
                return (T)Cache.Get(key);
            }

            return add.Invoke(this);
        }
#endif

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<object> GetEnumerator()
        {
            var enumerator = Cache.GetEnumerator();
            var keys = new List<string>();

            while (enumerator.MoveNext())
            {
                keys.Add(enumerator.Key.ToString());
            }

#if NET20
            foreach (var key in keys)
            {
                yield return Cache[key];
            }
#else
            return keys.Select(key => Cache[key]).GetEnumerator();
#endif
        }
    }
}