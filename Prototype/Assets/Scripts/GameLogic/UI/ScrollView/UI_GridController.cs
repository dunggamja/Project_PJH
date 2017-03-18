using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
/// <summary>
/// ScrollView에서 반복되는 그리드 아이템 관리용 클래스입니다. 
/// </summary>
public class UI_GridController : MonoBehaviour
{
    /// <summary>
    /// 오브젝트가 배치될 방향
    /// </summary>
    public enum ScrollDirection
    {
        DOWN,
        END,
    }

    /// <summary>
    /// 스크롤 뷰 오브젝트
    /// </summary>
    public ScrollRect _scrollRect;

    /// <summary>
    /// 스크롤 뷰 오브젝트의 Transform
    /// </summary>
    private RectTransform _transScrollRect;

    private RectTransform _transContent;

    /// <summary>
    /// 반복될 그리드 아이템.
    /// </summary>
    public GameObject _gridItem;


    /// <summary>
    /// 그리드 아이템 너비
    /// </summary>
    public int _itemWidth = 0;
    /// <summary>
    /// 그리드 아이템 높이
    /// </summary>
    public int _itemHeight = 0;
    /// <summary>
    /// 한 줄에 표시될 컬럼 갯 수 
    /// </summary>
    public int _columnCnt = 1;
    public int _rowCnt = 1;


    private List<UI_GridItemData> _listGridItemData = new List<UI_GridItemData>();
    private List<UI_GridItem> _listGridItem = new List<UI_GridItem>();

    private int _showColumnCnt = 0;
    private int _showRowCnt = 0;


    private void Awake()
    {
        _transScrollRect = _scrollRect.GetComponent<RectTransform>();
        _transContent = _scrollRect.content.GetComponent<RectTransform>();

        //스크롤이 변경될 때 실행될 함수 
        _scrollRect.onValueChanged.AddListener(onChangeScroll);
    }

    /// <summary>
    /// 그리드 아이템들을 생성합니다. 
    /// 현재는 무조건 Y축 아래 방향으로만 계산이 되어있음. (DOWN)
    /// 추후에 UP,DOWN, RIGHT, LEFT로 개선 예정.
    /// </summary>
    [ContextMenu("CreateGridItem")]
    public void Create_GridItem()
    {
        //화면에 보이는 스크롤 뷰 패널 사이즈.
        float scrollHeight = _transScrollRect.rect.height;
        float scrollWidth = _transScrollRect.rect.width;

        //0 예외처리.
        if (_itemWidth <= 0)
            _itemWidth = 1;
        if (_itemHeight <= 0)
            _itemHeight = 1;
        if (_columnCnt <= 0)
            _columnCnt = 1;

        
        //패널 사이즈에서 표시될 수 있는 그리드 아이템의 갯수를 체크.
        _showColumnCnt = (int)(scrollWidth / _itemWidth) + 1;
        _showRowCnt    = (int)(scrollHeight / _itemWidth) + 1;

        //컬럼 카운트가 1개일 경우 예외처리.
        if (1 == _columnCnt)
            _showColumnCnt = 1;

        //행 계산.
        //int rowCnt = _listGridItemData.Count /_columnCnt;

        _rowCnt = _listGridItemData.Count / _columnCnt;

        //행 계산에 대한 예외처리
        // - 예) 그리드 아이템 5개, 컬럼 갯수가 3개이면 
        //       5 / 3 = 1 ,  5 % 3 = 2 이므로 
        //       ++1,  2줄을 표시하게 됩니다.
        if ((0 != (_listGridItemData.Count % _columnCnt) && 0 != _rowCnt))
            ++_rowCnt;
        

        // 스크롤 영역 사이즈 계산.
        float contentSizeX = _transContent.sizeDelta.x;
        float contentSizeY = _transContent.sizeDelta.y;

        // 해당 영역으로 스크롤이 될 경우에만 
        // 영역 사이즈를 다시 계산합니다. 
        if (_scrollRect.horizontal)
            contentSizeX = _columnCnt * _itemWidth;
        if (_scrollRect.vertical)
            contentSizeY = _rowCnt * _itemHeight;

        // 스크롤영역 사이즈 변경.
        _transContent.sizeDelta = new Vector2(contentSizeX, contentSizeY);

        // 그리드 아이템의 시작지점 X.
        float startPosX = 0f;
        if(_scrollRect.horizontal)
            startPosX = (_itemWidth - contentSizeX) * 0.5f;

        // 그리드 아이템의 시작지점 Y
        float startPosY = 0f;
        if(_scrollRect.vertical)
            startPosY = (contentSizeY - _itemHeight) * 0.5f;

        // 그리드 아이템 생성.
        for (int y = 0; y < _showRowCnt; ++y)
        {
            for (int x = 0; x < _showColumnCnt; ++x)
            {
                int idx = y * _showColumnCnt + x;

                var newItem = Instantiate(_gridItem, _transContent.transform, false) as GameObject;
                var transItem = newItem.GetComponent<RectTransform>();
                var gridItemComponent = newItem.GetComponent<UI_GridItem>();

                if (null != gridItemComponent)
                    gridItemComponent._rectTransform = transItem;

                if (null != transItem)
                    transItem.anchoredPosition = new Vector2(x * _itemWidth + startPosX, -y * _itemHeight + startPosY);

                newItem.name = string.Format("{0}_{1}", newItem.name, idx);
                _listGridItem.Add(gridItemComponent);
            }
        }


        Refresh_GridItem(false);
        
    }

    private void onChangeScroll(Vector2 pos)
    {
        Refresh_GridItem();
    }


    /// <summary>
    /// 현재 생성되어 있는 그리드 아이템을 삭제합니다. 
    /// </summary>
    [ContextMenu("DeleteGridItem")]
    public void Clear_GridItem()
    {
        for (int i = 0; i < _listGridItem.Count; ++i)
        {
            Destroy(_listGridItem[i]);
        }

        _listGridItem.Clear();
    }

    [ContextMenu("TestGridItem")]
    public void TestCode()
    {
        for(int i = 0; i < 4; ++i)
            _listGridItemData.Add(new UI_GridItemData());

        Create_GridItem();
    }

    int _prevStartX_For_GridShow = 0;
    int _prevStartY_For_GridShow = 0;

    private void Refresh_GridItem(bool checkScrollPos = true)
    {
        int startX = -(int)(_transContent.anchoredPosition.x / _itemWidth);
        int startY = -(int)(_transContent.anchoredPosition.y / _itemHeight);

        if (startX < 0)
            startX = 0;
        if (startY < 0)
            startY = 0;
        if (_columnCnt - _showColumnCnt < startX)
            startX = _columnCnt - _showColumnCnt;
        if (_rowCnt - _showRowCnt < startY)
            startY = _rowCnt - _showColumnCnt;

        
    }
}
