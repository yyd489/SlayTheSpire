using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameWork
{
    public class ObjectPool : MonoBehaviour
    {
        private GameObject objParent;

        public GameObject poolingObjectPrefab;

        Queue<GameObject> poolingObjectQueue = new Queue<GameObject>();

        public void Awake()
        {
            objParent = this.gameObject;
            Initialize(5);
        }

        private void Initialize(int initCount)
        {
            for (int i = 0; i < initCount; i++)
            {
                poolingObjectQueue.Enqueue(CreateNewObject());
            }
        }

        // Queue에 오브젝트 추가
        public GameObject CreateNewObject()
        {
            var newObj = Instantiate(poolingObjectPrefab, objParent.transform);
            newObj.gameObject.SetActive(false);
            return newObj;
        }

        // Queue에 빼서 오브젝트 사용
        public GameObject GetObject(Transform parent)
        {
            if (poolingObjectQueue.Count > 0)
            {
                var obj = poolingObjectQueue.Dequeue();
                obj.transform.SetParent(parent);
                obj.gameObject.SetActive(true);
                return obj;
            }
            else
            {
                var newObj = CreateNewObject();
                newObj.gameObject.SetActive(true);
                newObj.transform.SetParent(parent);
                return newObj;
            }
        }

        // 사용한 오브젝트 다시 Queue에 추가
        public void ReturnObject(GameObject obj)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(objParent.transform);
            poolingObjectQueue.Enqueue(obj);
        }
    }
}