using System;
using System.Collections.Generic;

namespace FLS.ServerSide.SharingObject
{
    public class PageFilterModel
    {
        private int __page;
        private int __pageSize;
        private string __key;
        public string Key
        {
            get { return __key; }
            set { __key = string.IsNullOrWhiteSpace(value) ? null : value.Trim(); }
        }
        public int Page
        {
            get { return __page; }
            set { __page = value < 1 ? 1 : value; }
        }
        public int PageSize
        {
            get { return __pageSize; }
            set { __pageSize = value < 1 ? 1 : (value > 200 ? 200 : value); }
        }
        public List<FilterModel> Filters { get; set; }
    }
    public class FilterModel
    {
        public FilterEnum Key { get; set; }
        public object Value { get; set; }
    }
}
