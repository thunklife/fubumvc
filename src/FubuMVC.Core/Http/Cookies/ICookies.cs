using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Linq;

namespace FubuMVC.Core.Http.Cookies
{
    /// <summary>
    /// Provides access to cookies in the request
    /// </summary>
    public interface ICookies
    {
        bool Has(string name);
        Cookie Get(string name);

        /// <summary>
        /// This function is only useful for single value cookies
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        string GetValue(string name);

        /// <summary>
        /// All the cookies in the http request
        /// </summary>
        IEnumerable<Cookie> Request { get; }
    }

    public class Cookies : ICookies
    {
        private readonly Lazy<IEnumerable<Cookie>> _cookies;

        public Cookies(ICurrentHttpRequest request)
        {
            _cookies = new Lazy<IEnumerable<Cookie>>(() => {
                return request.GetHeader(HttpRequestHeader.Cookie).Select(x => CookieParser.ToCookie(x)).ToArray();
            });
        }

        public bool Has(string name)
        {
            return _cookies.Value.Any(x => x.Matches(name));
        }

        public Cookie Get(string name)
        {
            return _cookies.Value.FirstOrDefault(x => x.Matches(name));
        }

        public string GetValue(string name)
        {
            var cookie = Get(name);
            return cookie.GetValue(name);
        }

        public IEnumerable<Cookie> Request
        {
            get { return _cookies.Value; }
        }
    }
}