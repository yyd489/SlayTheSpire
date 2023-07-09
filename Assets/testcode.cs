using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class testcode : MonoBehaviour
{

    public Dropdown aa;

    // Start is called before the first frame update
   

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            // Screen.SetResolution(1920, 1080, false);

            Debug.Log(aa.options[aa.value].text);
            ss(aa.options[aa.value].text);
        }

    }

    public void ss(string cc)
    {
        Debug.Log(cc);
        string[] pp = cc.Split('x');
        Screen.SetResolution(int.Parse(pp[0]), int.Parse(pp[1]), false);
        
        
    }

}
