namespace Cavity.Data.Transformation
{
    using System;
    using System.Collections.Generic;

    [Obsolete("ITransformCsv<T> is now deprecated. Use ITransformData<T> instead.")]
    public interface ITransformCsv<T>
    {
        IEnumerable<T> TransformEntries(CsvFile csv);
    }
}