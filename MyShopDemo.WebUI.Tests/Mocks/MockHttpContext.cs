using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShopDemo.WebUI.Tests.Mocks
{
    public class MockHttpContext :  HttpContextBase
    {
        private MockRequest request;
        private MockResponse response;
        private HttpCookieCollection cookies;

        public MockHttpContext()
        {
            cookies = new HttpCookieCollection();
            this.request = new MockRequest(cookies);
            this.response = new MockResponse(cookies);
        }

        public override HttpRequestBase Request
        {
            get
            {
                return request;
            }
        }

        public override HttpResponseBase Response
        {
            get
            {
                return response;
            }
        }
    }

    public class MockResponse : HttpResponseBase
    {
        private readonly HttpCookieCollection cookie;

        public MockResponse(HttpCookieCollection cookies)
        {
            this.cookie = cookies;
        }

        public override HttpCookieCollection Cookies
        {
            get
            {
                return cookie;
            }
        }
    }

    public class MockRequest : HttpRequestBase
    {
        private readonly HttpCookieCollection cookie;

        public MockRequest(HttpCookieCollection cookies)
        {
            this.cookie = cookies;
        }

        public override HttpCookieCollection Cookies
        {
            get
            {
                return cookie;
            }
        }
    }
}
