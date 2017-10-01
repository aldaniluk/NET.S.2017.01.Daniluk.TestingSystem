using System.Linq.Expressions;
using System.Web.Mvc;
using System;
using WebApplication.Models.Answer;
using WebApplication.Models.Test;
using System.Web.Mvc.Html;

namespace WebApplication.Infrastructure.Helpers
{
    public static class CustomHelpers
    {
        public static MvcHtmlString Image(this HtmlHelper htmlHelper, string src, int width, int height)
        {
            var img = new TagBuilder("img");
            img.Attributes.Add("src", src);
            img.Attributes.Add("width", width.ToString());
            img.Attributes.Add("height", height.ToString());

            return new MvcHtmlString(img.ToString());
        }
    }
}