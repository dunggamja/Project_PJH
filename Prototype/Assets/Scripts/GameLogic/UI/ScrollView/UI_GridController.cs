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
        RIGHT,
    }

    /// <summary>
    /// 그리드 아이템이 배치될 방향.
    /// </summary>
    public ScrollDirection _eScrollDir = ScrollDirection.DOWN;

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


    float _scrollWidth = 0f;
    float _scrollHeight = 0f;

    float _contentSizeX = 0f;
    float _contentSizeY = 0f;

    float _startPosX = 0f;
    float _startPosY = 0f;


    /// <summary>
    /// 그리드 아이템들을 생성합니다. 
    /// 현재는 무조건 Y축 아래 방향으로만 계산이 되어있음. (DOWN)
    /// 추후에 UP,DOWN, RIGHT, LEFT로 개선 예정.
    /// </summary>
    [ContextMenu("CreateGridItem")]
    public void Create_GridItem()
    {
        //화면에 보이는 스크롤 뷰 패널 사이즈.
        _scrollHeight = _transScrollRect.rect.height;
        _scrollWidth = _transScrollRect.rect.width;

        //0 예외처리.
        if (_itemWidth <= 0)
            _itemWidth = 1;
        if (_itemHeight <= 0)
            _itemHeight = 1;


        //행, 렬 계산.
        Calc_RowColumnCnt();
        

        // 스크롤 영역 사이즈 계산.
        _contentSizeX = _transContent.sizeDelta.x;
        _contentSizeY = _transContent.sizeDelta.y;

        // 해당 영역으로 스크롤이 될 경우에만 
        // 영역 사이즈를 다시 계산합니다. 
        if (_scrollRect.horizontal)
            _contentSizeX = _columnCnt * _itemWidth;
        if (_scrollRect.vertical)
            _contentSizeY = _rowCnt * _itemHeight;

        // 스크롤영역 사이즈 변경.
        _transContent.sizeDelta = new Vector2(_contentSizeX, _contentSizeY);

        // 그리드 아이템의 시작지점 X.
        if(_scrollRect.horizontal)
            _startPosX = (_itemWidth - _contentSizeX) * 0.5f;

        // 그리드 아이템의 시작지점 Y
        if(_scrollRect.vertical)
            _startPosY = (_contentSizeY - _itemHeight) * 0.5f;

        _listGridItem.Clear();
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
                {
                    gridItemComponent.RectTrans = transItem;
                    gridItemComponent._dataIdx = idx;
                }

                if (null != transItem)
                    transItem.anchoredPosition = new Vector2(x * _itemWidth + _startPosX, -y * _itemHeight + _startPosY);

                newItem.name = string.Format("{0}_{1}", newItem.name, idx);
                _listGridItem.Add(gridItemComponent);
            }
        }

        
        Refresh_GridItem(false, true);


        //원본 아이템은 안보이도록 처리해줍니다. 
        var oriGridItem = _gridItem.GetComponent<UI_GridItem>();
        if (null != oriGridItem)
            oriGridItem.IsVisible = false;

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
        for(int i = 0; i < 700; ++i)
            _listGridItemData.Add(new UI_GridItemData());

        Create_GridItem();
    }

    int _prevStartX_For_GridShow = 0;
    int _prevStartY_For_GridShow = 0;

    
    /// <summary>
    /// 스크롤 뷰안에 있는 그리드 아이템을 갱신합니다. 
    /// </summary>
    /// <param name="checkScrollPos">이전 위치와 현재 위치를 비교합니다.</param>
    /// <param name="refreshAllItem">모든 아이템을 갱신 할것인지 변경된 아이템만 갱신할 것인지 체크합니다.</param>
    private void Refresh_GridItem( bool checkScrollPos = true,  bool refreshAllItem = false)
    {
        if (0 == _itemHeight || 0 == _itemWidth)
            return;

        //시작 좌표 (X , Y)
        int startX = -(int)(_transContent.anchoredPosition.x / _itemWidth);
        int startY = (int)(_transContent.anchoredPosition.y / _itemHeight);

        if (_columnCnt - _showColumnCnt < startX)
            startX = _columnCnt - _showColumnCnt;
        if (_rowCnt - _showRowCnt < startY)
            startY = _rowCnt - _showColumnCnt;

        if (startX < 0)
            startX = 0;
        if (startY < 0)
            startY = 0;
        

        //변경점이 없으면 return;
        if (checkScrollPos)
        {
            if (_prevStartX_For_GridShow == startX && _prevStartY_For_GridShow == startY)
                return;
        }


        //새로 표시된 Data의 인덱스.
        List<int> listNewIdx = new List<int>();
        List<int> showItemIdx = new List<int>();
        List<int> prevShowItemIdx = new List<int>();
        for (int i = 0; i < _listGridItem.Count; ++i)
            prevShowItemIdx.Add(_listGridItem[i]._dataIdx);


        //표시될 Data의 인덱스 
        for (int y = startY; (y < startY + _showRowCnt) && (y < _rowCnt); ++y)
        {
            for (int x = startX; (x < startX + _showColumnCnt) && (x < _columnCnt); ++x)
            {
                int showIdx = y * _columnCnt + x;
                showItemIdx.Add(showIdx);

                if (false == prevShowItemIdx.Contains(showIdx))
                    listNewIdx.Add(showIdx);
            }
        }

        //갱신될 그리드아이템 객체들.
        List<UI_GridItem> listRefreshItem = new List<UI_GridItem>();
        
        //모든 아이템 갱신.
        if (refreshAllItem)
        {
            listRefreshItem.AddRange(_listGridItem);
            
            // 데이터 인덱스 재설정.
            for (int i = 0; i < showItemIdx.Count; ++i)
                listRefreshItem[i]._dataIdx = showItemIdx[i];
        }
        //변경되어야 할 아이템만 갱신.
        else
        {
            // 그리드 아이템이 참조하는 데이터의 인덱스를 참조ㅜ
            //  - 현재 표시되어야 하는 데이터가 아닌 값을 참조한다면 갱신목록 리스트에 추가.
            for (int i = 0; i < _listGridItem.Count; ++i)
            {
                if (false == showItemIdx.Contains(_listGridItem[i]._dataIdx))
                    listRefreshItem.Add(_listGridItem[i]);
            }

            // 갱신 목록들 데이터 인덱스 설정.
            for (int i = 0; i < listNewIdx.Count; ++i)
                listRefreshItem[i]._dataIdx = listNewIdx[i];
        }

        for (int i = 0; i < listRefreshItem.Count; ++i)
        {
            int x = listRefreshItem[i]._dataIdx % _columnCnt;
            int y = listRefreshItem[i]._dataIdx / _columnCnt;
            listRefreshItem[i].RectTrans.anchoredPosition = new Vector2(x * _itemWidth + _startPosX, -y * _itemHeight + _startPosY);
            listRefreshItem[i].TestGridItem();

            //1. 해당 아이템이 스크롤영역안에 있는지 체크.
            //2. 데이터 INDEX 체크.
            // - 2개의 조건이 모두 참일 경우에만 표시합니다.
            listRefreshItem[i].IsVisible = CheckIdx_IsInDataList(listRefreshItem[i]._dataIdx);
        }

        _prevStartX_For_GridShow = startX;
        _prevStartY_For_GridShow = startY;
    }

    /// <summary>
    /// 해당 그리드 아이템의 INDEX가 데이터 리스트를 벗어나지 않는지 체크합니다. 
    /// </summary>
    /// <param name="idx"></param>
    /// <returns></returns>
    private bool CheckIdx_IsInDataList(int idx)
    {
        if (0 == _itemHeight || 0 == _itemWidth)
            return false ;

        if (idx < 0 || _listGridItemData.Count <= idx)
            return false;

        return true;
    }

    /// <summary>
    /// 표시될 수 있는 그리드 아이템의 행, 렬 수를 체크합니다. 
    /// </summary>
    private void Calc_RowColumnCnt()
    {
        //공통로직.
        if (_columnCnt <= 0)
            _columnCnt = 1;

        //패널 사이즈에서 표시될 수 있는 그리드 아이템의 갯수를 체크.
        _showColumnCnt = (int)(_scrollWidth / _itemWidth) + 1;
        _showRowCnt = (int)(_scrollHeight / _itemHeight) + 1;

        //아이템이 배치될 방향에 따라 따로 처리
        // 1. 아래로 배치
        if (ScrollDirection.DOWN == _eScrollDir)
        {
            //컬럼 카운트가 1개일 경우 예외처리.
            if (1 == _columnCnt)
                _showColumnCnt = 1;

            _rowCnt = _listGridItemData.Count / _columnCnt;

            //행 계산에 대한 예외처리
            // - 예) 그리드 아이템 5개, 컬럼 갯수가 3개이면 
            //       5 / 3 = 1 ,  5 % 3 = 2 이므로 
            //       ++1,  2줄을 표시하게 됩니다.
            if ((0 != (_listGridItemData.Count % _columnCnt) && 0 != _rowCnt))
                ++_rowCnt;
        }
        // 2. 오른쪽으로 배치
        else if(ScrollDirection.RIGHT == _eScrollDir)
        {
            //로우 카운트가 1개일 경우 예외처리.
            if (1 == _rowCnt)
                _showRowCnt = 1;

            _columnCnt = _listGridItemData.Count / _rowCnt;

            //열 계산에 대한 예외처리
            // - 예) 그리드 아이템 5개, 로우 갯수가 3개이면 
            //       5 / 3 = 1 ,  5 % 3 = 2 이므로 
            //       ++1,  2줄을 표시하게 됩니다.
            if ((0 != (_listGridItemData.Count % _rowCnt) && 0 != _columnCnt))
                ++_columnCnt;
        }
    }
}
