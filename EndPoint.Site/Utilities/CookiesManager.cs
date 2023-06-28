namespace EndPoint.Site.Utilities
{
    public class CookiesManager
    {
        public void Add(HttpContext context, string token, string value)
        {
            context.Response.Cookies.Append(token, value, getCookieOptions(context));
        }
        public bool Contains(HttpContext context, string token)
        {
            return context.Request.Cookies.ContainsKey(token);
        }
        public string GetValue(HttpContext context, string token)
        {
            string cookiesValue;

            if (!context.Request.Cookies.TryGetValue(token, out cookiesValue))
            {
                return null;
            }
            return cookiesValue;
        }
        public void Remove(HttpContext context, string token)
        {
            if (context.Request.Cookies.ContainsKey(token))
            {
                context.Response.Cookies.Delete(token);
            }
        }
        private CookieOptions getCookieOptions(HttpContext context)
        {
            return new CookieOptions
            {
                HttpOnly = true,
                Path = context.Request.PathBase.HasValue ? context.Request.PathBase.ToString() : "/",
                Secure = context.Request.IsHttps,
                Expires = DateTime.Now.AddDays(60)
            };
        }
        public Guid GetBrowserId(HttpContext context)
        {
            string browserId = GetValue(context, "BrowserId");
            if (browserId==null)
            {
                string value = Guid.NewGuid().ToString();
                Add(context, "BrowserId", value);
                browserId = value;
            }
            Guid guidbrowser;
            Guid.TryParse(browserId, out guidbrowser);
            return guidbrowser;
        }

    }
}
