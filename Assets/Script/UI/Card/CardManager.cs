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

        [SerializeField] GameObject usedCardBox;
        private ObjectPool cardPool;

        [SerializeField] private List<Sprite> cardImages;
        [HideInInspector] public Dictionary<string, Sprite> dicCardImages = new Dictionary<string, Sprite>();
        private List<CardJsonData> cardDatas;

        // 덱
        Queue<CardJsonData> queMainDeck = new Queue<CardJsonData>();
        [SerializeField] List<CardJsonData> listUseDeck = new List<CardJsonData>();

        public int GetMainDeckCount() { return queMainDeck.Count; }
        public int GetUseDeckCount() { return listUseDeck.Count - cards.Count; }


        public void Init()
        {
            cardPool = usedCardBox.GetComponent<ObjectPool>();
            //await UniTask.WaitUntil(() => GameManager.Instance.dataManager.data.cardData.cardCollect != null);

            cardDatas = GameManager.Instance.dataManager.data.cardData.GetCardStat();

            for (int i = 0; i < cardDatas.Count; i++)
            {
                dicCardImages.Add(cardDatas[i].cardName, cardImages[i]);
            }
            
            List<int> listDeckData = GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.listHaveCard;
            List<int> listHaveCard = listDeckData.ToList();

            int haveCount = listHaveCard.Count;

            for (int i = 0; i < haveCount; i++)
            {
                int random = Random.Range(0, listHaveCard.Count);
                queMainDeck.Enqueue(cardDatas[listHaveCard[random]]);
                listHaveCard.RemoveAt(random);
            }

            for (int i = 0; i < 5; i++)
            {
                cards.Add(cardPool.GetObject(this.transform).GetComponent<CardBase>());

                CardJsonData newCard = queMainDeck.Dequeue();
                listUseDeck.Add(newCard);
                cards[i].Init(newCard);
            }

            GameManager.Instance.battleManager.RefreshDeckCountText(GetMainDeckCount(), GetUseDeckCount());
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
            cardPool.ReturnObject(useCard.gameObject);
            selectCard.gameObject.SetActive(false);
            cards.Remove(useCard);
            GameManager.Instance.battleManager.RefreshDeckCountText(GetMainDeckCount(), GetUseDeckCount());
            DefaltCardSorting();
        }

        public void DrawCard()
        {
            CardBase tempCard;

            tempCard = cardPool.GetObject(this.transform).GetComponent<CardBase>();

            //tempCard.Init(cardDatas[0]);
            if (queMainDeck.Count <= 0)
                ReloadCardDeck();

            tempCard.Init(queMainDeck.Dequeue());

            cards.Add(tempCard);
            GameManager.Instance.battleManager.RefreshDeckCountText(GetMainDeckCount(), GetUseDeckCount());
        }

        public void RemovePlayerCard()
        {
            for (int i = 0; i < cards.Count; i++)
                cardPool.ReturnObject(cards[i].gameObject);

            cards.Clear();
            GameManager.Instance.battleManager.RefreshDeckCountText(GetMainDeckCount(), GetUseDeckCount());
        }

        public void ReloadPlayerCard()
        {
            CardBase tempCard;
            for(int i = 0; i < 5; i++)
            {
                if (queMainDeck.Count == 0) ReloadCardDeck();

                CardJsonData tempDeckCard = queMainDeck.Dequeue();

                if(!tempDeckCard.canDelete) listUseDeck.Add(tempDeckCard);

                tempCard = cardPool.GetObject(this.transform).GetComponent<CardBase>();
                tempCard.Init(cardDatas[i]);
                cards.Add(tempCard);
            }

            GameManager.Instance.battleManager.RefreshDeckCountText(GetMainDeckCount(), GetUseDeckCount());
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

            GameManager.Instance.battleManager.RefreshDeckCountText(GetMainDeckCount(), GetUseDeckCount());
        }
    }
}
