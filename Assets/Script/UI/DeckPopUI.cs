using System.Collections;
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


        private void Start()
        {
            Init();
        }

        public void Init()
        {
           
            List<int> listDeckCards = GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.listHaveCard;


            List<CardJsonData> cardDatas = GameManager.Instance.dataManager.data.cardData.GetCardStat();
            for (int i = 0; i < listDeckCards.Count; i++)
            {
                GameObject cardObj = Instantiate(cardPrefab, cardPanel);
                cardObj.GetComponent<CardBase>().cardManager = GameManager.Instance.cardManager;

                if(ShopManager.activeShop ==true)
                {
                    cardObj.AddComponent<Button>().onClick.AddListener(() => DeleteChanceCard(cardObj));
                }
                cardObj.GetComponent<UnityEngine.EventSystems.EventTrigger>().enabled = false;
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
