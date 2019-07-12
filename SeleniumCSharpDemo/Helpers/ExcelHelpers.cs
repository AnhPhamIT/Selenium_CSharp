using ExcelDataReader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCSharpDemo.Helpers
{
    public class ExcelHelpers
    {
        public static DataTable ExcelToDataTable(string fileName, string sheet)
        {
            //open file excel with Read mode
            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (data) => new ExcelDataTableConfiguration()
                        {
                            //set first row as header
                            UseHeaderRow = true
                        }
                    });

                    //Get all the Tables
                    DataTableCollection table = result.Tables;
                    //Store it in DataTable
                    DataTable resultTable = table[sheet];
                    //return
                    return resultTable;
                }
            }
        }

        public static List<Dictionary<string, string>> ReadDataInExcel(string fileName, string sheet, string testcaseID)
        {
            DataTable table = ExcelToDataTable(fileName, sheet);
            List<Dictionary<string, string>> dictArr = new List<Dictionary<string, string>>();

            //Iterate through the rows and columns of the Table
            for (int row = 0; row < table.Rows.Count; row++)
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                string curTestcaseID = table.Rows[row][0].ToString();
                if (curTestcaseID.Equals(testcaseID, StringComparison.CurrentCultureIgnoreCase))
                {
                    
                    for (int col = 0; col < table.Columns.Count; col++)
                    {
                        string headerValue = table.Columns[col].ColumnName;
                        string colValue = table.Rows[row][col].ToString();

                        dict.Add(headerValue, colValue);
                    }
                    dictArr.Add(dict);
                }
            }
            return dictArr;
        }
    }
}
