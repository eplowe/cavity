namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
#if !NET20
    using System.Linq;
#endif
    using System.Reflection;

    using Cavity.Collections;
    using Cavity.Properties;

    using Xunit.Extensions;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class CsvFileAttribute : DataAttribute
    {
        public CsvFileAttribute(params string[] files)
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

        private CsvFileAttribute()
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

            var list = new List<object>();
            if (1 == parameterTypes.Length && parameterTypes[0] == typeof(DataSet))
            {
                var data = new DataSet
                               {
                                   Locale = CultureInfo.InvariantCulture
                               };
                foreach (var file in Files)
                {
                    data.Tables.Add(new CsvFile(file).ToDataTable());
                }

                list.Add(data);
            }
            else
            {
#if NET20
                if (IEnumerableExtensionMethods.Count(Files) != parameterTypes.Length)
                {
                    throw new InvalidOperationException(StringExtensionMethods.FormatWith(Resources.CsvFileAttribute_CountsDiffer, IEnumerableExtensionMethods.Count(Files), parameterTypes.Length));
                }
#else
                if (Files.Count() != parameterTypes.Length)
                {
                    throw new InvalidOperationException(Resources.CsvFileAttribute_CountsDiffer.FormatWith(Files.Count(), parameterTypes.Length));
                }
#endif

                var index = -1;
                foreach (var file in Files)
                {
                    index++;
                    if (parameterTypes[index] == typeof(CsvFile) || parameterTypes[index] == typeof(IEnumerable<KeyStringDictionary>))
                    {
                        list.Add(new CsvFile(file));
                        continue;
                    }

                    if (parameterTypes[index] == typeof(DataTable))
                    {
                        list.Add(new CsvFile(file).ToDataTable());
                        continue;
                    }

                    if (parameterTypes[index] != typeof(CsvFile))
                    {
                        throw new InvalidOperationException(Resources.CsvFileAttribute_UnsupportedParameterType);
                    }
                }
            }

            yield return list.ToArray();
        }
    }
}