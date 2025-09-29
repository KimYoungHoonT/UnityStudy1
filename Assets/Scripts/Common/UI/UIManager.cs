using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonBehaviour<UIManager>
{
    public Transform UICanvasTransform;
    public Transform CloseUITransform;

    private BaseUI m_FrontUI; // 가장 위에 떠있는 UI용
    private Dictionary<Type, GameObject> m_OpenUIPool = new Dictionary<Type, GameObject>(); // 열려있는 UI 관리용
    private Dictionary<Type, GameObject> m_ClosedUIPool = new Dictionary<Type, GameObject>(); // 닫혀있는 UI 관리용

    // 열기를 원하는 UI화면의 실제 인스턴스 가져오기
    private BaseUI GetUI<T>(out bool isAlreadyOpen)
    {
        Type uiType = typeof(T);

        BaseUI ui = null;
        isAlreadyOpen = false;

        // 이미 한번 생성됬었고 열려 있다면
        if (m_OpenUIPool.ContainsKey(uiType) == true)
        {
            ui = m_OpenUIPool[uiType].GetComponent<BaseUI>();
            isAlreadyOpen = true;
        }
        // 이미 한번 생성됬었고 닫혀 있다면
        else if (m_ClosedUIPool.ContainsKey(uiType) == true)
        {
            ui = m_ClosedUIPool[uiType].GetComponent<BaseUI>();
            m_ClosedUIPool.Remove(uiType);
        }
        // 한번도 해당 UI 를 만든적이 없다면 생성해주기
        else
        {   
            GameObject uiObj = Instantiate(Resources.Load<GameObject>($"UI/{uiType}"));
            ui = uiObj.GetComponent<BaseUI>();
        }

        return ui;
    }

    // 어떤 UI 화면을 열기 위한 함수
    public void OpenUI<T>(BaseUIData uiData)
    {
        Type uiType = typeof(T);

        Logger.Log($"{GetType()}::OpenUI({uiType})");

        bool isAlreadyOpen = false;
        var ui = GetUI<T>(out isAlreadyOpen);

        // 유아이를 열려고 했으나 생성조차 안됬을떄
        if (ui == null)
        {
            Logger.LogError($"{uiType} does not exist.");
        }

        // 이미 열려있는걸 또 열려고 하는 상황
        if (isAlreadyOpen == true)
        {
            Logger.LogError($"{uiType} is already open");
            return;
        }

        int siblingIndex = UICanvasTransform.childCount;
        ui.Init(UICanvasTransform);
        ui.transform.SetSiblingIndex(siblingIndex);
        ui.gameObject.SetActive(true);
        ui.SetInfo(uiData);
        ui.ShowUI();

        m_FrontUI = ui;
        m_OpenUIPool[uiType] = ui.gameObject;
    }

    // 열려있는 UI를 닫아주기
    public void CloseUI(BaseUI ui)
    {
        Type uiType = ui.GetType();

        Logger.Log($"{GetType()}::CloseUI({uiType})");
        
        ui.gameObject.SetActive(false);
        m_ClosedUIPool[uiType] = ui.gameObject;
        ui.transform.SetParent(CloseUITransform);

        m_FrontUI = null;
        var lastChild = UICanvasTransform.GetChild(UICanvasTransform.childCount - 1);
        if (lastChild != null)
        {
            m_FrontUI = lastChild.gameObject.GetComponent<BaseUI>();
        }
    }

    // 현재 활성화된 유아이 가져오기
    public BaseUI GetActiveUI<T>()
    {
        var uiType = typeof(T);
        return m_OpenUIPool.ContainsKey(uiType) ? m_OpenUIPool[uiType].GetComponent<BaseUI>() : null;
    }

    public bool ExitsOpenUI()
    {
        return m_FrontUI != null;
    }

    public BaseUI GetCurrentFrontUI()
    {
        return m_FrontUI;
    }
    
    // 유아이 전체 닫기
    public void CloseAllOpenUI()
    {
        // m_FrontUI가 닫힐때 마다 계속 변경될테니 그걸 통해서 반복
        while (m_FrontUI != null)
        {
            m_FrontUI.CloseUI(true);
        }
    }
}
