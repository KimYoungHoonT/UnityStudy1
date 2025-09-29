using System;
using UnityEngine;

public class BaseUIData
{
    public Action OnShow;
    public Action OnClose;
}

public class BaseUI : MonoBehaviour
{
    public Animation m_UIOpenAnim;

    public Action m_OnShow;
    public Action m_OnClose;

    public virtual void Init(Transform anchor)
    {
        Logger.Log($"{GetType()}::Init");

        m_OnShow = null;
        m_OnClose = null;

        transform.SetParent(anchor);

        var rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = Vector3.zero;
        rectTransform.localScale = Vector3.zero;
        rectTransform.offsetMax = Vector3.zero;
        rectTransform.offsetMin = Vector3.zero;
    }

    public virtual void SetInfo(BaseUIData uiData)
    {
        Logger.Log($"{GetType()}::SetInfo");

        m_OnShow = uiData.OnShow;
        m_OnClose = uiData.OnClose;
    }

    public virtual void ShowUI()
    {
        // 1. !m_UIOpenAnim 는 m_UIOpenAnim가 Animator 컴퍼넌트 일경우 를 뜻함 
        // 2. 즉 현재 m_UIOpenAnim는 구버전인 Animation 을 사용중이기에 !m_UIOpenAnim 는 사용할 수 없음
        // 즉, 그냥 널체크 하는것임
        if (m_UIOpenAnim != null) 
        {
            m_UIOpenAnim.Play();
        }

        m_OnShow?.Invoke();
        m_OnShow = null;
    }

    public virtual void CloseUI(bool isCloaseAll = false)
    {
        if (isCloaseAll == false)
        {
            m_OnClose?.Invoke();
        }
        m_OnClose = null;

        UIManager.Instance.CloseUI(this);
    }

    public virtual void OnClickCloseButton()
    {
        AudioManager.Instance.PlaySFX(SFX.ui_button_click);
        CloseUI();
    }
}
