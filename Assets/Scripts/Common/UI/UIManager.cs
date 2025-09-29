using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonBehaviour<UIManager>
{
    public Transform UICanvasTransform;
    public Transform CloseUITransform;

    private BaseUI m_FrontUI; // ���� ���� ���ִ� UI��
    private Dictionary<Type, GameObject> m_OpenUIPool = new Dictionary<Type, GameObject>(); // �����ִ� UI ������
    private Dictionary<Type, GameObject> m_ClosedUIPool = new Dictionary<Type, GameObject>(); // �����ִ� UI ������

    // ���⸦ ���ϴ� UIȭ���� ���� �ν��Ͻ� ��������
    private BaseUI GetUI<T>(out bool isAlreadyOpen)
    {
        Type uiType = typeof(T);

        BaseUI ui = null;
        isAlreadyOpen = false;

        // �̹� �ѹ� ��������� ���� �ִٸ�
        if (m_OpenUIPool.ContainsKey(uiType) == true)
        {
            ui = m_OpenUIPool[uiType].GetComponent<BaseUI>();
            isAlreadyOpen = true;
        }
        // �̹� �ѹ� ��������� ���� �ִٸ�
        else if (m_ClosedUIPool.ContainsKey(uiType) == true)
        {
            ui = m_ClosedUIPool[uiType].GetComponent<BaseUI>();
            m_ClosedUIPool.Remove(uiType);
        }
        // �ѹ��� �ش� UI �� �������� ���ٸ� �������ֱ�
        else
        {   
            GameObject uiObj = Instantiate(Resources.Load<GameObject>($"UI/{uiType}"));
            ui = uiObj.GetComponent<BaseUI>();
        }

        return ui;
    }

    // � UI ȭ���� ���� ���� �Լ�
    public void OpenUI<T>(BaseUIData uiData)
    {
        Type uiType = typeof(T);

        Logger.Log($"{GetType()}::OpenUI({uiType})");

        bool isAlreadyOpen = false;
        var ui = GetUI<T>(out isAlreadyOpen);

        // �����̸� ������ ������ �������� �ȉ�����
        if (ui == null)
        {
            Logger.LogError($"{uiType} does not exist.");
        }

        // �̹� �����ִ°� �� ������ �ϴ� ��Ȳ
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

    // �����ִ� UI�� �ݾ��ֱ�
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

    // ���� Ȱ��ȭ�� ������ ��������
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
    
    // ������ ��ü �ݱ�
    public void CloseAllOpenUI()
    {
        // m_FrontUI�� ������ ���� ��� ������״� �װ� ���ؼ� �ݺ�
        while (m_FrontUI != null)
        {
            m_FrontUI.CloseUI(true);
        }
    }
}
