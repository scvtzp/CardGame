using I2.Loc;
using TMPro;

namespace DefaultNamespace
{
    public static class SmartLocalize
    {
        /// <summary>
        /// 로컬라이징의 특정 문자열을 특정 번역값으로 대체해주는 함수 
        /// </summary>
        /// <param name="changeKey">대괄호에 쌓인 변경될 텍스트</param>
        /// <param name="changeValueKey">변경할 텍스트의 번역 키</param>
        public static void SetSmartLocString(this TextMeshProUGUI text, string localizekey ,string changeKey, string changeValueKey)
        {
            text.text = LocalizationManager.GetTranslation(localizekey).Replace($"[{changeKey}]", LocalizationManager.GetTranslation(changeValueKey));
        }
        
        /// <summary>
        /// 로컬라이징의 특정 문자열을 특정 문자로 대체해주는 함수 
        /// </summary>
        /// <param name="changeKey">대괄호에 쌓인 변경될 텍스트</param>
        /// <param name="changeValue">변경할 텍스트</param>
        public static void SetSmartString(this TextMeshProUGUI text, string localizekey ,string changeKey, string changeValue)
        {
            text.text = LocalizationManager.GetTranslation(localizekey).Replace($"[{changeKey}]", changeValue);
        }
    }
}