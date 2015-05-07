using Carving.Infrastructrue.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Carving.Test.Carving.Infrastructrue.Excel
{
    public class ExcelReaderTest
    {
        [Fact]
        public void TestRead()
        {
            ExcelReader reader = new ExcelReader(@"C:\Users\mikawudi\Desktop\test.xls");
            if(!reader.Open())
            {
                return;
            }
            var result = reader.Select<TestClass>("Sheet1");
            reader.Close();
        }
    }
    public class TestClass
    {
        public int testCol1 { get; set; }
        public string testCol2 { get; set; }
        public int testCol3 { get; set; }
    }
}
