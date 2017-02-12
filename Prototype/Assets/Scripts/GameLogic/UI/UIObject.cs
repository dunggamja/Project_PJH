using UnityEngine;
using System.Collections;


//모든 UI객체들은 UIObject를 사용해야 함.
public class UIObject : MonoBehaviour
{
    /// <summary>
    /// UI 컬링을 위한 zPosition
    /// </summary>
    public const float INVISIBLE_ZPOS = 9999999f;

    /// <summary>
    /// 현재 보이는 UI인지 체크    
    /// </summary>
    private bool _isVisible = true;

    /// <summary>
    /// 현재 보이는 UI인지 체크
    /// 안보이는 UI일 경우 zPosition을 안보이는 곳으로 변경.
    /// UIObject 의 z값은 0으로 고정되어있다고 생각한다. 
    /// </summary>
    public bool IsVisible
    {
        get { return _isVisible; }
        set
        {
            //transform의 zPosition 값을 0으로 이동
            if (value)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
            }
            //transform의 zPosition 값을 컬링되는 위치로 이동.
            else
            {
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, INVISIBLE_ZPOS);
            }
            

            _isVisible = value;
            
        }
    }
    

}
