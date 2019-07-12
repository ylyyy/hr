using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//System.Web.Mvc  //不同再导命名空间
namespace WebApplication1.Views.Index//这个使用要导命名空间
{
    public static class HtmlHelperExt
    {
        public static MvcHtmlString Message(this HtmlHelper helper, string id, string name, string style, object message)
        {
            if (message != null)
            {
                TagBuilder bulder = new TagBuilder("span");
                bulder.GenerateId(id); //标记字段
                bulder.MergeAttribute("name", name); //添加属性
                bulder.MergeAttribute("style", style);
                bulder.SetInnerText(message.ToString()); //显示的文本
                return bulder.ToMvcHtmlString(TagRenderMode.Normal);
            }
            return null;
        }
        public static MvcHtmlString submit(this HtmlHelper helper, string id, string value, string style)
        {
            TagBuilder bulder = new TagBuilder("input");
            bulder.GenerateId(id); //标记字段
            bulder.MergeAttribute("type", "submit"); //添加属性
            bulder.MergeAttribute("value", value);
            bulder.MergeAttribute("style", style);
            return bulder.ToMvcHtmlString(TagRenderMode.Normal);
        }
    }
    public static class TagBulderExtensions
    {
        public static MvcHtmlString ToMvcHtmlString(this TagBuilder tagbuilder, TagRenderMode mode)
        {
            Debug.Assert(tagbuilder != null);
            return new MvcHtmlString(tagbuilder.ToString(mode));
        }
    }
}