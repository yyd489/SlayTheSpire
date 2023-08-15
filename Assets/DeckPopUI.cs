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
            var aa = GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.listHaveCard;
         
            List<CardJsonData> cardDatas; cardDatas = GameManager.Instance.dataManager.data.cardData.GetCardStat();
            for (int i = 0; i < aa.Count; i++)
            {
                GameObject cardObj = Instantiate(cardPrefab, cardPanel);
                cardObj.GetComponent<Button>().enabled = false;
                cardObj.GetComponent<UnityEngine.EventSystems.EventTrigger>().enabled = false;

                cardObj.GetComponent<CardBase>().cardManager = GameManager.Instance.cardManager;
                cardObj.GetComponent<CardBase>().Init(cardDatas[i]);
                
            }

        }

    }
}
