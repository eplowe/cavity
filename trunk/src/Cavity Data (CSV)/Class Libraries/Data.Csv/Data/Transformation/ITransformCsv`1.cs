namespace Cavity.Data.Transformation
{
    using System.Collections.Generic;

    public interface ITransformCsv<T>
    {
        IEnumerable<T> TransformEntries(CsvFile csv);
    }
}