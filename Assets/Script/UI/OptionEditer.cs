using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace FrameWork
{
    public class OptionEditer : MonoBehaviour
    {
        public Button backButton;
        public Button quitButton;
        public TMP_Dropdown wideDropdown;
        public TMP_Dropdown frameDropdown;

        private void Start()
        {
           
            backButton.onClick.AddListener(() => Soundmanager.instance.effectPlaySound(2));
            backButton.onClick.AddListener(() => Destroy(this.transform.parent.gameObject));

            quitButton.onClick.AddListener(() => Soundmanager.instance.effectPlaySound(2));
            quitButton.onClick.AddListener(() => Application.Quit());

            wideDropdown.value= PlayerPrefs.GetInt("WideSizeValue", wideDropdown.value);

            frameDropdown.value = PlayerPrefs.GetInt("FrameValue", frameDropdown.value);


            wideDropdown.onValueChanged.AddListener(delegate
            {
                ChangeWideSize(wideDropdown.options[wideDropdown.value].text);
            });

            frameDropdown.onValueChanged.AddListener(delegate
            {
                ChangeFrame(frameDropdown.options[frameDropdown.value].text);
            });


        }

        private void ChangeWideSize(string wideSize)
        {
          
            string[] sizeXY = wideSize.Split('x');
        
            Screen.SetResolution(int.Parse(sizeXY[0]), int.Parse(sizeXY[1]), false);

            PlayerPrefs.SetInt("WideSizeValue", wideDropdown.value);
        }

        private void ChangeFrame(string frame)
        {
            Application.targetFrameRate = int.Parse(frame);

            PlayerPrefs.SetInt("FrameValue", frameDropdown.value);
        }

        public void ChangeBackGroundSound(float SetVolume)
        {
            Soundmanager.instance.backgroundAudio.volume = SetVolume;
        }

        public void ChangeEffectSound(float SetVolume)
        {
            Soundmanager.instance.effectAudio.volume = SetVolume;
            Soundmanager.instance.effectAudio2.volume = SetVolume;
        }
    }
}
