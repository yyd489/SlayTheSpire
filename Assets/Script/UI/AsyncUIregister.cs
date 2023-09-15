using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
namespace FrameWork
{
   
   public static class AsyncUIregister
   {
      
        public async static void InstansUI(string path, Transform parent = null)
        {   
           // Addressables.LoadAssetAsync<GameObject>(path).Completed += InstansPrefab;
            var instantsObject = Addressables.LoadAssetAsync<GameObject>(path);
            await UniTask.WaitUntil(() => instantsObject.Result != null);
            var instantGameobject = instantsObject.Result;
           
            MonoBehaviour.Instantiate(instantGameobject,parent);
        }

        public async static Task<GameObject> AsyncInstansUI(string path, Vector2 pos = new Vector2(), Transform parent = null)
        {
            // Addressables.LoadAssetAsync<GameObject>(path).Completed += InstansPrefab;
            var instantsObject = Addressables.LoadAssetAsync<GameObject>(path);
            await UniTask.WaitUntil(() => instantsObject.Result != null);
            var instantGameobject = instantsObject.Result;
            return InstansPrefab(instantsObject);
        }

      

        //public async static Task<GameObject> RegisterPrefab(string path)
        //{
        //    // Addressables.LoadAssetAsync<GameObject>(path).Completed += InstansPrefab;
        //    var instantsObject = Addressables.LoadAssetAsync<GameObject>(path);
        //    await UniTask.WaitUntil(() => instantsObject.Result != null);
        //    var instantGameobject = instantsObject.Result;
        //    return instantGameobject;
        //}

        public static GameObject InstansPrefab(AsyncOperationHandle<GameObject> obj)
        {
                GameObject prefab = obj.Result;
                return MonoBehaviour.Instantiate(prefab);
            
        }

        //public static void InstansPrefab(AsyncOperationHandle<GameObject> obj, Vector2 pos, Transform parent)
        //{
        //    GameObject prefab = obj.Result;
        //    MonoBehaviour.Instantiate(prefab, pos, Quaternion.identity,parent);
        //}

        //public static GameObject InstansPrefab(AsyncOperationHandle<GameObject> obj, Vector2 pos, Transform parent)
        //{
        //    GameObject prefab = obj.Result;

        //    return MonoBehaviour.Instantiate(prefab, pos, Quaternion.identity, parent);
        //}
    }
}
