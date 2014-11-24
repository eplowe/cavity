namespace Cavity.Net
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Reflection;
    using Cavity.IO;
    using Xunit.Extensions;

    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "HttpRequest", Justification = "This naming is correct.")]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class HttpRequestAttribute : DataAttribute
    {
        public HttpRequestAttribute(string file)
            : this()
        {
            if (null == file)
            {
                throw new ArgumentNullException("file");
            }

            File = file;
        }

        private HttpRequestAttribute()
        {
        }

        public string File { get; private set; }

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

            if (0 == parameterTypes.Length)
            {
                throw new InvalidOperationException("A parameter is required.");
            }

            if (1 != parameterTypes.Length)
            {
                throw new InvalidOperationException("Only one parameter is permitted.");
            }

            var list = new List<object>();
            var info = new FileInfo(File);

            if (parameterTypes[0] == typeof(HttpRequest))
            {
                list.Add(HttpRequest.FromString(info.ReadToEnd()));
            }
            else
            {
                throw new InvalidOperationException("Only Http Request is supported as a parameter type.");
            }

            yield return list.ToArray();
        }
    }
}