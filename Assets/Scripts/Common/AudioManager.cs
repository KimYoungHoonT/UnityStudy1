using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum BGM
{
    lobby,
    COUNT
}

public enum SFX
{
    chapter_clear,
    stage_clear,
    ui_button_click,
    COUNT
}

public class AudioManager : SingletonBehaviour<AudioManager>
{
    public Transform BGMTransform;
    public Transform SFXTransform;

    private const string AUDIO_PATH = "Audio";

    private Dictionary<BGM, AudioSource> m_BGMPlayer = new Dictionary<BGM, AudioSource>();
    private AudioSource m_CurrentBGMSource;


    private Dictionary<SFX, AudioSource> m_SFXPlayer = new Dictionary<SFX, AudioSource>();

    protected override void Init()
    {
        base.Init();

        LoadBGMPlayer();
        LoadSFXPlayer();
    }

    // 모든 BGM 파일 목록을 순회하여, 게임오브젝트 생성 및 오디오 소스 컴퍼넌트 추가, 해당 BGM 연동
    private void LoadBGMPlayer() // 리소스를 불러오는 단계
    {
        for (int i = 0; i < (int)BGM.COUNT; i++)
        {
            string audioName = ((BGM)i).ToString();
            string pathString = $"{AUDIO_PATH}/{audioName}";
            AudioClip audioClip = Resources.Load<AudioClip>(pathString);
            if (audioClip == null)
            {
                Logger.LogError($"{audioName} clip does not exist.");
                continue;
            }

            GameObject newGo = new GameObject(audioName);
            AudioSource newAudioSource = newGo.AddComponent<AudioSource>();
            newAudioSource.clip = audioClip;
            newAudioSource.loop = true;
            newAudioSource.playOnAwake = false;

            newGo.transform.parent = BGMTransform;

            m_BGMPlayer[(BGM)i] = newAudioSource;
        }
    }

    private void LoadSFXPlayer() // 리소스를 불러오는 단계
    {
        for (int i = 0; i < (int)SFX.COUNT; i ++)
        {
            string audioName = ((SFX)i).ToString();
            string pathString = $"{AUDIO_PATH}/{audioName}";
            AudioClip audioClip = Resources.Load<AudioClip>(pathString);
            if (audioClip == null)
            {
                Logger.LogError($"{audioName} clip does not exist.");
                continue;
            }

            GameObject newGo = new GameObject(audioName);
            AudioSource newAudioSource = newGo.AddComponent<AudioSource>();
            newAudioSource.clip = audioClip;
            newAudioSource.loop = false;
            newAudioSource.playOnAwake = false;

            newGo.transform.parent = SFXTransform;

            m_SFXPlayer[(SFX)i] = newAudioSource;
        }
    }

    public void PlayBGM(BGM bgm) // BGM 실행시키기
    {
        if (m_CurrentBGMSource != null) // 현재 플레이 하는 BGM 이 있다면
        {
            m_CurrentBGMSource.Stop(); // 완전정지
            m_CurrentBGMSource = null;
        }

        if (m_BGMPlayer.ContainsKey(bgm) == false) // 플레이 하려는 BGM 이 없다면
        {
            Logger.LogError($"Invalid cilp name. {bgm}");
            return;
        }

        m_CurrentBGMSource = m_BGMPlayer[bgm];
        m_CurrentBGMSource.Play();
    }

    public void PauseBGM() // 일시정지
    {
        if (m_CurrentBGMSource != null)
            m_CurrentBGMSource.Pause();
    }

    public void ResumeBGM() // 일시정지 해제
    {
        if (m_CurrentBGMSource != null)
            m_CurrentBGMSource.UnPause();
    }

    public void StopBGM() // 완전정지
    {
        if (m_CurrentBGMSource != null)
            m_CurrentBGMSource.Stop();
    }

    public void PlaySFX(SFX sfx)
    {
        if (m_SFXPlayer.ContainsKey(sfx) == false)
        {
            Logger.LogError($"Invalid cilp name. {sfx}");
            return;
        }

        m_SFXPlayer[sfx].Play();
    }

    public void Mute()
    {
        foreach (var audioSourceItem in m_BGMPlayer)
        {
            audioSourceItem.Value.volume = 0f;
        }

        foreach (var audioSourceItem in m_SFXPlayer)
        {
            audioSourceItem.Value.volume = 0f;
        }
    }

    public void UnMute()
    {
        foreach (var audioSourceItem in m_BGMPlayer)
        {
            audioSourceItem.Value.volume = 1f;
        }

        foreach (var audioSourceItem in m_SFXPlayer)
        {
            audioSourceItem.Value.volume = 1f;
        }
    }
}
