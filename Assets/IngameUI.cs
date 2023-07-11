using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace FrameWork
{

    public class IngameUI : MonoBehaviour
    {
        public Button optionButton;
        public Button deckButton;
        public Button mapButton;
        // Start is called before the first frame update

        public void Init()
        {

            optionButton = GameObject.Find("OptionButton").GetComponent<Button>();
            optionButton.onClick.AddListener(() => InstantiateUI("Assets/Prefabs/UI/OptionCanvas.prefab"));

            deckButton = GameObject.Find("DeckButton").GetComponent<Button>();


            mapButton = GameObject.Find("MapButton").GetComponent<Button>();

        }

        private void InstantiateUI(string path)
        {
            UImanager.RegisterUI(path);
           
        }

    }
}

