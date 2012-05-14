namespace Cavity.Data.Transformation
{
    using System.Collections.Generic;
    using System.Xml;

    public sealed class AdditionTransformer : ITransformCsv<int>
    {
        public IEnumerable<int> TransformEntries(CsvFile csv)
        {
            foreach (var entry in csv)
            {
                var value = 0;
                foreach (var item in entry)
                {
                    value += XmlConvert.ToInt32(item.Value);
                }

                yield return value;
            }
        }
    }
}