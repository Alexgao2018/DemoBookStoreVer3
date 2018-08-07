using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Text;
namespace MyBookShopWeb.Models
{
    public static class PagerHelper
    {
        public static MvcHtmlString Pager(this HtmlHelper helper, int totalRows,
            int pageSize, int currentPage, int numbers)
        {
            //计算总页数
            int totalPages = (totalRows % pageSize == 0 ?
            totalRows / pageSize : totalRows / pageSize + 1);
            int leftPageNum = 1;//起始页码数
            int rightPageNum = totalPages;//结束页码数
            //取能显示的页码数量的一半做偏移
            //（小于一半时，页码输出不变
            //大于一半时时，页码输出左右值要动态计算
            int offset = ((numbers % 2 == 1) ? numbers : numbers + 1) / 2;//页码个数左右偏移量
            if (totalPages > numbers)
            { // 总页数大于要显示的页数
                if (currentPage <= offset)
                { //当前页码小于中间值
                    leftPageNum = 1;
                    rightPageNum = numbers;
                }
                else
                { //如果当前页大于左偏移
                    //如果当前页码右偏移超出最大分页数
                    if (currentPage + offset >= totalPages + 1)
                    {
                        leftPageNum = totalPages - numbers + 1;
                    }
                    else
                    {
                        //左右偏移都存在时的计算
                        leftPageNum = currentPage - offset;
                        rightPageNum = (numbers % 2 == 1) ?
                        (currentPage + offset) : (currentPage + offset - 1);
                    }
                }
            }
            //用户提交的查询参数保存在路由中
            RouteValueDictionary routeValues = helper.ViewContext.RouteData.Values;
            //查询条件存在url中，则通过Request.QueryString获取
            var queryString = helper.ViewContext.HttpContext.Request.QueryString;
            //将QueryString中的参数循环保存在路由对象中
            foreach (String key in queryString.Keys)
            {
                routeValues[key] = queryString[key];
            }
            //查询条件通过表单Post提交，则通过Request.Form获取
            var formString = helper.ViewContext.HttpContext.Request.Form;
            //将Form中的参数循环保存在路由对象中
            foreach (String key in formString)
            {
                routeValues[key] = formString[key];
            }
            StringBuilder builder = new StringBuilder();
            builder.Append("<div class=\"pager\">");
            //当前页码大于1
            if (currentPage > 1)
            {
                //在路由中存储页码,输出第一页<<标签
                routeValues["pageNo"] = 1;
                builder.Append(helper.ActionLink("<<", routeValues["action"].ToString(), routeValues));
                //在路由中存储上一页page，输出上一页<标签
                routeValues["pageNo"] = currentPage - 1;
                builder.Append(helper.ActionLink("<", routeValues["action"].ToString(), routeValues));
            }
            //视图中输出页码
            for (int i = leftPageNum; i <= rightPageNum; i++)
            {
                if (i == currentPage)
                {
                    builder.Append("<span class=\"current\">" + i + "</span>");
                }
                else
                {
                    routeValues["pageNo"] = i;
                    builder.Append(helper.ActionLink(i.ToString(),
                    routeValues["action"].ToString(), routeValues));
                }
            }
            if (currentPage < totalPages)
            {
                //输出下一页标签>
                routeValues["pageNo"] = currentPage + 1;
                builder.Append(helper.ActionLink(">", routeValues["action"].ToString(), routeValues));
                //输出最后一页标签
                routeValues["pageNo"] = totalPages;
                builder.Append(helper.ActionLink(">>", routeValues["action"].ToString(), routeValues));
            }
            builder.Append("<span class=\"pageInfo\">第" + currentPage + "页，共" + totalPages + "页</span>");
            builder.Append("</div>");
            return new MvcHtmlString(builder.ToString());
        }
        public static MvcHtmlString Pager(this HtmlHelper helper, PagerInfo info, int numbers)
        {
            return Pager(helper, info.TotalRows, info.PageSize, info.CurrentPage, numbers);
        }
    }
    public class PagerInfo
    {
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalRows { get; private set; }
        public int TotalPages { get; private set; }

        public PagerInfo(int pageSize, int currentPage, int totalRows)
        {
            this.CurrentPage = currentPage;
            this.PageSize = pageSize;
            this.TotalRows = totalRows;
            this.TotalPages = (totalRows % PageSize == 0 ? (totalRows / PageSize) : (totalRows / PageSize + 1));
        }
    }
}