using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FrameWork
{
    public enum Buff
    {
        PowerUp = 0,
        DefenceDown = 1,
        PowerDown = 2
    }

    public class BuffStatus : MonoBehaviour
    {
        public Buff buff;
        public int turn;
        [SerializeField] Image iconImg;
        [SerializeField] TextMeshProUGUI turnText;

        public void InitBuff(Buff initBuff, int initTurn)
        {
            buff = initBuff;
            turn = initTurn;
            iconImg.sprite = GameManager.Instance.battleManager.arrBuffIcon[((int)buff)];
            turnText.text = turn.ToString();
        }

        public void RefreshBuff()
        {
            turn--;
            turnText.text = turn.ToString();
        }
    }
}