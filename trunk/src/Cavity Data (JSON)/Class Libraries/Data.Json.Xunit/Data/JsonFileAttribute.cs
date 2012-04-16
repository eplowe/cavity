namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
#if !NET20
    using System.Linq;
#endif
    using System.Reflection;

    using Cavity.IO;
    using Cavity.Properties;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using Xunit.Extensions;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class JsonFileAttribute : DataAttribute
    {
        public JsonFileAttribute(params string[] files)
            : this()
        {
            if (null == files)
            {
                throw new ArgumentNullException("files");
            }

            if (0 == files.Length)
            {
                throw new ArgumentOutOfRangeException("files");
            }

            Files = files;
        }

        private JsonFileAttribute()
        {
        }

        public IEnumerable<string> Files { get; private set; }

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
            if (Cavity.Collections.IEnumerableExtensionMethods.Count(Files) != parameterTypes.Length)
            {
                throw new InvalidOperationException(StringExtensionMethods.FormatWith(Resources.Attribute_CountsDiffer, Cavity.Collections.IEnumerableExtensionMethods.Count(Files), parameterTypes.Length));
            }
#else
            if (Files.Count() != parameterTypes.Length)
            {
                throw new InvalidOperationException(Resources.Attribute_CountsDiffer.FormatWith(Files.Count(), parameterTypes.Length));
            }
#endif

            var list = new List<object>();
            var index = -1;
            foreach (var file in Files)
            {
                var info = new FileInfo(file);
#if NET20
                var value = FileInfoExtensionMethods.ReadToEnd(info);
#else
                var value = info.ReadToEnd();
#endif
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