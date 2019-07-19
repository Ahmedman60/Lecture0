using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCProjectLab.Helpers
{
    public static class CustomHtmlHelpers
    {
        public static  IHtmlString Image(this HtmlHelper htmlhelper,string src,string alt)
        {
            //Building the Image Tag()
            TagBuilder tb = new TagBuilder("img");
            //VirtualPathUtility is convert the releative path(المكان الانت فاتح منو دوقتى ) to المسار الكامل من الروت ديركتورى عشان لوحطيط على سيرفير مثلا. 
            //Relative Path start from the working directory instead of the root directory.
            tb.Attributes.Add("src", VirtualPathUtility.ToAbsolute(src));
            tb.Attributes.Add("alt", alt);
            return new MvcHtmlString(tb.ToString(TagRenderMode.SelfClosing));
        }

        public static IHtmlString ImageActionLink(this HtmlHelper htmlHelper, string linkText, string action, string controller, object routeValues, object htmlAttributes, string imageSrc ,string imageClass)
        {
            //<a href="/Products/Details/1" ><img src=""/></a>
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var img = new TagBuilder("img"); //<img  src="ImageSrc"/>
            img.Attributes.Add("src", VirtualPathUtility.ToAbsolute(imageSrc));
            img.AddCssClass(imageClass);
            var anchor = new TagBuilder("a")  //<a href="" class="Form-Control"><img  src="ImageSrc" /> <a/>
            {
                InnerHtml = img.ToString(TagRenderMode.SelfClosing)
            };
            anchor.Attributes["href"] = urlHelper.Action(action, controller, routeValues);
            anchor.MergeAttributes(new RouteValueDictionary(htmlAttributes));  // new {Class ="Form-Control" }

            return MvcHtmlString.Create(anchor.ToString());
        }
    }
}