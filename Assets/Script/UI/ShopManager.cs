using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace FrameWork
{
    using FrameWork.Data;
    public class ShopManager : MonoBehaviour
    {
        public Transform cardPanel;
        public Transform cardCostParent;
        public GameObject cardPrefab;
        public static bool activeShop;

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
            activeShop = true;
            GameManager.Instance.ingameUI.deckButton.onClick.Invoke();
          
        }
    }
}
