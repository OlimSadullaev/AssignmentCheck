using AssignmentCheck.Domain.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentCheck.Service.Extensions
{
    public static class CollectionExtension
    {
        public static IQueryable<T> ToPagedList<T>(this IQueryable<T> source, PaginationParams @object)
        {
            return @object.PageIndex > 0 && @object.PageSize >= 0
                ? source.Skip((@object.PageIndex - 1) * @object.PageSize).Take(@object.PageSize)
                : source;
        }
    }
}
