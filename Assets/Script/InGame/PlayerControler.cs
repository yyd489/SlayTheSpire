using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FrameWork
{
    public class PlayerControler : MonoBehaviour
    {
        [SerializeField] private Texture2D cursorImg;

        // 캐릭터
        [SerializeField] private CharacterBase playerCharacter;
        [HideInInspector] public CharacterBase targetCharacter;

        // 카드
        private CardSorting cardSorting;
        [HideInInspector] public CardBase selectCard;
        [HideInInspector] public bool onDrag;

        public void Init()
        {
            Cursor.SetCursor(cursorImg, Vector2.zero, CursorMode.Auto);
            playerCharacter = GameObject.Find("ironclad").GetComponent<CharacterBase>();
            cardSorting = GameObject.Find("CardCanvas").transform.Find("Hand").GetComponent<CardSorting>();
            selectCard = GameObject.Find("CardCanvas").transform.Find("SelectCard").GetComponent<CardBase>();
        }

        private void FixedUpdate()
        {
            if (onDrag)
            {
                if (targetCharacter != null && Input.GetMouseButtonDown(0))
                {
                    playerCharacter.Attack(targetCharacter, selectCard.cardData.cardEffect);
                    onDrag = false;
                    cardSorting.UseCard(selectCard);
                }

                if (Input.GetMouseButtonDown(1))
                {
                    onDrag = false;
                    selectCard.gameObject.SetActive(true);
                    selectCard = null;
                    selectCard.CancleDrag();
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