using FLS.ServerSide.SharingObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLS.ServerSide.EFCore.Entities
{
    public static class EFHandles
    {
        public static async Task<PagedList<T>> GetPagedList<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            var result = new PagedList<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = query.Count()
            };
            var pageCount = (double)result.TotalItems / pageSize;
            result.TotalPage = (int)Math.Ceiling(pageCount);
            var skip = (page - 1) * pageSize;
            result.Items = await query.Skip(skip).Take(pageSize).ToListAsync();
            return result;
        }
        public static PagedQueryableList<T> GetPagedQueryableList<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            var result = new PagedQueryableList<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalItems = query.Count()
            };
            var pageCount = (double)result.TotalItems / pageSize;
            result.TotalPage = (int)Math.Ceiling(pageCount);
            var skip = (page - 1) * pageSize;
            result.Query = query.Skip(skip).Take(pageSize);
            return result;
        }
    }
}
