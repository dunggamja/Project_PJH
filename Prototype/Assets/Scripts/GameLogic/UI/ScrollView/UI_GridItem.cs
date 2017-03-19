using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class UI_GridItemData
{

}

public class UI_GridItem : UIObject
{
    [HideInInspector]
    public int  _dataIdx = 0;

    public void TestGridItem()
    {
        GetControl<UnityEngine.UI.Text>("Text").text = _dataIdx.ToString();
    }
}
