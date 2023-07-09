using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace FrameWork
{
    public struct AssetPrefab { 
        
        public string key; 
        public GameObject value;

        public AssetPrefab(string key, GameObject value)
        {
            this.key = key;
            this.value = value;
        }
    
    }

   public partial class UImanager
   {
        //public static List<GameObject> listUiPrefab = new List<GameObject>();
        public static List<AssetPrefab> listUIPrefab = new List<AssetPrefab>();
        //public static GameObject activeUI;
        // Start is called before the first frame update
        public void Init()
        {
            RegisterPath();
        }

        public static void RegisterUI(string key, string path)
        {
            AssetPrefab assetprefab = new AssetPrefab(key, (GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)));
            listUIPrefab.Add(assetprefab);
        }

        public void RegisterPath()
        {
            RegisterUI("Option","Assets/Prefabs/UI/OptionCanvas.prefab");

        }

         
   }
}
