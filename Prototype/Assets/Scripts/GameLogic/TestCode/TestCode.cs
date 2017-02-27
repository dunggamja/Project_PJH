using UnityEngine;
using System.Collections;

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
}
