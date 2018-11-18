using System;
using System.Collections.Generic;

namespace FLS.ServerSide.SharingObject
{
    static public class URI_API
    {
        // CUSTOMER
        public const string CUSTOMER_SEARCH = "api/customers";
        public const string CUSTOMER_DETAIL = "api/customers/{id}";
        public const string CUSTOMER_ADD = "api/customers/add";
        public const string CUSTOMER_MODIFY = "api/customers/{id}/modify";
        public const string CUSTOMER_REMOVE = "api/customers/{id}/remove";
        // FARMING_SEASON
        public const string FARMING_SEASON_SEARCH = "api/farming-seasons";
        public const string FARMING_SEASON_DETAIL = "api/farming-seasons/{id}";
        public const string FARMING_SEASON_ADD = "api/farming-seasons/add";
        public const string FARMING_SEASON_MODIFY = "api/farming-seasons/{id}/modify";
        public const string FARMING_SEASON_REMOVE = "api/farming-seasons/{id}/remove";
        // FARM_REGION
        public const string FARM_REGION_SEARCH = "api/farm-regions";
        public const string FARM_REGION_DETAIL = "api/farm-regions/{id}";
        public const string FARM_REGION_ADD = "api/farm-regions/add";
        public const string FARM_REGION_MODIFY = "api/farm-regions/{id}/modify";
        public const string FARM_REGION_REMOVE = "api/farm-regions/{id}/remove";
        // FISH_POND
        public const string FISH_POND_SEARCH = "api/fish-ponds";
        public const string FISH_POND_DETAIL = "api/fish-ponds/{id}";
        public const string FISH_POND_ADD = "api/fish-ponds/add";
        public const string FISH_POND_MODIFY = "api/fish-ponds/{id}/modify";
        public const string FISH_POND_REMOVE = "api/fish-ponds/{id}/remove";
        // PRODUCT
        public const string PRODUCT_SEARCH = "api/products";
        public const string PRODUCT_SEARCH_LIVESTOCK = "api/products/livestocks";
        public const string PRODUCT_DETAIL = "api/products/{id}";
        public const string PRODUCT_ADD = "api/products/add";
        public const string PRODUCT_MODIFY = "api/products/{id}/modify";
        public const string PRODUCT_REMOVE = "api/products/{id}/remove";
        public const string PRODUCT_UNIT_PRODUCT_ADD = "api/products/{id}/units/add";
        public const string PRODUCT_UNIT_PRODUCT_MODIFY = "api/products/units/{id}/modify";
        public const string PRODUCT_UNIT_PRODUCT_REMOVE = "api/products/units/{id}/remove";
        // PRODUCT_GROUP
        public const string PRODUCT_GROUP_SEARCH = "api/product-groups";
        public const string PRODUCT_GROUP_DETAIL = "api/product-groups/{id}";
        public const string PRODUCT_GROUP_ADD = "api/product-groups/add";
        public const string PRODUCT_GROUP_MODIFY = "api/product-groups/{id}/modify";
        public const string PRODUCT_GROUP_REMOVE = "api/product-groups/{id}/remove";
        public const string PRODUCT_GROUP_SEARCH_SUBGROUP = "api/product-groups/{id}/product-subgroups";
        public const string PRODUCT_GROUP_SEARCH_PRODUCT = "api/product-groups/{id}/products";
        // PRODUCT_SUBGROUP
        public const string PRODUCT_SUBGROUP_SEARCH_ALL = "api/product-subgroups";
        public const string PRODUCT_SUBGROUP_SEARCH = "api/product-groups/{id}/product-subgroups";
        public const string PRODUCT_SUBGROUP_DETAIL = "api/product-subgroups/{id}";
        public const string PRODUCT_SUBGROUP_ADD = "api/product-subgroups/add";
        public const string PRODUCT_SUBGROUP_MODIFY = "api/product-subgroups/{id}/modify";
        public const string PRODUCT_SUBGROUP_REMOVE = "api/product-subgroups/{id}/remove";
        public const string PRODUCT_SUBGROUP_SEARCH_PRODUCT = "api/product-subgroups/{id}/products";
        // PRODUCT_UNIT
        public const string PRODUCT_UNIT_SEARCH = "api/product-units";
        public const string PRODUCT_UNIT_DETAIL = "api/product-units/{id}";
        public const string PRODUCT_UNIT_ADD = "api/product-units/add";
        public const string PRODUCT_UNIT_MODIFY = "api/product-units/{id}/modify";
        public const string PRODUCT_UNIT_REMOVE = "api/product-units/{id}/remove";
        // EXPENDITURE_TYPE
        public const string EXPENDITURE_TYPE_SEARCH = "api/expenditure-docket-types";
        public const string EXPENDITURE_TYPE_DETAIL = "api/expenditure-docket-types/{id}";
        public const string EXPENDITURE_TYPE_ADD = "api/expenditure-docket-types/add";
        public const string EXPENDITURE_TYPE_MODIFY = "api/expenditure-docket-types/{id}/modify";
        public const string EXPENDITURE_TYPE_REMOVE = "api/expenditure-docket-types/{id}/remove";
        // STOCK_ISSUE_DOCKET
        public const string STOCK_ISSUE_DOCKET_SEARCH = "api/stock-issue-dockets";
        public const string STOCK_ISSUE_DOCKET_DETAIL = "api/stock-issue-dockets/{id}";
        public const string STOCK_ISSUE_DOCKET_ADD = "api/stock-issue-dockets/add";
        public const string STOCK_ISSUE_DOCKET_MODIFY = "api/stock-issue-dockets/{id}/modify";
        public const string STOCK_ISSUE_DOCKET_REMOVE = "api/stock-issue-dockets/{id}/remove";
        // STOCK_ISSUE_DOCKET_TYPE
        public const string STOCK_ISSUE_DOCKET_TYPE_SEARCH = "api/stock-issue-docket-types";
        public const string STOCK_ISSUE_DOCKET_TYPE_DETAIL = "api/stock-issue-docket-types/{id}";
        public const string STOCK_ISSUE_DOCKET_TYPE_ADD = "api/stock-issue-docket-types/add";
        public const string STOCK_ISSUE_DOCKET_TYPE_MODIFY = "api/stock-issue-docket-types/{id}/modify";
        public const string STOCK_ISSUE_DOCKET_TYPE_REMOVE = "api/stock-issue-docket-types/{id}/remove";
        // STOCK_RECEIVE_DOCKET
        public const string STOCK_RECEIVE_DOCKET_SEARCH = "api/stock-receive-dockets";
        public const string STOCK_RECEIVE_DOCKET_DETAIL = "api/stock-receive-dockets/{id}";
        public const string STOCK_RECEIVE_DOCKET_ADD = "api/stock-receive-dockets/add";
        public const string STOCK_RECEIVE_DOCKET_MODIFY = "api/stock-receive-dockets/{id}/modify";
        public const string STOCK_RECEIVE_DOCKET_REMOVE = "api/stock-receive-dockets/{id}/remove";
        // STOCK_RECEIVE_DOCKET_TYPE
        public const string STOCK_RECEIVE_DOCKET_TYPE_SEARCH = "api/stock-receive-docket-types";
        public const string STOCK_RECEIVE_DOCKET_TYPE_DETAIL = "api/stock-receive-docket-types/{id}";
        public const string STOCK_RECEIVE_DOCKET_TYPE_ADD = "api/stock-receive-docket-types/add";
        public const string STOCK_RECEIVE_DOCKET_TYPE_MODIFY = "api/stock-receive-docket-types/{id}/modify";
        public const string STOCK_RECEIVE_DOCKET_TYPE_REMOVE = "api/stock-receive-docket-types/{id}/remove";
        // SUPPLIER
        public const string SUPPLIER_SEARCH = "api/suppliers";
        public const string SUPPLIER_DETAIL = "api/suppliers/{id}";
        public const string SUPPLIER_ADD = "api/suppliers/add";
        public const string SUPPLIER_MODIFY = "api/suppliers/{id}/modify";
        public const string SUPPLIER_REMOVE = "api/suppliers/{id}/remove";
        public const string SUPPLIER_SEARCH_BRANCH = "api/suppliers/{id}/branchs";
        // TAX_PERCENT
        public const string TAX_PERCENT_SEARCH = "api/tax-percents";
        public const string TAX_PERCENT_DETAIL = "api/tax-percents/{id}";
        public const string TAX_PERCENT_ADD = "api/tax-percents/add";
        public const string TAX_PERCENT_MODIFY = "api/tax-percents/{id}/modify";
        public const string TAX_PERCENT_REMOVE = "api/tax-percents/{id}/remove";
        // WAREHOUSE
        public const string WAREHOUSE_SEARCH = "api/warehouses";
        public const string WAREHOUSE_DETAIL = "api/warehouses/{id}";
        public const string WAREHOUSE_ADD = "api/warehouses/add";
        public const string WAREHOUSE_MODIFY = "api/warehouses/{id}/modify";
        public const string WAREHOUSE_REMOVE = "api/warehouses/{id}/remove";
        // WAREHOUSE_TYPE
        public const string WAREHOUSE_TYPE_SEARCH = "api/warehouse-types";
        public const string WAREHOUSE_TYPE_DETAIL = "api/warehouse-types/{id}";
        public const string WAREHOUSE_TYPE_ADD = "api/warehouse-types/add";
        public const string WAREHOUSE_TYPE_MODIFY = "api/warehouse-types/{id}/modify";
        public const string WAREHOUSE_TYPE_REMOVE = "api/warehouse-types/{id}/remove";

        // -- CACHE -- //
        public const string CACHE_EXPENDITURE_TYPE = "api/cache/expenditure-docket-types";
        public const string CACHE_FARM_REGION = "api/cache/farm-regions";
        public const string CACHE_FISH_POND = "api/cache/fish-ponds";
        public const string CACHE_PRODUCT_GROUP = "api/cache/product-groups";
        public const string CACHE_PRODUCT_SUBGROUP = "api/cache/product-subgroups";
        public const string CACHE_PRODUCT_UNIT = "api/cache/product-units";
        public const string CACHE_STOCK_ISSUE_DOCKET_TYPE = "api/cache/stock-issue-docket-types";
        public const string CACHE_STOCK_RECEIVE_DOCKET_TYPE = "api/cache/stock-receive-docket-types";
        public const string CACHE_TAX_PERCENT = "api/cache/tax-percents";
        public const string CACHE_WAREHOUSE = "api/cache/warehouses";
        public const string CACHE_WAREHOUSE_TYPE = "api/cache/warehouse-types";

        // LIVESTOCK PROCEED
        /// <summary>
        /// PUT
        /// </summary>
        public const string LIVESTOCK_PROCEED_RELEASE = "api/livestock-proceeds/release";
    }
    static public class REQUEST_HEADER
    {
        public const string USERNAME = "fls-acss-usrnme";
    }
    }
