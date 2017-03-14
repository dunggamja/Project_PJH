using UnityEngine;
using System.Collections;

public class CommandUIDlg : UIForm
{
    public enum Components
    {
        TurnEnd,
        Img_TurnEnd,
        Btn_TurnEnd,
        Text_TurnEnd
    }

    #region override function
    public override void Open()
    {
        base.Open();

        var test1 = GetControl<UnityEngine.UI.Button>(Components.Btn_TurnEnd.ToString());
        var test2 = GetControl<UnityEngine.UI.Text>(Components.Text_TurnEnd.ToString());
        var test3 = GetControl<UnityEngine.UI.Image>(Components.Img_TurnEnd.ToString());

        test1.onClick.AddListener(delegate () { Debug.Log("Click_button"); });
        test2.text = "ehlsekehlsek";
        
        

        Debug.Log("fdfd");
    }
    #endregion override function

}
