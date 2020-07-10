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

public class ExcelWritter : MonoBehaviour
{
    public static List<string> ansList = new List<string>();
    //新增一行新資料需要花費的時間
    public static string timer;


    public static void WriteExcel(string ExcelName ,string SheetName) {
        string Path = Application.streamingAssetsPath + "/" + ExcelName + ".xlsx";

        
        FileInfo newFile = new FileInfo(Path);

        //透過ExcelPackage開啟Excel表單
        using (ExcelPackage package = new ExcelPackage(newFile)) {
            //讀取Sheet表單
            ExcelWorksheet worksheet;
            worksheet = package.Workbook.Worksheets[SheetName];
            
            // 讀取哪些欄位已經有資料，接續從該資料下方繼續寫入新資料
            float collect = ExcelWritter.ReadExcel(Path,SheetName).Count;
            worksheet.Cells["A" + (collect + 1)].Value = timer;
            //抓取要填入資料的表單代號[A、B、C]，可自行增加與修改要寫入的資料欄位
            //假設此案只儲存LevelID和Total Score
            for (int i = 0; i < 2; i++)
            {
                string letter = "";
                switch (i)
                {
                    case 0:
                        letter = "A";
                        break;
                    
                    case 1:
                        letter = "B";
                        break;
                }
                //將資料帶入表格內，如果是第一個位置資料
                // worksheet.Cells["A1"].Value = 要帶入的資料
                worksheet.Cells[letter + (collect + 1)].Value = ExcelWritter.ansList[i];
            
            
            }
            //儲存Excel
            package.Save();
            //將帶入Excel內的anList List資料清除，以免資料堆砌
            ExcelWritter.ansList.Clear();
        }


    }

    //讀取Row目前在Excel表單中有多少行數
   static DataRowCollection ReadExcel(string ExcelPath, string SheetName) {

        FileStream stream = File.Open(ExcelPath, FileMode.Open, FileAccess.Read, FileShare.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

        DataSet result = excelReader.AsDataSet();
        return result.Tables[SheetName].Rows;
        }








}
