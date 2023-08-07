using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class testscr : MonoBehaviour
{
    public Text aas;
   
    // Start is called before the first frame update
    void Start()
    {
        string aa = "<color=yellow>Message : </color>" + "메세지";

        aas.text = aa;
    }

   
}
