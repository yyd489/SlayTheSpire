﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FrameWork
{
    using FrameWork.Data;
      

    public class DeckPopUI : MonoBehaviour
    {
        public GameObject cardPrefab;
        public Transform cardPanel;

        public static List<int> listDeckCards = new List<int>();

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            List<CardJsonData> cardDatas = GameManager.Instance.dataManager.data.cardData.GetCardStat();
            for (int i = 0; i < listDeckCards.Count; i++)
            {
                GameObject cardObj = Instantiate(cardPrefab, cardPanel);

                if(ShopManager.activeShop ==true)
                {
                    cardObj.AddComponent<Button>().onClick.AddListener(() => DeleteChanceCard(cardObj));
                }

                ShopManager.ChangeSize(cardObj, ItemType.Card);
                Destroy(cardObj.GetComponent<UnityEngine.EventSystems.EventTrigger>());
                cardObj.GetComponent<CardBase>().Init(cardDatas[listDeckCards[i]]);
            }

           // DeleteChanceCard();
        }

        public void DeleteChanceCard(GameObject card)
        {
            List<int> listDeckCards = GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.listHaveCard;
            listDeckCards.RemoveAt(card.transform.GetSiblingIndex());
            Destroy(card);
            ShopManager.activeShop = false;
            Destroy(this.transform.parent.gameObject);
        }



    }

}
