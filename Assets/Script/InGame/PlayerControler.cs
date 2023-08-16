﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FrameWork
{
    public class PlayerControler : MonoBehaviour
    {
        [SerializeField] private Texture2D cursorImg;

        // 캐릭터
        [HideInInspector] public CharacterBase playerCharacter;
        [HideInInspector] public CharacterBase targetCharacter;

        // 카드
        [HideInInspector] public CardBase selectCard;
        [HideInInspector] public bool onDrag;

        public void Init()
        {
            Cursor.SetCursor(cursorImg, Vector2.zero, CursorMode.Auto);
            selectCard = transform.Find("Card").transform.Find("SelectCard").GetComponent<CardBase>();
        }

        private void FixedUpdate()
        {
            if (onDrag)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (selectCard.cardData.cardType == Data.CardType.Attack && targetCharacter != null)
                    {
                        selectCard.UseSelectCard();
                        onDrag = false;
                        GameManager.Instance.cardManager.UseCard(selectCard);
                    }
                    else if(selectCard.cardData.cardType != Data.CardType.Attack)
                    {
                        selectCard.UseSelectCard();
                        onDrag = false;
                        GameManager.Instance.cardManager.UseCard(selectCard);
                    }
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    onDrag = false;
                    selectCard.gameObject.SetActive(true);
                    selectCard.CancleDrag();
                    selectCard = null;
                }
            }
        }
    }
}


/*
float mouseYPos = Camera.main.ScreenToViewportPoint(setCard.transform.position).y;
if (mouseYPos <= 0.25f)
  {
      setCard.transform.position = Input.mousePosition;
  }
  else
  {
      setCard.gameObject.SetActive(false);

      if(targetCharacter != null && Input.GetMouseButtonDown(0))
      {

      }
  }
*/