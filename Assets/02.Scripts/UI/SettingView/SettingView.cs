using Manager;
using UI.UIBase;
using UnityEngine;
using UnityEngine.UI;

namespace UI.SettingView
{
    public class SettingView : ViewBase
    {
        [SerializeField] Slider bgmSlider;
        [SerializeField] Slider sfxSlider;

        public override void Init()
        {
            bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
            bgmSlider.onValueChanged.AddListener(SetBGMVolume);
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }

        public void SetBGMVolume(float value)
        {
            SoundManager.Instance.SetBGMVolume(value);
            PlayerPrefs.SetFloat("BGMVolume", value);
        }

        public void SetSFXVolume(float value)
        {
            SoundManager.Instance.SetSFXVolume(value);
            PlayerPrefs.SetFloat("SFXVolume", value);
        }
    }
}