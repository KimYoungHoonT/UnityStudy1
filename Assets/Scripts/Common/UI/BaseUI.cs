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
            // m_UIOpenAnim 이 있으면 플레이 해줘라
            m_UIOpenAnim.Play();
        }
        
        // 나머지 함수 걸어준거 있으면 함수 호출
        m_OnShow?.Invoke();
        // 1회성 콜백 보장을 위함
        // 메모리 해제 (참조 제거)
        m_OnShow = null;
    }

    public virtual void CloseUI(bool isCloaseAll = false)
    {
        // 올 클로즈가 아니라면 해당하는 유아이에 대해서만 진행
        if (isCloaseAll == false)
        {
            m_OnClose?.Invoke();
        }
        m_OnClose = null;

        UIManager.Instance.CloseUI(this);
    }

    // 유아이에 닫기 버튼을 위한 인터페이스
    public virtual void OnClickCloseButton()
    {
        // 버튼 클릭시 효과음 출력
        AudioManager.Instance.PlaySFX(SFX.ui_button_click);
        CloseUI();
    }
}
