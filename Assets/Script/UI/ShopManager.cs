﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;
namespace FrameWork
{
    using FrameWork.Data;
    public class ShopManager : MonoBehaviour
    {
        public Transform cardPanel;
        public Transform cardCostParent;
        public GameObject cardPrefab;
        public static bool activeShop;
        public Sprite soldOut;
        public Image deleteCardButton;
        public static GameObject saveBeforeCard;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            List<CardJsonData> cardDatas = GameManager.Instance.dataManager.data.cardData.GetCardStat();
            int allCardCount = GameManager.Instance.dataManager.data.cardData.cardCollect.listcardData.Count;
            CardManager cardManager = GameManager.Instance.cardManager;
           
            for (int i = 0; i < 5; i++)
            {
                int randomCardIndex = Random.Range(0, allCardCount);
                int cost = cardDatas[randomCardIndex].cost;
                CardBase cardBase = Instantiate(cardPrefab, cardPanel).GetComponent<CardBase>();
                cardBase.gameObject.GetComponent<UnityEngine.EventSystems.EventTrigger>().enabled = false;
              
                cardBase.gameObject.AddComponent<Button>().onClick.AddListener(() => Addcard(cost,cardBase.gameObject,randomCardIndex));
                cardBase.cardManager = cardManager;
                cardBase.Init(cardDatas[randomCardIndex]);
                cardCostParent.GetChild(i).Find("CostText").GetComponent<TextMeshProUGUI>().text = "" + cost;

                ChangeSize(cardBase.gameObject);
                
            }

        }

        public void Addcard(int cost, GameObject card,int cardDataIndex)
        {
            var characterData = GameManager.Instance.dataManager.data.characterData.GetCharacterStat();
            int gold = characterData.gold;
            int cardShopIndex = card.transform.GetSiblingIndex();

            if(gold >= cost)
            {
                Destroy(card);
                Destroy(cardCostParent.GetChild(cardShopIndex).gameObject);
                characterData.listHaveCard.Add(cardDataIndex);
                characterData.gold -= cost;
            }
           
        }

        public void DeleteCardButton()
        {
            var characterData = GameManager.Instance.dataManager.data.characterData.GetCharacterStat();
            int gold = characterData.gold;
            int cost = 75;

            if (gold >= cost)
            {
                activeShop = true;
                GameManager.Instance.ingameUI.deckButton.onClick.Invoke();
                characterData.gold -= cost;
                deleteCardButton.sprite = soldOut;
            }
        }

        public static void ChangeSize(GameObject Obj)
        {
            EventTrigger eventTrigger = Obj.AddComponent<EventTrigger>();
            EventTrigger.Entry entry_PointerEnter = new EventTrigger.Entry();
            entry_PointerEnter.eventID = EventTriggerType.PointerEnter;
            entry_PointerEnter.callback.AddListener((data) => { OnPointerEnter((PointerEventData)data); });
            eventTrigger.triggers.Add(entry_PointerEnter);

            EventTrigger.Entry entry_PointerExit = new EventTrigger.Entry();
            entry_PointerExit.eventID = EventTriggerType.PointerExit;
            entry_PointerExit.callback.AddListener((data) => { OnPointerExit((PointerEventData)data); });
            eventTrigger.triggers.Add(entry_PointerExit);

        }


        public static void OnPointerEnter(PointerEventData data)
        {
            data.pointerEnter.gameObject.transform.DOScale(0.7f, 0.25f);// = new Vector3(0.7f, 0.7f, 0.7f);
           saveBeforeCard = data.pointerEnter.gameObject;    
        }

        public static void OnPointerExit(PointerEventData data)
        {    
            saveBeforeCard.transform.DOScale(0.5f, 0.5f);
        }
    }
}
