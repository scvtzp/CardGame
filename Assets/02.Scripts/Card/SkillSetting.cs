using System;
using System.Collections.Generic;
using Manager;
using UnityEngine;

namespace DefaultNamespace
{
    // 파일 이름 디폴트 덱 매니저로 하고 여기서 json으로 카드 변환. 추가까지 다 할까?
    public class SkillSetting : Singleton<SkillSetting>
    {
        private Dictionary<string, Type> skills = new Dictionary<string, Type>()
        {
            {"Damage",typeof(Damage)},
            {"Heal",typeof(Heal)},
        };

        Dictionary<string, List<Card>> defaultDeckSetting = new Dictionary<string, List<Card>>();
        
        public void GetDefaultDeck()
        {
            //데이터 형식: id, 코스트, 타겟, 스킬, 부가효과
            TextAsset csvFile = Resources.Load<TextAsset>("DefaultDeckSetting");
            string[] lines = csvFile.text.Split('\n');

            string id = "";
            foreach (string line in lines)
            {
                string[] columns = line.Split(',');

                // 첫칸에 값이 있음 = 카드 Id라는 뜻.
                // 이 이후로 다음 id가 등장하기 전까지의 모든 줄이 각각 하나의 카드.
                if (columns[0] != "")
                {
                    id = columns[0];
                    defaultDeckSetting.Add(id, new List<Card>());
                }
                else
                {
                    Card card = new Card();
                    
                    //여기서 카드 데이터 추가.
                    //스킬 string->class는 캐싱해둔 skills 사용.
                    defaultDeckSetting[id].Add(card);
                }
            }
        }
    }
}