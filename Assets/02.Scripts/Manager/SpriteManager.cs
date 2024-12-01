using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Manager.Generics;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _02.Scripts.Manager
{
    public class SpriteManager : Singleton<SpriteManager>
    {
        private readonly Dictionary<string, Sprite> _spriteDic = new Dictionary<string, Sprite>();
        
        public async Task<Sprite> GetSpriteAsync(string spriteName)
        {
            if (_spriteDic.TryGetValue(spriteName, out Sprite sprite))
            {
                return sprite;
            }

            try
            {
                // 어드레서블에서 비동기로 스프라이트 로드
                var handle = Addressables.LoadAssetAsync<Sprite>($"Assets/04.Images/CardImage/{spriteName}.jpg");
                sprite = await handle.Task;

                // 로드된 스프라이트를 딕셔너리에 저장
                _spriteDic[spriteName] = sprite;

                return sprite;
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"어드레서블에서 {spriteName}를 찾을 수 없습니다. 에러: {ex.Message}");
                return null;
            }
        }
    }
}