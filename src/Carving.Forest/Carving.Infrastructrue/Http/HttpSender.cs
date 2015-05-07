using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Carving.Infrastructrue.Http
{
    public class HttpSender
    {
        public HttpWebRequest request { get; private set; }
        private string Url;
        private string paramStr = null;
        public string ResponseStateCode { get; private set; }
        public string ResponseStateDesc { get; private set; }
        HttpMethod Method;
        private bool isJsonData = false;
        /// <summary>
        /// 调用异步获取响应方法的回掉事件
        /// </summary>
        public event Action<HttpSender, string> AsyncGetResponseCallBack;
        private HttpSender(string url, HttpMethod method)
        {
            this.Url = url;
            this.Method = method;
        }
        /// <summary>
        /// 构造非Json请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paramDic"></param>
        /// <param name="method"></param>
        public HttpSender(string url, Dictionary<string, string> paramDic, HttpMethod method = HttpMethod.GET)
            :this(url, method)
        {
            bool hasParam = paramDic != null && paramDic.Count > 0;
            if (hasParam)
                this.paramStr = string.Join("&", paramDic.Select(x => string.Format("{0}={1}", x.Key, x.Value)));
            if (this.Method == HttpMethod.GET && hasParam)
            {
                this.Url = string.Format("{0}?{1}", this.Url, paramStr);
            }
        }
        /// <summary>
        /// 构造Json请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="objData"></param>
        public HttpSender(string url, object objData)
            :this(url, HttpMethod.POST)
        {
            this.paramStr = LitJson.JsonMapper.ToJson(objData);
            this.isJsonData = true;
        }
        private HttpWebRequest CreateRequest()
        {
            var tempRequest  = HttpWebRequest.Create(this.Url) as HttpWebRequest;
            tempRequest.Method = this.Method.ToString();
            if (this.Method == HttpMethod.POST)
                tempRequest.ContentType = "application/x-www-form-urlencoded";
            if (this.isJsonData)
                tempRequest.ContentType = "text/Json";
            if (this.Method == HttpMethod.POST && !string.IsNullOrEmpty(this.paramStr))
            {
                var reqStreamWriter = new StreamWriter(tempRequest.GetRequestStream());
                reqStreamWriter.Write(paramStr);
                reqStreamWriter.Close();
            }
            return tempRequest;
        }
        public string GetResponse()
        {
            this.request = CreateRequest();
            var response = this.request.GetResponse() as HttpWebResponse;
            return GetResponseData(response);
        }
        public bool GetResponseAsync()
        {
            try
            {
                this.request = CreateRequest();
                this.request.BeginGetResponse(EndGetResponse, this.request);
            }
            catch
            {
                return false;
            }
            return true;
        }
        private string GetResponseData(HttpWebResponse response)
        {
            this.ResponseStateCode = response.StatusCode.ToString();
            this.ResponseStateDesc = response.StatusDescription.ToString();
            var responseStream = new StreamReader(response.GetResponseStream());
            var result = responseStream.ReadToEnd();
            responseStream.Close();
            return result;
        }
        private void EndGetResponse(IAsyncResult ar)
        {
            var requ = ar.AsyncState as HttpWebRequest;
            var response = requ.EndGetResponse(ar) as HttpWebResponse;
            var result = GetResponseData(response);
            if (AsyncGetResponseCallBack != null)
            {
                AsyncGetResponseCallBack(this, result);
            }
        }
    }
    public enum HttpMethod
    {
        GET,
        POST
    }
}
