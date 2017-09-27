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
        //public static MvcHtmlString AnswerFor<TModel, TProperty>
        //    (this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object value)
        //    where TModel : PassTestViewModel
        //{
        //    //AnswerViewModel answer = value as AnswerViewModel;
        //    var name = ExpressionHelper.GetExpressionText(expression);
        //    //var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
        //    TagBuilder tb = new TagBuilder("input");
        //    var m = htmlHelper.ViewData.Model;
        //    //tb.Attributes.Add("type", "submit");
        //    tb.Attributes.Add("name", name);
        //    tb.Attributes.Add("id", value.ToString());
        //    tb.Attributes.Add("value", value.ToString());
        //    tb.Attributes.Add("style", "color:black");
        //    tb.Attributes.Add("class", "btn btn-default");

        //    return new MvcHtmlString(tb.ToString());
        //}

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