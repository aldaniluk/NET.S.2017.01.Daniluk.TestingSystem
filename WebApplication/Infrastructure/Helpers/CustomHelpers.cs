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
    }
}