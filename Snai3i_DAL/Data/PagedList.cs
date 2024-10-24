using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snai3i_DAL.Data
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }

        public int TotalPages { get; private set; }

        public int TotalCount { get; private set; }

        public int pageSize { get; private set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public PagedList(List<T> items, int _currentpage, int _pagesize, int count)
        {
            CurrentPage = _currentpage;
            TotalCount = count;
            pageSize = _pagesize;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)pageSize);

            AddRange(items);
        }

        public static async Task<PagedList<T>> ToPagedList(IQueryable<T> query, int _pagesize, int _currentpage)
        {
            var count = await query.CountAsync();

            var items = await query.Skip( (_currentpage - 1) * _pagesize ).Take(_pagesize).ToListAsync();
            return new PagedList<T>(items, _currentpage , _pagesize ,count);

        }
    }
}
