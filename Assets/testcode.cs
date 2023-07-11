using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
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
          
        }

    }

    private void Start()
    {
      //  Addressables.LoadAssetAsync<GameObject>("Assets/Prefabs/UI/OptionCanvas.prefab").Completed += cdcd;
    
    }

  

    public void cdcd(AsyncOperationHandle<GameObject> obj)
    {
        GameObject asa = obj.Result;

        Instantiate(asa);

    }

}
