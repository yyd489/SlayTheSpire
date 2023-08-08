using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace FrameWork
{
    using FrameWork.Data;

    public class IngameTopUI : MonoBehaviour
    {
        public Button optionButton;
        public Button deckButton;
        public Button mapButton;

        public GameObject mapPop;
        public GameObject optionPop;

        public TextMeshProUGUI goldText;
        public TextMeshProUGUI maxHpText;
        public TextMeshProUGUI nowHpText;
       
        // Start is called before the first frame update

        public void Init()
        {
            optionButton = GameObject.Find("OptionButton").GetComponent<Button>();
            optionButton.onClick.AddListener(() => AsyncUIregister.InstansUI("Assets/Prefabs/UI/OptionCanvas.prefab"));
            deckButton = GameObject.Find("DeckButton").GetComponent<Button>();

            mapButton = GameObject.Find("MapButton").GetComponent<Button>();    
            mapButton.onClick.AddListener(() => mapPop.gameObject.SetActive(true));
        }

        public void ChangeState()
        {
            goldText.text = DataManager.data.characterData.characterInfoCollect.characterCollect.gold.ToString();
            maxHpText.text = DataManager.data.characterData.characterInfoCollect.characterCollect.maxHp.ToString();
            nowHpText.text = DataManager.data.characterData.characterInfoCollect.characterCollect.hp.ToString();

        }
    }
}

