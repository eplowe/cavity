namespace Cavity.Data.Transformation
{
    using System.Collections.Generic;

    using Cavity.Collections;

    public interface ITransformCsv
    {
        IEnumerable<KeyStringDictionary> TransformEntries(CsvFile csv);
    }
}