using System.Collections.Generic;
using WebApplication.Models.Test;

namespace WebApplication.Models.Paging
{
    public class TestPreviewPagingViewModel
    {
        public List<PreviewTestViewModel> Tests { get; set; }
        public PagingInfo PagingInfo { get; set; }        
    }
}