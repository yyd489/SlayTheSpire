using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace FrameWork
{
    using FrameWork.Data;
    public class CardBase : MonoBehaviour
    {
        public CardJsonData cardData;
        // 카드 UI
        public TextMeshProUGUI CardName;
        public TextMeshProUGUI CardText;
        public TextMeshProUGUI CardPoint;
        public TextMeshProUGUI CardType;
        public Image CardImg;

        // 카드정렬
        public CardSorting cardSorting;
        public int cardIndex;

        // 카드 선택 & 드래그
        public Vector2 defaultPos;

        public void Init(CardJsonData data)
        {
            cardData = data;
            CardName.text = cardData.cardName;
            CardText.text = cardData.cardGuide;
            CardPoint.text = cardData.cardCost.ToString();
            CardType.text = cardData.cardType.ToString();
            CardImg.sprite = cardSorting.cardImages[cardData.imageIndex];
        }

        public void OnPointDown()
        {
            if (Input.GetMouseButton(0))
            {
                if (!GameManager.Instance.playerControler.onDrag)
                {
                    defaultPos = transform.localPosition;
                    transform.rotation = Quaternion.identity;
                    cardSorting.SelectCard(this);
                    GameManager.Instance.playerControler.onDrag = true;
                }
            }
        }

        public void CancleDrag()
        {
            transform.localPosition = defaultPos;
            cardSorting.DefaltCardSorting();
        }
    }
}