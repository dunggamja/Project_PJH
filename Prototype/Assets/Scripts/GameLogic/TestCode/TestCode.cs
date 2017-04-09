using UnityEngine;
using System.Collections;
using System.Text;
public class TestCode : MonoBehaviour {

    /// <summary>
    /// 테스트 용 
    /// </summary>
    [ContextMenu("TestOpnUIForm")]
    public void TestOpenUIForm()
    {
        UIFormManager.Instance.OpenUIForm<UIFormTest>();
    }


    [ContextMenu("TestOpnUIForm2")]
    public void TestOpenUIForm2()
    {
        UIFormManager.Instance.OpenUIForm<UIFormTest2>();
    }

    [ContextMenu("TestCloseUIForm")]
    public void TestCloseUIForm()
    {
        UIFormManager.Instance.CloseUIForm<UIFormTest>();
    }

    [ContextMenu("TestDeleteUIForm")]
    public void TestDeleteUIForm()
    {
        UIFormManager.Instance.DestroyCloseUIForm<UIFormTest>();
    }

    [ContextMenu("TestDeleteAllUIForm")]
    public void TestDeleteAllUIForm()
    {
        UIFormManager.Instance.DestroyAllUIForms();
    }


    [ContextMenu("TestOpnUIFormCommandUIDlg")]
    public void TestOpnUIFormCommandUIDlg()
    {
        UIFormManager.Instance.OpenUIForm<CommandUIDlg>();
    }


    [ContextMenu("TestLoadData")]
    public void TestLoadData()
    {
        //DataLoader.Instance.LoadTextFile("test.txt");
        //string source = sr.ReadLine();
        //int lineCnt = 0;
        //while (null != source)
        //{
        //    string[] strSplit = source.Split('\t');
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendFormat("Line : {0}, ", lineCnt);

        //    for (int i = 0; i < strSplit.Length; ++i)
        //    {
        //        sb.AppendFormat("t:{0} ", strSplit[i]);
        //    }

        //    Debug.Log(sb.ToString());

        //    source = sr.ReadLine();
        //}
        
    }
}
