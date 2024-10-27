using System;
using System.Collections.Generic;
using DefaultNamespace;
using Manager;
using UI.ScrollView;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CardList : MonoBehaviour
    {
        [SerializeField] private CardListScrollView scrollView;

        private List<CardData> _cards;
        
        public void Init(List<CardData> cards)
        {
            _cards = cards;
            GetComponent<Button>().onClick.AddListener(OnPressedButton);
        }

        private void OnPressedButton()
        {
            if (scrollView.gameObject.activeSelf)
                scrollView.gameObject.SetActive(false);
            
            else
            {
                scrollView.Init(_cards);
                scrollView.gameObject.SetActive(true);
            }
        }
    }
}