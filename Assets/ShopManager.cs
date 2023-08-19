using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace FrameWork
{
    using FrameWork.Data;
    public class ShopManager : MonoBehaviour
    {
        public Transform cardPanel;
        public GameObject cardPrefab;

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
                CardBase cardBase = Instantiate(cardPrefab, cardPanel).GetComponent<CardBase>();
                cardBase.gameObject.GetComponent<UnityEngine.EventSystems.EventTrigger>().enabled = false;
                cardBase.gameObject.AddComponent<Button>();
                cardBase.cardManager = cardManager;
                cardBase.Init(cardDatas[Random.Range(0, allCardCount)]);
            }

        }

        public void Addcard()
        {

        }
        public void Deletecard()
        {

        }
    }
}
