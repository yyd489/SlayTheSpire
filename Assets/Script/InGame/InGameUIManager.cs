using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FrameWork
{
    public class InGameUIManager : MonoBehaviour
    {
        [SerializeField] private GameObject TurnEndBtn;
        [SerializeField] private TextMeshProUGUI deckCount;
        [SerializeField] private TextMeshProUGUI useDeckCount;
        [SerializeField] private TextMeshProUGUI energyText;

        [SerializeField] private TextMeshProUGUI narrationText;
                
        public void SetTurnEndBtn(bool active)
        {
            TurnEndBtn.SetActive(active);
        }

        public void RefreshDeckCountText(int deck, int useDeck)
        {
            deckCount.text = deck.ToString();
            useDeckCount.text = useDeck.ToString();
        }

        public void RefreshEnergyText(int useEnergy = 0)
        {
            GameManager.Instance.battleManager.energy -= useEnergy;
            energyText.text = string.Format("{0}/3", GameManager.Instance.battleManager.energy);
        }

        public IEnumerator OnNarration(string text)
        {
            narrationText.gameObject.SetActive(true);
            narrationText.text = text;
            narrationText.alpha = 0f;

            float alpha = 0.025f;

            while (narrationText.alpha < 1f)
            {
                narrationText.alpha += alpha;
                yield return null;
            }

            while (narrationText.alpha > 0f)
            {
                narrationText.alpha -= alpha;
                yield return null;
            }
        }
    }
}
