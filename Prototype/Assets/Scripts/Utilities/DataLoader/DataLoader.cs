using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// 데이터 로드를 위한 클래스.
/// </summary>
public class DataLoad
{
    public List<string> _listLoadFile = new List<string>();

    public void AddData(string data)
    {
        if (null == _listLoadFile)
        {
            Debug.Log("_listLoadFile is null");
            return;
        }
        _listLoadFile.Add(data);
    }

    public int Count()
    {
        return _listLoadFile.Count;
    }


    public void GetData(int idx, out string data)
    {
        try
        {
            data = _listLoadFile[idx];
        }
        catch
        {
            Debug.Log("GetData is Failed");
            data = string.Empty;
        }
    }

    public void GetData(int idx, out Byte data)
    {
        try
        {
            data = Byte.Parse(_listLoadFile[idx]);
        }
        catch
        {
            Debug.Log("GetData is Failed");
            data = 0;
        }
    }

    public void GetData(int idx, out Int16 data)
    {
        try
        {
            data = Int16.Parse(_listLoadFile[idx]);
        }
        catch
        {
            Debug.Log("GetData is Failed");
            data = 0;
        }
    }

    public void GetData(int idx, out Int32 data)
    {
        try
        {
            data = Int32.Parse(_listLoadFile[idx]);
        }
        catch
        {
            Debug.Log("GetData is Failed");
            data = 0;
        }
    }

    public void GetData(int idx, out Int64 data)
    {
        try
        {
            data = Int64.Parse(_listLoadFile[idx]);
        }
        catch
        {
            Debug.Log("GetData is Failed");
            data = 0;
        }
    }
}

/// <summary>
/// 데이터를 읽기 위한 클래스입니다. 
/// TAB키로 구분된 데이터들을 읽어들입니다. 
/// </summary>
public class DataLoader
{
    static private DataLoader _instance = null;

    static public DataLoader Instance
    {
        get
        {
            if (null == _instance)
            {
                _instance = new DataLoader();
            }
            return _instance;
        }
    }

    private List<DataLoad> _listTempLoadFile = new List<DataLoad>();

    public void LoadTextFile(string filePath)
    {
        string path = string.Empty;

#if UNITY_EDITOR
        path = string.Format("{0}{1}", Application.dataPath, "/Resources/Text/");
#else
        path = string.Format("{0}{1}", Application.dataPath, "/Resources/Text/");
#endif

        _listTempLoadFile = new List<DataLoad>();
        _listTempLoadFile.Clear();

        StreamReader sr = new StreamReader(path + filePath);

        if (null == sr)
        {
            Debug.Log("Error, LoadTextFile : " + path + filePath);
            return;
        }
       

        string strLine = sr.ReadLine();

        bool bFindStartTable = false;
        while(false == string.IsNullOrEmpty(strLine))
        {
            
            if (false == bFindStartTable)
            {
                // [Table] 키워드를 발견한 다음줄부터 
                // 데이터들을 읽어 들인다. 
                string tempCompare = strLine.ToLower();
                if (tempCompare.Contains("[table]"))
                {
                    bFindStartTable = true;
                    continue;
                    
                }
            }
            

            string[] strSplit = strLine.Split('\t');
            DataLoad tempData = new DataLoad();

            for (int i = 0; i < strSplit.Length; ++i)
            {
                //첫 문자가 ;로 시작 한다면 그 후는 주석처리로 무시.
                if (0 == strSplit[i].IndexOf(';'))
                {
                    break;
                }
                else
                {
                    tempData.AddData(strSplit[i]);
                }
            }

            if(0 < tempData.Count())
                _listTempLoadFile.Add(tempData);

            strLine = sr.ReadLine();
        }

        Debug.Log("LoadTextFile : " + path + filePath);
    }


    public DataLoad GetDataLoad(int idx)
    {
        if (idx < _listTempLoadFile.Count)
            return _listTempLoadFile[idx];

        return null;
    }

    public void ClearTempData()
    {
        _listTempLoadFile.Clear();
    }

}
