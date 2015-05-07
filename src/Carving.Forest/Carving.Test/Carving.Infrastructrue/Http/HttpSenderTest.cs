using Carving.Infrastructrue.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Carving.Test.Carving.Infrastructrue.Http
{
    public class HttpSenderTest
    {
        [Fact]
        public void TestSendGet()
        {
            string url = "http://www.baidu.com";
            var sender = new HttpSender(url, new Dictionary<string, string>() { {"name", "mika"} });
            var strResponse = sender.GetResponse();
        }
        [Fact]
        public void TestSendGetAsync()
        {
            string url = "http://www.baidu.com";
            var sender = new HttpSender(url, new Dictionary<string, string>() { { "name", "mika" } });
            sender.AsyncGetResponseCallBack += sender_AsyncGetResponseCallBack;
            var strResponse = sender.GetResponseAsync();
            Console.Read();
        }
        [Fact]
        public void TestSendPost()
        {
            string url = "http://localhost:41916/default3.aspx";
            var sender = new HttpSender(url, new Dictionary<string, string>() { { "name", "mika" } }, HttpMethod.POST);
            var strResponse = sender.GetResponse();
        }
        [Fact]
        public void TestSendPostAsync()
        {
            string url = "http://localhost:41916/default3.aspx";
            var sender = new HttpSender(url, new Dictionary<string, string>() { { "name", "mika" } }, HttpMethod.POST);
            sender.AsyncGetResponseCallBack += sender_AsyncGetResponseCallBack;
            var strResponse = sender.GetResponseAsync();
            Console.Read();
        }
        [Fact]
        public void TestSendJson()
        {
            string url = "http://localhost:41916/default3.aspx";
            var sender = new HttpSender(url, new TestData("test1", 100));
            var strResponse = sender.GetResponse();
        }
        [Fact]
        public void TestSendJsonAsync()
        {
            string url = "http://localhost:41916/default3.aspx";
            var sender = new HttpSender(url, new TestData("test1", 100));
            sender.AsyncGetResponseCallBack += sender_AsyncGetResponseCallBack;
            var strResponse = sender.GetResponseAsync();
            Console.Read();
        }
        void sender_AsyncGetResponseCallBack(HttpSender arg1, string arg2)
        {

        }
    }
    public class TestData
    {
        public string TestPar1;
        public int TestInt1;
        public TestData(string str, int testInt)
        {
            this.TestPar1 = str;
            this.TestInt1 = testInt;
        }
    }
}
