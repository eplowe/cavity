namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
#if !NET20
    using System.Linq;
#endif
    using System.Reflection;

    using Cavity.Properties;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using Xunit.Extensions;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class JsonDataAttribute : DataAttribute
    {
        public JsonDataAttribute(params string[] values)
            : this()
        {
            if (null == values)
            {
                throw new ArgumentNullException("values");
            }

            if (0 == values.Length)
            {
                throw new ArgumentOutOfRangeException("values");
            }

            Values = values;
        }

        private JsonDataAttribute()
        {
        }

        public IEnumerable<string> Values { get; private set; }

        public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, 
                                                      Type[] parameterTypes)
        {
            if (null == methodUnderTest)
            {
                throw new ArgumentNullException("methodUnderTest");
            }

            if (null == parameterTypes)
            {
                throw new ArgumentNullException("parameterTypes");
            }

#if NET20
            if (Cavity.Collections.IEnumerableExtensionMethods.Count(Values) != parameterTypes.Length)
            {
                throw new InvalidOperationException(StringExtensionMethods.FormatWith(Resources.Attribute_CountsDiffer, Cavity.Collections.IEnumerableExtensionMethods.Count(Values), parameterTypes.Length));
            }
#else
            if (Values.Count() != parameterTypes.Length)
            {
                throw new InvalidOperationException(Resources.Attribute_CountsDiffer.FormatWith(Values.Count(), parameterTypes.Length));
            }
#endif

            var list = new List<object>();
            var index = -1;
            foreach (var value in Values)
            {
                index++;
                if (parameterTypes[index] == typeof(JObject))
                {
                    list.Add(JObject.Parse(value));
                    continue;
                }

                list.Add(JsonConvert.DeserializeObject(value, parameterTypes[index]));
            }

            yield return list.ToArray();
        }
    }
}