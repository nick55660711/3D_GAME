using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using Excel;
using OfficeOpenXml;
using UnityEngine;
using UnityEditor;
using System.Data;
public class ReadExcel : MonoBehaviour
{

    public string Excelname;
    public  List<String> ReadData;
    // Start is called before the first frame update
    void Awake()
    {
        Read();
        
    }

    //讀取EXCEL表格內容

    void  Read()
    {
        // Excel路徑
        string path = Application.streamingAssetsPath + "/" + Excelname + ".xlsx";

         
        // 抓取的Excel工作表名稱
        string set = "Data";

        // 抓取Excel檔案-指派給stream
        FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read);
        // 創建並讀取Excel檔案
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

        // 將讀取的Excel檔案暫存在內存
        DataSet result = excelReader.AsDataSet();

        // 抓取Excel的行列數
        int columns = result.Tables[set].Columns.Count;
        int rows = result.Tables[set].Rows.Count;

        // 將資料抓取出來
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                String data = result.Tables[set].Rows[i][j].ToString();
                ReadData.Add(data);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
