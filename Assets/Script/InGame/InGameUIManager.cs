using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

        [SerializeField] private Button usedCardDeckBtn;
        [SerializeField] private Button haveCardDeckBtn;
        
        private IEnumerator coNarration;

        public void Init()
        {
            usedCardDeckBtn.onClick.AddListener(() => UsedCardDeckOpen());
            haveCardDeckBtn.onClick.AddListener(() => HaveCardDeckOpen());
        }

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

        public void Narration(string text)
        {
            if (coNarration != null)
            {
                StopCoroutine(coNarration);
                coNarration = null;
            }

            coNarration = GameManager.Instance.inGameUIManager.OnNarration(text);
            StartCoroutine(coNarration);
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

        public void HaveCardDeckOpen()
        {
            if (GameManager.Instance.playerControler.onDrag) return;

            AsyncUIregister.InstansUI("Assets/Prefabs/UI/DeckPopup.prefab");
            List<int> cardList = new List<int>(GameManager.Instance.cardManager.queMainDeck);

            cardList.Sort();
            DeckPopUI.listDeckCards = cardList;
        }

        public void UsedCardDeckOpen()
        {
            if (GameManager.Instance.playerControler.onDrag) return;

            AsyncUIregister.InstansUI("Assets/Prefabs/UI/DeckPopup.prefab");
            List<int> cardList = new List<int>();

            for (int i = 0; i < GameManager.Instance.cardManager.listUseDeck.Count; i++)
            {
                cardList.Add(GameManager.Instance.cardManager.listUseDeck[i]);
            }

            cardList.Sort();
            DeckPopUI.listDeckCards = cardList;
        }
    }
}
