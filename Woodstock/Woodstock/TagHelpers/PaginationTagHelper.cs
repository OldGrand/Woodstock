using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Woodstock.PL.TagHelpers
{
    [HtmlTargetElement(ParentAnchorTag)]
    public class PaginationTagHelper : TagHelper
    {
        private const string ParentAnchorTag = "pagination";
        private readonly IHtmlGenerator _htmlGenerator;

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        [HtmlAttributeNotBound]
        public HttpContext HttpContext => ViewContext.HttpContext;

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int Range { get; set; } = 3;

        public PaginationTagHelper(IHtmlGenerator htmlGenerator)
        {
            _htmlGenerator = htmlGenerator;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            var anchorTagHelper = new AnchorTagHelper(_htmlGenerator)
            {
                Action = "Index",
                ViewContext = ViewContext,
            };

            var range = GetPaginationRange(CurrentPage, TotalPages, Range);
            for (int i = range.Start.Value; i <= range.End.Value; i++)
            {
                var anchorOutput = new TagHelperOutput("a", new TagHelperAttributeList(),
                (useCachedResult, encoder) => Task.Factory.StartNew<TagHelperContent>(() => new DefaultTagHelperContent()));
                anchorOutput.Content.AppendHtml(i.ToString());
                var anchorContext = new TagHelperContext(new TagHelperAttributeList(
                    new[]
                    {
                        new TagHelperAttribute("asp-controller", new HtmlString("Catalog")),
                        new TagHelperAttribute("asp-action", new HtmlString("Index")),
                        new TagHelperAttribute("asp-route-pageNum", new HtmlString(i.ToString())),
                    }), 
                    new Dictionary<object, object>(),
                    Guid.NewGuid().ToString());
                    anchorTagHelper.ProcessAsync(anchorContext, anchorOutput).GetAwaiter().GetResult();
                    output.Content.SetHtmlContent(anchorOutput);
            }
        }

        private static Range GetPaginationRange(int currentPage, int total, int range)
        {
            if (range > total)
                return 1..total;

            var middleOfRange = range / 2;
            var start = currentPage - middleOfRange;
            var end = currentPage + middleOfRange;

            if (start <= 0)
            {
                start = 1;
                end = currentPage + range - 1;
            }

            if (end > total)
            {
                end = total;
                start = currentPage - range + 1;
            }

            return start..end;
        }
    }
}
