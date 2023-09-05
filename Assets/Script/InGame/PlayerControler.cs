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
        public CharacterBase playerCharacter;
        [HideInInspector] public Ironclad ironclad;
        public CharacterBase targetCharacter;

        [SerializeField] private GameObject targetBox;

        // 카드
        [HideInInspector] public CardBase selectCard;
        [HideInInspector] public bool onDrag;
        [HideInInspector] public Data.PotionJsonData selectPotion;

        public void Init()
        {
            Cursor.SetCursor(cursorImg, Vector2.zero, CursorMode.Auto);
            ironclad = playerCharacter.GetComponent<Ironclad>();
            ironclad.Init(null);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && onDrag)
            {
                Vector3 pos = Camera.main.WorldToViewportPoint(Input.mousePosition);

                if (pos.y < 20f) return;

                if (selectPotion != null && targetCharacter != null)
                {
                    targetCharacter.Hit(0, selectPotion.potionEffect);
                    GameManager.Instance.potionManager.DropPotion();
                    selectPotion = null;
                    TargetSet(null);
                    onDrag = false;
                }
                else
                {
                    if (selectCard.cardData.cardType == Data.CardType.Attack && targetCharacter != null && selectCard.cardData.cardName != "천둥")
                    {
                        selectCard.UseSelectCard();
                        GameManager.Instance.cardManager.UseCard(selectCard);
                        selectPotion = null;
                        TargetSet(null);
                        onDrag = false;
                    }
                    else if (selectCard.cardData.cardType != Data.CardType.Attack || selectCard.cardData.cardName == "천둥")
                    {
                        selectCard.UseSelectCard();
                        GameManager.Instance.cardManager.UseCard(selectCard);
                        TargetSet(null);
                        onDrag = false;
                    }
                }
            }
            else if (Input.GetMouseButtonDown(1) && onDrag)
            {
                if (selectPotion != null)
                    selectPotion = null;
                else
                {
                    selectCard.gameObject.SetActive(true);
                    selectCard.CancleDrag();
                    selectCard = null;
                }

                targetBox.SetActive(false);
                onDrag = false;
            }
        }

        public void SelectPorionData(Data.PotionJsonData potionData)
        {
            selectPotion = potionData;
            onDrag = true;
        }

        public void TargetSet(CharacterBase target)
        {
            targetCharacter = target;
            if (targetCharacter != null && selectCard.cardData.cardName != "천둥")
            {
                targetBox.SetActive(true);
                targetBox.transform.position = Camera.main.WorldToScreenPoint(target.transform.parent.transform.position + new Vector3(0f, -0.3f));
            }
            else
                targetBox.SetActive(false);
        }
    }
}