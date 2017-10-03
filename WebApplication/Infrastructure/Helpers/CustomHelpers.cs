using System.Linq.Expressions;
using System.Web.Mvc;
using System;
using WebApplication.Models.Answer;
using WebApplication.Models.Test;
using System.Web.Mvc.Html;
using WebApplication.Models.Paging;
using System.Text;
using System.Globalization;

namespace WebApplication.Infrastructure.Helpers
{
    public static class CustomHelpers
    {
        public static MvcHtmlString Image(this HtmlHelper htmlHelper, string src, 
            int? width = null, int? height = null, string classes = null)
        {
            var img = new TagBuilder("img");
            img.Attributes.Add("src", src);
            if(width != null)
                img.Attributes.Add("width", width.ToString());
            if (height != null)
                img.Attributes.Add("height", height.ToString());
            if (classes != null)
                img.Attributes.Add("class", classes.ToString());

            return new MvcHtmlString(img.ToString());
        }

        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, string classes = null)
        {
            var result = new StringBuilder();
            result.Append("<ul class=\"pager\">");
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                var li = new TagBuilder("li");
                li.AddCssClass("page-item");

                var tag = new TagBuilder("a");
                tag.AddCssClass("page-link");
                if (classes != null)
                    tag.AddCssClass(classes);
                tag.MergeAttribute("id", i.ToString());
                tag.InnerHtml = i.ToString(CultureInfo.InvariantCulture);
                if (i == pagingInfo.CurrentPage)
                    tag.AddCssClass("active");

                li.InnerHtml = tag.ToString();
                result.Append(li);
            }
            result.Append("</ul>");
            return MvcHtmlString.Create(result.ToString());
        }
    }
}