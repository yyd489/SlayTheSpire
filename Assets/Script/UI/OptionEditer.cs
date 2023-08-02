using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class OptionEditer : MonoBehaviour
{
    public Button backButton;
    public TMP_Dropdown wideDropdown;
    public TMP_Dropdown frameDropdown;

    private void Start()
    {

        backButton.onClick.AddListener(() => Destroy(this.transform.parent.gameObject));
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
    }

    private void ChangeFrame(string frame)
    {

        Application.targetFrameRate = int.Parse(frame);

        Debug.Log(Application.targetFrameRate);
    }

}
