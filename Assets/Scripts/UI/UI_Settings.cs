using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_Settings : MonoBehaviour
{
    [SerializeField] PlayerSettings settings;
    [SerializeField] Slider inputSensitivitySlider;
    [SerializeField] Slider masterAudioSlider;
    [SerializeField] Slider musicAudioSlider;
    [SerializeField] Slider sfxAudioSlider;
    [SerializeField] Slider uiAudioSlider;
    [SerializeField] Slider voiceAudioSlider;

    private void OnEnable()
    {
        if (settings == null) return;

        UpdateSliderValues();

        inputSensitivitySlider.onValueChanged.AddListener(OnInputSensitivitySliderChange);
        masterAudioSlider.onValueChanged.AddListener(OnMasterAudioSliderChange);
        musicAudioSlider.onValueChanged.AddListener(OnMusicAudioSliderChange);
        sfxAudioSlider.onValueChanged.AddListener(OnSFXAudioSliderChange);
        uiAudioSlider.onValueChanged.AddListener(OnUIAudioSliderChange);
        voiceAudioSlider.onValueChanged.AddListener(OnVoiceAudioSliderChange);
    }

    private void UpdateSliderValues()
    {
        inputSensitivitySlider.value = settings.InputSensitivity;
        masterAudioSlider.value = settings.MasterAudioValue;
        musicAudioSlider.value = settings.MusicAudioValue;
        sfxAudioSlider.value = settings.SFXAudioValue;
        uiAudioSlider.value = settings.UIAudioValue;
        voiceAudioSlider.value = settings.VoiceAudioValue;
    }

    private void OnDisable()
    {
        inputSensitivitySlider.onValueChanged.RemoveListener(OnInputSensitivitySliderChange);
        masterAudioSlider.onValueChanged.RemoveListener(OnMasterAudioSliderChange);
        musicAudioSlider.onValueChanged.RemoveListener(OnMusicAudioSliderChange);
        sfxAudioSlider.onValueChanged.RemoveListener(OnSFXAudioSliderChange);
        uiAudioSlider.onValueChanged.RemoveListener(OnUIAudioSliderChange);
        voiceAudioSlider.onValueChanged.RemoveListener(OnVoiceAudioSliderChange);
    }

    public void OnBackButtonPress()
    {
        gameObject.SetActive(false);
    }

    public void OnResetDefaultButtonPress()
    {
        settings.ResetToDefault();
        UpdateSliderValues();
    }

    public void OnInputSensitivitySliderChange(float newValue)
    {
        if (settings == null) return;
        settings.InputSensitivity = newValue;
    }

    public void OnMasterAudioSliderChange(float newValue)
    {
        if (settings == null) return;
        settings.MasterAudioValue = newValue;
    }

    public void OnMusicAudioSliderChange(float newValue)
    {
        if (settings == null) return;
        settings.MusicAudioValue = newValue;
    }

    public void OnSFXAudioSliderChange(float newValue)
    {
        if (settings == null) return;
        settings.SFXAudioValue = newValue;
    }

    public void OnUIAudioSliderChange(float newValue)
    {
        if (settings == null) return;
        settings.UIAudioValue = newValue;
    }

    public void OnVoiceAudioSliderChange(float newValue)
    {
        if (settings == null) return;
        settings.VoiceAudioValue = newValue;
    }
}
