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
        public TextMeshProUGUI CardName;
        public TextMeshProUGUI CardText;
        public TextMeshProUGUI CardPoint;
        public TextMeshProUGUI CardType;
        public Image CardImg;

        public CardSorting cardSorting;
        private Image image;

        [SerializeField] List<Sprite> cardSprites;
        public int cardIndex;

        // 카드 선택 & 드래그
        public Vector2 defaultPos;

        // 레이캐스트
        private float maxDistance = 100f;
        private Vector3 mousePos;

        private bool onDrag;

        private void Start()
        {
            Init();
            image = GetComponent<Image>();
            onDrag = false;
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
                    onDrag = true;
                    defaultPos = transform.localPosition;
                    transform.rotation = Quaternion.identity;
                    cardSorting.SelectCard(this);
                }
            }

            GameManager.Instance.playerControler.onDrag = onDrag;
        }

        public void CancleDrag()
        {
            onDrag = false;
            transform.localPosition = defaultPos;
            cardSorting.DefaltCardSorting();
        }
    }
}