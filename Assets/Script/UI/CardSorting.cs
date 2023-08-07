using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

namespace FrameWork
{
    public class CardSorting : MonoBehaviour
    {
        [SerializeField] List<CardBase> cards;
        [SerializeField] CardBase selectCard;
        [SerializeField] GameObject usedCardBox;

        void Start()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                cards.Add(transform.GetChild(i).GetComponent<CardBase>());
            }
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
            selectCard.Init();

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
            useCard.transform.parent = usedCardBox.transform;
            selectCard.gameObject.SetActive(false);
            cards.Remove(useCard);
            DefaltCardSorting();
        }
    }
}
