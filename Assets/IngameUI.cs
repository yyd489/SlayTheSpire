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
            deckButton = GameObject.Find("DeckButton").GetComponent<Button>();
            mapButton = GameObject.Find("MapButton").GetComponent<Button>();

            var assetPrefabs = UImanager.listUIPrefab;

            for (int i = 0; i < assetPrefabs.Count; i++)
            {

                if (assetPrefabs[i].key == "Option")
                {   
                    int index = i;
                    optionButton.onClick.AddListener(() => InstantiateUI(assetPrefabs[index].value));
                    
                }
            }
        }

        private void InstantiateUI(GameObject UI)
        {
            Instantiate(UI);
           
        }

        

    }
}

