using System;
using UnityEngine;
using UnityEngine.Audio;
using static UnityEngine.Rendering.DebugUI;

public class PlayerSettings : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    private float defaultMasterAudioValue = 0;
    private float defaultMusicAudioValue = -20.0f;
    private float defaultSFXAudioValue = 0;
    private float defaultUIAudioValue = -20.0f;
    private float defaultVoiceAudioValue = -10.0f;

    private void Start()
    {
        LoadSettings();
    }

    private void LoadSettings()
    {
        if (audioMixer == null) return;

        /*audioMixer.GetFloat("Master", out defaultMasterAudioValue);
        audioMixer.GetFloat("Music", out defaultMusicAudioValue);
        audioMixer.GetFloat("SFX", out defaultSFXAudioValue);
        audioMixer.GetFloat("UI", out defaultUIAudioValue);
        audioMixer.GetFloat("Voice", out defaultVoiceAudioValue);*/

        if (PlayerPrefs.HasKey("MasterAudio"))
            audioMixer.SetFloat("Master", PlayerPrefs.GetFloat("MasterAudio"));

        if (PlayerPrefs.HasKey("MusicAudio"))
            audioMixer.SetFloat("Music", PlayerPrefs.GetFloat("MusicAudio"));

        if (PlayerPrefs.HasKey("SFXAudio"))
            audioMixer.SetFloat("SFX", PlayerPrefs.GetFloat("SFXAudio"));

        if (PlayerPrefs.HasKey("UIAudio"))
            audioMixer.SetFloat("UI", PlayerPrefs.GetFloat("UIAudio"));

        if (PlayerPrefs.HasKey("VoiceAudio"))
            audioMixer.SetFloat("Voice", PlayerPrefs.GetFloat("VoiceAudio"));
    }

    public void ResetToDefault()
    {
        if (audioMixer == null) return;

        audioMixer.SetFloat("Master", defaultMasterAudioValue);
        audioMixer.SetFloat("Music", defaultMusicAudioValue);
        audioMixer.SetFloat("SFX", defaultSFXAudioValue);
        audioMixer.SetFloat("UI", defaultUIAudioValue);
        audioMixer.SetFloat("Voice", defaultVoiceAudioValue);

        PlayerPrefs.DeleteKey("MasterAudio");
        PlayerPrefs.DeleteKey("MusicAudio");
        PlayerPrefs.DeleteKey("SFXAudio");
        PlayerPrefs.DeleteKey("UIAudio");
        PlayerPrefs.DeleteKey("VoiceAudio");
        PlayerPrefs.Save();
    }

    public float InputSensitivity
    {
        get
        {
            return 1.0f;
        }

        set
        {
            PlayerPrefs.SetFloat("InputSensitivity", value);
        }
    }

    public float MasterAudioValue
    {
        get
        {
            float masterAudioValue = 0;
            if (audioMixer != null) audioMixer.GetFloat("Master", out masterAudioValue);
            return masterAudioValue;
        }

        set
        {
            if (audioMixer != null) audioMixer.SetFloat("Master", value);
            PlayerPrefs.SetFloat("MasterAudio", value);
            PlayerPrefs.Save();
        }
    }

    public float MusicAudioValue
    {
        get
        {
            float audioValue = 0;
            if (audioMixer != null) audioMixer.GetFloat("Music", out audioValue);
            return audioValue;
        }

        set
        {
            if (audioMixer != null) audioMixer.SetFloat("Music", value);
            PlayerPrefs.SetFloat("MusicAudio", value);
            PlayerPrefs.Save();
        }
    }

    public float SFXAudioValue
    {
        get
        {
            float audioValue = 0;
            if (audioMixer != null) audioMixer.GetFloat("SFX", out audioValue);
            return audioValue;
        }

        set
        {
            if (audioMixer != null) audioMixer.SetFloat("SFX", value);
            PlayerPrefs.SetFloat("SFXAudio", value);
            PlayerPrefs.Save();
        }
    }

    public float UIAudioValue
    {
        get
        {
            float audioValue = 0;
            if (audioMixer != null) audioMixer.GetFloat("UI", out audioValue);
            return audioValue;
        }

        set
        {
            if (audioMixer != null) audioMixer.SetFloat("UI", value);
            PlayerPrefs.SetFloat("UIAudio", value);
            PlayerPrefs.Save();
        }
    }

    public float VoiceAudioValue
    {
        get
        {
            float audioValue = 0;
            if (audioMixer != null) audioMixer.GetFloat("Voice", out audioValue);
            return audioValue;
        }

        set
        {
            if (audioMixer != null) audioMixer.SetFloat("Voice", value);
            PlayerPrefs.SetFloat("VoiceAudio", value);
            PlayerPrefs.Save();
        }
    }
}
