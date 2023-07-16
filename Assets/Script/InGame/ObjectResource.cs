using System;
using UnityEngine;

namespace FrameWork
{
    public class ObjectResource : MonoBehaviour
    {
        [SerializeField] Animator _animator;
        [SerializeField] Renderer[] _renderers;
        public Animator Animator { get { return _animator; } }
        public Renderer[] Renderers { get { return _renderers; } }
#if UNITY_EDITOR
        private void OnValidate()
        {
            _animator = GetComponentInChildren<Animator>();
            _renderers = GetComponentsInChildren<Renderer>();
        }
#endif
        public void ActiveRender(bool isActive)
        {
            foreach (var renderer in _renderers)
            {
                renderer.gameObject.SetActive(isActive);
            }
        }
    }
}