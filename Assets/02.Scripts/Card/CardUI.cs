using System;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    /// <summary>
    /// 덱, 핸드, 버린더미 표시 UI.
    /// 사실상 플레이어만 들고있을거임.
    /// </summary>
    public class CardUI : MonoBehaviour
    {
        [SerializeField] HandView handView;
        [SerializeField] CardList cardList;
        [SerializeField] CardList trashList;
        [SerializeField] private DeckService deckService;

        private void Start()
        {
            handView.SetCardSetting(deckService);
            cardList.Init(deckService.Hand);
            trashList.Init(deckService.Trash);
        }
    }
}