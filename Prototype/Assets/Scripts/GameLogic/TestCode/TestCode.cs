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
}
