using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace FrameWork
{
    public class CardBase : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI CardName;
        [SerializeField] protected TextMeshProUGUI CardText;
        [SerializeField] protected TextMeshProUGUI CardPoint;
        [SerializeField] protected TextMeshProUGUI CardType;
        [SerializeField] protected Image CardImg;

        [SerializeField] List<Sprite> cardSprites;

        private void Init()
        {
            CardName.text = "Test";
            CardText.text = "Test Text";
            CardPoint.text = "5";
            CardType.text = "Attack";
            CardImg.sprite = cardSprites[0];
        }

        private void Start()
        {
            Init();
        }
    }
}