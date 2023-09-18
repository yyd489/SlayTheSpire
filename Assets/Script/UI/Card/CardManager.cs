using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace FrameWork
{
    using FrameWork.Data;

    public class CardManager : MonoBehaviour
    {
        [SerializeField] List<CardBase> cards;
        [SerializeField] CardBase selectCard;
        private int selectIndex;

        [SerializeField] GameObject usedCardBox;
        private ObjectPool cardPool;

        [SerializeField] private List<Sprite> cardImages;
        [HideInInspector] public Dictionary<string, Sprite> dicCardImages = new Dictionary<string, Sprite>();

        // 덱
        public Queue<int> queMainDeck = new Queue<int>();
        public List<int> listUseDeck = new List<int>();
        private List<int> listHandCard = new List<int>();


        public void Init()
        {
            cardPool = usedCardBox.GetComponent<ObjectPool>();
            //await UniTask.WaitUntil(() => GameManager.Instance.dataManager.data.cardData.cardCollect != null);

            List<CardJsonData> cardDatas = GameManager.Instance.dataManager.data.cardData.GetCardStat();

            for (int i = 0; i < cardDatas.Count; i++)
            {
                dicCardImages.Add(cardDatas[i].cardName, cardImages[i]);
            }
        }

        public void StageStart()
        {
            List<CardJsonData> cardDatas = GameManager.Instance.dataManager.data.cardData.GetCardStat();
            List<int> listHaveCard = GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.listHaveCard.ToList();

            int haveCount = listHaveCard.Count;

            for (int i = 0; i < haveCount; i++)
            {
                int random = Random.Range(0, listHaveCard.Count);
                queMainDeck.Enqueue(listHaveCard[random]);
                listHaveCard.RemoveAt(random);
            }

            for (int i = 0; i < 5; i++)
            {
                int index = queMainDeck.Dequeue();
                listHandCard.Add(index);

                CardBase tempCard = cardPool.GetObject(this.transform).GetComponent<CardBase>();                
                CardJsonData newCard = cardDatas[index];
                tempCard.Init(newCard);
                cards.Add(tempCard);
            }

            GameManager.Instance.inGameUIManager.RefreshDeckCountText(queMainDeck.Count, listUseDeck.Count);
            DefaltCardSorting();
        }

        public void DefaltCardSorting()
        {
            selectCard.gameObject.SetActive(false);

            int cardCount = cards.Count;
            int halfCount = (int)(cardCount * 0.5f);
            bool isEvenNumber = (cardCount % 2 == 0);
            if (isEvenNumber) halfCount--;

            for (int i = 0; i < cardCount; i++)
            {
                cards[i].cardIndex = i;
                int tempInt = i - halfCount;
                float xPos = 0f;
                float yPos = 0f;
                float zRot = 0f;

                // x좌표
                if (isEvenNumber)
                {
                    if (cardCount * 0.5f > halfCount)
                    {
                        xPos = -90f;
                        if (i == halfCount) halfCount++;
                    }
                    else xPos = 90f;
                }

                xPos += tempInt * 180f;

                // 각도
                zRot -= tempInt * 5f;
                if (isEvenNumber)
                {
                    if (xPos < 0) zRot += 5f;
                    else if (xPos > 0) zRot -= 5f;
                }

                cards[i].transform.rotation = Quaternion.identity;
                cards[i].transform.Rotate(new Vector3(0f, 0f, zRot));
                
                // y좌표
                if (tempInt < 0) tempInt *= -1;

                for (int j = 1; j <= tempInt; j++)
                {
                    yPos -= j * 15f;
                }

                cards[i].transform.localPosition = new Vector2(xPos, yPos);
            }
        }

        public void SelectCard(CardBase useCard)
        {
            int cardIndex = useCard.cardIndex;
            GameManager.Instance.playerControler.selectCard = useCard;
            useCard.gameObject.SetActive(false);
            selectCard.gameObject.SetActive(true);
            selectCard.Init(useCard.cardData);

            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i] == useCard)
                {
                    selectIndex = listHandCard[i];
                    break;
                }
            }

            if (cardIndex > 0)
            {
                Vector3 movePos = cards[cardIndex - 1].transform.localPosition - new Vector3(50f, 0f);
                cards[cardIndex - 1].transform.localPosition = movePos;
            }

            if(cardIndex + 1 < cards.Count )
            {
                Vector3 movePos = cards[cardIndex + 1].transform.localPosition + new Vector3(50f, 0f);
                cards[cardIndex + 1].transform.localPosition = movePos;
            }
        }

        public void UseCard(CardBase useCard)
        {
            selectCard.gameObject.SetActive(false);
            if (GameManager.Instance.battleManager.battleState != BattleState.EndBattle)
            {
                cardPool.ReturnObject(useCard.gameObject);

                if (!useCard.cardData.canDelete) listUseDeck.Add(selectIndex);
                listHandCard.Remove(selectIndex);
                cards.Remove(useCard);
                GameManager.Instance.inGameUIManager.RefreshDeckCountText(queMainDeck.Count, listUseDeck.Count);
                DefaltCardSorting();
            }
        }

        public void DrawCard()
        {
            List<CardJsonData> cardDatas = GameManager.Instance.dataManager.data.cardData.GetCardStat();

            CardBase tempCard = cardPool.GetObject(this.transform).GetComponent<CardBase>();
            
            if (queMainDeck.Count <= 0)
                ReloadCardDeck();

            int index = queMainDeck.Dequeue();
            listHandCard.Add(index);
            tempCard.Init(cardDatas[index]);

            cards.Add(tempCard);
            GameManager.Instance.inGameUIManager.RefreshDeckCountText(queMainDeck.Count, listUseDeck.Count);
        }

        public void RemovePlayerCard()
        {
            for (int i = 0; i < cards.Count; i++)
            {
                cardPool.ReturnObject(cards[i].gameObject);
                listUseDeck.Add(listHandCard[i]);
            }

            listHandCard.Clear();
            cards.Clear();
            GameManager.Instance.inGameUIManager.RefreshDeckCountText(queMainDeck.Count, listUseDeck.Count);
        }

        public void ReloadPlayerCard()
        {
            List<CardJsonData> cardDatas = GameManager.Instance.dataManager.data.cardData.GetCardStat();

            CardBase tempCard;
            for(int i = 0; i < 5; i++)
            {
                if (queMainDeck.Count == 0) ReloadCardDeck();

                int index = queMainDeck.Dequeue();
                listHandCard.Add(index);
                CardJsonData tempDeckCard = cardDatas[index];

                tempCard = cardPool.GetObject(this.transform).GetComponent<CardBase>();
                tempCard.Init(tempDeckCard);
                cards.Add(tempCard);
            }

            GameManager.Instance.inGameUIManager.RefreshDeckCountText(queMainDeck.Count, listUseDeck.Count);
            DefaltCardSorting();
        }

        public void ReloadCardDeck()
        {
            int listCount = listUseDeck.Count;

            for (int i = 0; i < listCount; i++)
            {
                int random = Random.Range(0, listUseDeck.Count);

                queMainDeck.Enqueue(listUseDeck[random]);

                listUseDeck.RemoveAt(random);
            }
        }

        public void ResetCardDecks()
        {
            RemovePlayerCard();
            queMainDeck.Clear();
            listUseDeck.Clear();
            cards.Clear();
        }
    }
}
