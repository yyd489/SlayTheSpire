using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Cysharp.Threading.Tasks;
namespace FrameWork
{
   
   public class UImanager
   {
        public async static void InstansUI(string path, Vector2 pos = new Vector2(), Transform parent = null)
        {
            
           // Addressables.LoadAssetAsync<GameObject>(path).Completed += InstansPrefab;
            var instantsObject = Addressables.LoadAssetAsync<GameObject>(path);
            await UniTask.WaitUntil(() => instantsObject.Result != null);
            var instantGameobject = instantsObject.Result;

            if (null == parent)
            {
               MonoBehaviour.Instantiate(instantGameobject);
            }
            else
            {
                InstansPrefab(instantsObject, pos, parent);
            }
           
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
