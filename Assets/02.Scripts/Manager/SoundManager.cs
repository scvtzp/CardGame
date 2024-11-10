using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Generics;

namespace Manager
{
    public class SoundManager : Singleton<SoundManager>
    {
        [Header("Audio Sources")] 
        [SerializeField] private AudioSource bgmSource; // 배경음용
        [SerializeField] private AudioSource sfxSource; // 효과음용

        private readonly Dictionary<string, AudioClip> _soundDic = new Dictionary<string, AudioClip>();
        
        // 효과음 재생 (어드레서블에서 로드)
        public void PlaySfx(string sfxName)
        {
            if (_soundDic.TryGetValue(sfxName, out AudioClip clip))
                sfxSource.PlayOneShot(clip);
            
            else
            {
                Addressables.LoadAssetAsync<AudioClip>($"Assets/05.Sounds/{sfxName}.mp3").Completed += handle =>
                {
                    if (handle.Status == AsyncOperationStatus.Succeeded)
                    {
                        clip = handle.Result;
                        _soundDic[sfxName] = clip;
                        sfxSource.PlayOneShot(clip);
                    }
                    else
                        Debug.LogWarning($"어드레서블에서 {sfxName} 효과음을 찾을 수 없습니다.");
                };
            }
        }

        public void PlayBgm(string sfxName)
        {
            if (_soundDic.TryGetValue(sfxName, out AudioClip clip))
            {
                bgmSource.clip = clip;
                bgmSource.loop = true;
                bgmSource.Play();
            }
            
            else
            {
                Addressables.LoadAssetAsync<AudioClip>($"Assets/05.Sounds/{sfxName}.mp3").Completed += handle =>
                {
                    if (handle.Status == AsyncOperationStatus.Succeeded)
                    {
                        clip = handle.Result;
                        _soundDic[sfxName] = clip;
                        bgmSource.clip = clip;
                        bgmSource.loop = true;
                        bgmSource.Play();
                    }
                    else
                        Debug.LogWarning($"어드레서블에서 {sfxName} 효과음을 찾을 수 없습니다.");
                };
            }
        }
    }
}