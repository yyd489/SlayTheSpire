using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace FrameWork
{

    public class IngameTopUI : MonoBehaviour
    {
        public Button optionButton;
        public Button deckButton;
        public Button mapButton;

        public GameObject mapPop;
        // Start is called before the first frame update

        public void Init()
        {
            optionButton = GameObject.Find("OptionButton").GetComponent<Button>();
            optionButton.onClick.AddListener(() => AsyncUIregister.InstansUI("Assets/Prefabs/UI/OptionCanvas.prefab"));
            deckButton = GameObject.Find("DeckButton").GetComponent<Button>();

            mapButton = GameObject.Find("MapButton").GetComponent<Button>();    
            mapButton.onClick.AddListener(() => mapPop.gameObject.SetActive(true));
        }

    }
}

