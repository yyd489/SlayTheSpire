using UnityEngine;
using UnityEngine.UI;

public class ButtonTouchSetting : MonoBehaviour
{
    void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
    }
}