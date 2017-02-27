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

        BindObject<Components>();

        var test1 = GetControl<UnityEngine.UI.Button>((int)Components.Btn_TurnEnd);
        var test2 = GetControl<UnityEngine.UI.Text>((int)Components.Text_TurnEnd);
        var test3 = GetControl<UnityEngine.UI.Image>((int)Components.Img_TurnEnd);

        
        test2.text = "ehlsekehlsek";
        test1.onClick.AddListener(delegate () { Debug.Log("Click_button"); });
        

        Debug.Log("fdfd");
    }
    #endregion override function

}
