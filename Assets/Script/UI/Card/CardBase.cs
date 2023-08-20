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
        public CardManager cardManager;
        public int cardIndex;

        // 카드 선택 & 드래그
        public Vector2 defaultPos;

        public void Init(CardJsonData data)
        {
            cardData = data;
            CardName.text = cardData.cardName;
            CardText.text = cardData.cardGuide;
            CardPoint.text = cardData.cardCost.ToString();
            CardImg.sprite = cardManager.dicCardImages[cardData.cardName];

            if (cardData.cardType == Data.CardType.Defence) CardType.text = Data.CardType.Skill.ToString();
            else CardType.text = cardData.cardType.ToString();
        }
    
        public void OnPointDown()
        {
            if (Input.GetMouseButton(0))
            {
                if (!GameManager.Instance.playerControler.onDrag && cardData.cardCost <= GameManager.Instance.battleManager.energy)
                {
                    defaultPos = transform.localPosition;
                    transform.rotation = Quaternion.identity;
                    cardManager.SelectCard(this);
                    GameManager.Instance.playerControler.onDrag = true;
                }
            }
        }

        public void UseSelectCard()
        {
            GameManager.Instance.battleManager.RefreshEnergyText(cardData.cardCost);
            CharacterBase playerCharacter = GameManager.Instance.playerControler.playerCharacter;

            if (cardData.cardType == Data.CardType.Attack)
            {
                bool isAllAttack = false;
                if(cardData.cardName == "천둥")
                    isAllAttack = true;

                Debug.Log(GameManager.Instance.playerControler.targetCharacter + " / " + cardData.cardEffect + " / " + cardData.cardSubEffect);
                playerCharacter.Attack(GameManager.Instance.playerControler.targetCharacter, cardData.cardEffect, cardData.cardSubEffect, isAllAttack);
            }
            else if (cardData.cardType == Data.CardType.Defence)
            {
                playerCharacter.SetShield(cardData.cardEffect);

                if (cardData.cardSubEffect > 0) GameManager.Instance.cardManager.DrawCard();
            }
            else if (cardData.cardType == Data.CardType.Skill)
            {
                if(cardData.cardName == "발화")
                {
                    BuffStatus newBuff = new BuffStatus();
                    newBuff.InitBuff(Buff.PowerUp, 1);
                    playerCharacter.AddBuffStat(newBuff);
                }
                else
                {
                    GameManager.Instance.battleManager.energy += 2;
                }
            }
        }

        public void CancleDrag()
        {
            transform.localPosition = defaultPos;
            cardManager.DefaltCardSorting();
        }
    }
}