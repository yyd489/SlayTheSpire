using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
namespace FrameWork
{
   
   public class UImanager
   {
      
        public static void RegisterUI(string path)
        {
            Addressables.LoadAssetAsync<GameObject>(path).Completed += InstansPrefab;
        }

        public static void InstansPrefab(AsyncOperationHandle<GameObject> obj)
        {
            GameObject prefab = obj.Result;
            MonoBehaviour.Instantiate(prefab);
        }     
   }
}
