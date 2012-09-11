namespace Cavity.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This is not a collection.")]
    public sealed class DataFile : IEnumerable<DataSheet>
    {
        public FileInfo Info { get; private set; }

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "TODO")]
        public string Input { get; private set; }

        public static IEnumerable<DataSheet> From(FileInfo file, string input)
        {
            if (null == file)
            {
                throw new ArgumentNullException("file");
            }

            if (string.IsNullOrWhiteSpace(input))
            {
                input = file.Extension.Substring(1);
            }

            Console.WriteLine("{0} : {1}".FormatWith(input, file.Extension.Substring(1)));

            return new DataFile
                       {
                           Info = file
                       };
        }

        public IEnumerator<DataSheet> GetEnumerator()
        {
            yield break;
            ////switch (Input)
            ////{
            ////    case "TSV":
            ////        yield return new TsvFile(Info);
            ////        break;
            ////}
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}