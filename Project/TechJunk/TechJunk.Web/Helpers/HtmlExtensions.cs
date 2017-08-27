namespace TechJunk.Web.Helpers
{
    using System.Web.Mvc;

    public static class HtmlExtensions
    {
        public static MvcHtmlString Image(this HtmlHelper helper, string imageUrl, string alt = null, string width = "200px", string height = "125px")
        {
            TagBuilder builder = new TagBuilder("img");
            builder.MergeAttribute("src", imageUrl);
            if (alt != null)
            {
                builder.MergeAttribute("alt", alt);
            }

            builder.MergeAttribute("width", width);
            builder.MergeAttribute("height", height);

            return new MvcHtmlString(builder.ToString(TagRenderMode.SelfClosing));
        }
    }
}