namespace Cavity.Data
{
    using System;
    using Cavity.Net;

    public interface IRecord
    {
        CacheControl Cacheability { get; set; }

        DateTime? Created { get; set; }
    }
}