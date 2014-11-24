namespace Cavity.Data.Transformation
{
    using System;
    using System.Collections.Generic;
    using Cavity.Collections;

    [Obsolete("ITransformCsv is now deprecated. Use ITransformData instead.")]
    public interface ITransformCsv
    {
        IEnumerable<KeyStringDictionary> TransformEntries(CsvFile csv);
    }
}