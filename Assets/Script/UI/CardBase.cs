using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace FrameWork
{
    public class CardBase : MonoBehaviour
    {
        // 카드 UI
        public TextMeshProUGUI CardName;
        public TextMeshProUGUI CardText;
        public TextMeshProUGUI CardPoint;
        public TextMeshProUGUI CardType;
        public Image CardImg;

        // 카드 이미지 리소스 - 추후 이동필요
        [SerializeField] List<Sprite> cardSprites;

        // 카드정렬
        public CardSorting cardSorting;
        public int cardIndex;

        // 카드 선택 & 드래그
        public Vector2 defaultPos;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            CardName.text = "Test";
            CardText.text = "Test Text";
            CardPoint.text = "5";
            CardType.text = "Attack";
            CardImg.sprite = cardSprites[0];
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