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
        [SerializeField] private TextMeshProUGUI CardName;
        [SerializeField] private TextMeshProUGUI CardText;
        [SerializeField] private TextMeshProUGUI CardPoint;
        [SerializeField] private TextMeshProUGUI CardType;
        [SerializeField] private Image CardImg;

        [SerializeField] private CardSorting cardSorting;

        [SerializeField] List<Sprite> cardSprites;
        public Vector2 defaltPos;
        public int cardIndex;

        Vector2 defaultPos;
        private bool onDrag;

        private void Start()
        {
            Init();
            onDrag = false;
        }

        private void Init()
        {
            CardName.text = "Test";
            CardText.text = "Test Text";
            CardPoint.text = "5";
            CardType.text = "Attack";
            CardImg.sprite = cardSprites[0];
        }

        private void FixedUpdate()
        {
            if(onDrag)
            {
                transform.position = Input.mousePosition;
            }
        }

        public void OnPointDown()
        {
            if (!onDrag)
            {
                if (Input.GetMouseButton(0))
                {
                    defaltPos = transform.localPosition;
                    onDrag = true;
                    transform.rotation = Quaternion.identity;
                    cardSorting.SelectCard(this);
                }
            }
            else
            {
                if (Input.GetMouseButton(1))
                {
                    onDrag = false;
                    transform.localPosition = defaltPos;
                    cardSorting.DefaltCardSorting();
                }
            }
        }
    }
}