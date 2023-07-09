using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OptionEditer : MonoBehaviour
{
    public Button backButton;

    private void Start()
    {
        
        backButton.onClick.AddListener(() => Destroy(this.transform.parent.gameObject));
    }

}
