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

        void Start()
        {
            DefaltCardSorting();
        }

        public void DefaltCardSorting()
        {
            int cardCount = cards.Count;
            int halfCount = (int)(cardCount * 0.5f);

            for (int i = 0; i < cardCount; i++)
            {
                cards[i].cardIndex = i;
                int tempInt = i - halfCount;
                float xPos = tempInt * 180f;
                float yPos = 0f;

                cards[i].transform.rotation = Quaternion.identity;
                cards[i].transform.Rotate(new Vector3(0f, 0f, 0f - tempInt * 5f));

                if (tempInt < 0) tempInt *= -1;

                for (int j = 1; j <= tempInt; j++)
                {
                    yPos -= j * 15f;
                }

                cards[i].transform.localPosition = new Vector2(xPos, yPos);
            }
        }

        public void SelectCard(CardBase selectCard)
        {
            int cardIndex = selectCard.cardIndex;

            if (cardIndex > 0)
            {
                Vector3 movePos = cards[cardIndex - 1].transform.localPosition - new Vector3(50f, 0f);
                cards[cardIndex - 1].transform.DOLocalMove(movePos, 0.5f);
            }

            if(cardIndex + 1 <= cards.Count )
            {
                Vector3 movePos = cards[cardIndex + 1].transform.localPosition + new Vector3(50f, 0f);
                cards[cardIndex + 1].transform.DOLocalMove(movePos, 0.5f);
            }
        }

    }
}
