using System.Collections;
using TMPro;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    // �ΰ� ���� ������
    public Animation _logoAnim;
    public TextMeshProUGUI _logoText;

    // Ÿ��Ʋ�� ���� ������
    public GameObject Title;

    private void Awake()
    {
        _logoAnim.gameObject.SetActive(true);
        Title.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(LoadGameCoroutine());
    }

    private IEnumerator LoadGameCoroutine()
    {
        Logger.Log($"{GetType()}::LoadGameCoroutine");

        _logoAnim.Play();
        yield return new WaitForSeconds(_logoAnim.clip.length);

        _logoAnim.gameObject.SetActive(false);
        Title.SetActive(true);
    }
}
