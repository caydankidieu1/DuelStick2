using UnityEngine;
using System;
using System.Collections.Generic;

public class ReadExcelOnline
{
    public static void GetTable(string id, string gridId, Action<List<List<string>>> callBack)
    {
        LoadWebClient(id, gridId, s =>
        {
            List<List<string>> listStr = new List<List<string>>();
            s = s.Replace("\r", string.Empty);
            Debug.Log(s);
            var rows = s.Split('\n');


            for (int i = 0; i < rows.Length; i++)
            {
                var col = rows[i].Split(',');
                if (col[0] == "End")
                {
                    Debug.Log("End");
                    break;
                }
                List<string> row = new List<string>();
                row.AddRange(col);
                listStr.Add(row);
            }
            callBack(listStr);
        });
    }

    public static void LoadWebClient(string id, string gridId, Action<string> callBack)
    {
        string url = string.Format(
            @"https://docs.google.com/spreadsheets/d/{0}/gviz/tq?tqx=out:csv&sheet={1}", id, gridId);
        LoadWebClient3(url, callBack);
    }

    public static void LoadWebClient3(string id, Action<string> callBack)
    {
        WWW w = new WWW(id);

        while (!w.isDone)
        {
            w.MoveNext();
        }

        Debug.Log(w.text);
        callBack(w.text);
    }
}