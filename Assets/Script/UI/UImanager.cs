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

        public static void InstansUI(string path, Vector2 pos = new Vector2())
        {
            Addressables.LoadAssetAsync<GameObject>(path).Completed += InstansPrefab;
        }

        public static void InstansPrefab(AsyncOperationHandle<GameObject> obj)
        {
            GameObject prefab = obj.Result;
            MonoBehaviour.Instantiate(prefab);
        }

        public static void InstansPrefab(AsyncOperationHandle<GameObject> obj, Vector2 pos, Transform parent)
        {
            GameObject prefab = obj.Result;
            MonoBehaviour.Instantiate(prefab, pos, Quaternion.identity,parent);
        }
    }
}
