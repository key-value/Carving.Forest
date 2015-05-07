// Carving.Infrastructrue CallLogger .cs
// writer sundy
// Last Update Time 2015-05-07-14:32
// Create Time 2015-05-07-14:32

using System.Globalization;
using System.IO;
using System.Linq;
using Castle.DynamicProxy;
using NLog;


namespace Carving.Infrastructrue.Aop
{
    public class CallLogger : IInterceptor
    {

        public CallLogger()
        {

        }

        public void Intercept(IInvocation invocation)
        {
            Log.Log.SendInfo(string.Format("Calling method {0} with parameters {1}... ",
               invocation.Method.Name,
               string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray())));

            invocation.Proceed();

            Log.Log.SendInfo(string.Format("Done: result was {0}.", invocation.ReturnValue));
        }
    }
}