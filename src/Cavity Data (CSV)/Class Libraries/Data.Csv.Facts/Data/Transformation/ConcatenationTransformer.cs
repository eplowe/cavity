namespace Cavity.Data.Transformation
{
    using System.Collections.Generic;

    using Cavity.Collections;

    public sealed class ConcatenationTransformer : ITransformCsv
    {
        public IEnumerable<KeyStringDictionary> TransformEntries(CsvFile csv)
        {
            foreach (var entry in csv)
            {
                var value = string.Empty;
                foreach (var item in entry)
                {
                    value += item.Value;
                }

                yield return new KeyStringDictionary
                                 {
                                     { "CONCAT", value }
                                 };
            }
        }
    }
}