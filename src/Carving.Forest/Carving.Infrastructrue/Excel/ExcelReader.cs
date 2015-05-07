using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToExcel;

namespace Carving.Infrastructrue.Excel
{
    public class ExcelReader
    {
        private ExcelQueryFactory excelFile = null;
        private string filePath = null;
        public ExcelReader(string filePath)
        {
            this.filePath = filePath;
        }
        public bool Open()
        {
            bool result = false;
            try
            {
                this.excelFile = new ExcelQueryFactory(this.filePath);
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }
        public List<T> Select<T>(string sheetName)
        {
            List<T> result = null;
            try
            {
                var queryAble = this.excelFile.Worksheet<T>(sheetName);
                result = queryAble.ToList();
            }
            catch
            {

            }
            return result;
        }
        public void Close()
        {
            if(this.excelFile!=null)
            {
                this.excelFile.Dispose();
            }
        }
    }
}
