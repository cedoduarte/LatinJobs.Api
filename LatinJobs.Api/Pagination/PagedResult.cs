﻿namespace LatinJobs.Api.Pagination
{
    public class PagedResult<T>
    {
        public int TotalCount { get; set; }
        public IEnumerable<T>? Items { get; set; }
    }
}
