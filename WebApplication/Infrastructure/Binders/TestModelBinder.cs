using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using WebApplication.Models.Question;
using WebApplication.Models.Test;

namespace WebApplication.Infrastructure.Binders
{
    public class TestModelBinder : IModelBinder
    {
        // ???????????????
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var entry = new TestViewModel();

            //string name = FromPostedData<string>(bindingContext, "Name");
            //string description = FromPostedData<string>(bindingContext, "Description");
            //double minPercentage = FromPostedData<double>(bindingContext, "MinPercentage");
            //List<QuestionViewModel> questions = FromPostedData<List<QuestionViewModel>>(bindingContext, "Questions");
            
            //entry.Name = name;
            //entry.Description = description;
            //entry.MinPercentage = minPercentage;
            //for (int i = 0; i < questions?.Count; i++)
            //{
            //    if(questions[i].Img!=null && questions[i].Img?.ContentLength!=0)
            //    {
            //        using (var binaryReader = new BinaryReader(questions[i].Img.InputStream))
            //        {
            //            questions[i].ByteImg = binaryReader.ReadBytes(questions[i].Img.ContentLength);
            //        }
            //    }
            //}
            //entry.Questions = questions;

            return entry;
        }

        private T FromPostedData<T>(ModelBindingContext bindingContext, string prefix)
        {
            var valueProvider = bindingContext.ValueProvider;
            var valueProviderResult = valueProvider.GetValue(prefix);
            return (T)valueProviderResult?.ConvertTo(typeof(T));
        }
    }
}