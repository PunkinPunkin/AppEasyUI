using System.Linq.Dynamic.Core;

namespace AppEasyUI.Models
{
    public static class EzUiPaginationExtension
    {
        public static EzUiPagination<T> ToPagination<T>(this IQueryable<T> data, int pageIndex = 1, int pageSize = 10)
        {
            return new(data, new EzUiPaginationOption(pageIndex, pageSize));
        }

        public static EzUiPagination<T> ToPagination<T>(this IQueryable<T> data, EzUiPaginationOption? paginationOption, EzUiSortInfo? sortInfo = null)
        {
            return new(data, paginationOption, sortInfo);
        }

        public static EzUiPagination<T> ToPagination<T>(this IEnumerable<T> data, int pageIndex = 1, int pageSize = 10)
        {
            return new(data, new EzUiPaginationOption(pageIndex, pageSize));
        }

        public static EzUiPagination<T> ToPagination<T>(this IEnumerable<T> data, EzUiPaginationOption? paginationOption, EzUiSortInfo? sortInfo = null)
        {
            return new(data, paginationOption, sortInfo);
        }
    }


    public class EzUiPagination<T>
    {
        public int Total { get; set; } = 0;
        public IEnumerable<T> Rows { get; set; } = Array.Empty<T>();

        public EzUiPagination(IEnumerable<T> data, EzUiPaginationOption? paginationOption)
        {
            SetPagination(data.AsQueryable(), paginationOption, null);
        }

        public EzUiPagination(IEnumerable<T> data, EzUiPaginationOption? paginationOption, EzUiSortInfo? sortInfo)
        {
            SetPagination(data.AsQueryable(), paginationOption, sortInfo);
        }

        public EzUiPagination(IQueryable<T> data, EzUiPaginationOption? paginationOption)
        {
            SetPagination(data, paginationOption, null);
        }

        public EzUiPagination(IQueryable<T> data, EzUiPaginationOption? paginationOption, EzUiSortInfo? sortInfo)
        {
            SetPagination(data, paginationOption, sortInfo);
        }

        private void SetPagination(IQueryable<T> data, EzUiPaginationOption? paginationOption, EzUiSortInfo? sortInfo)
        {
            if (paginationOption == null)
            {
                paginationOption = new EzUiPaginationOption();
            }
            if (sortInfo != null)
            {
                data = data.OrderBy(sortInfo.Sort + " " + sortInfo.Order);
            }
            int PageNumber = paginationOption.Page;
            int PageSize = paginationOption.Rows;
            Total = data.Count();
            Rows = data.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
        }
    }

    public class EzUiPaginationOption
    {
        public int Page { get; set; } = 1;
        public int Rows { get; set; } = 10;

        public EzUiPaginationOption()
        {
        }

        public EzUiPaginationOption(int page, int rows)
        {
            Page = page;
            Rows = rows;
        }
    }

    public class EzUiSortInfo
    {
        public EzUiSortType Order { get; set; } = EzUiSortType.asc;
        public string Sort { get; set; } = "";
    }

    public enum EzUiSortType
    {
        asc, desc
    }
}
