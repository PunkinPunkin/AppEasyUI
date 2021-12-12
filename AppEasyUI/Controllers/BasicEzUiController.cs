using AppEasyUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppEasyUI.Controllers
{
    public abstract class BasicEzUiController : Controller
    {
        public ILogger _logger;
        public BasicEzUiController(ILogger logger)
        {
            _logger = logger;
        }

        protected EzUiPaginationOption? GetPageOptionInfo()
        {
            if (Request.Form.ContainsKey("page"))
            {
                string page = Request.Form["page"];
                if (int.TryParse(page, out int pageIndex))
                {
                    EzUiPaginationOption paginationOption = new();
                    if (pageIndex > 0)
                        paginationOption.Page = pageIndex;
                    string rows = Request.Form["rows"];
                    if (int.TryParse(rows, out int pageSize))
                    {
                        if (pageSize > 0)
                            paginationOption.Rows = pageSize;
                    }
                    return paginationOption;
                }
            }
            return null;
        }

        protected EzUiSortInfo? GetSortInfo()
        {
            if (Request.Form.ContainsKey("sort"))
            {
                string sort = Request.Form["sort"];
                if (!string.IsNullOrWhiteSpace(sort))
                {
                    EzUiSortInfo sortInfo = new();
                    sortInfo.Sort = sort;
                    string order = Request.Form["order"];
                    if (Enum.TryParse(typeof(EzUiSortType), order, out object? ezSortType))
                    {
                        if (ezSortType != null)
                            sortInfo.Order = (EzUiSortType)ezSortType;
                    }
                    return sortInfo;
                }
            }
            return null;
        }
    }
}
