using System.Collections;

using TripleDot.Blur;
using TripleDot.Interfaces;
using TripleDot.Utils;

using UnityEngine;

namespace TripleDot.Components
{
    public class PopupWindow : MonoBehaviour, IWindow
    {
        [SerializeField] private Animator _animator;
        [SerializeField, Tooltip("The animation state name is also use to determine the desired trigger.")] 
        private string _showAnimationName = "Show";
        [SerializeField, Tooltip("The animation state name is also use to determine the desired trigger.")]
        private string _hideAnimationName = "Hide";

        private int _showTrigger;
        private int _hideTrigger;
        
        private void Awake()
        {
            if (_animator == null)
            {
                Debug.LogWarning($"Animator is not assigned for PopupWindow: {gameObject.name}", this);
                return;
            }
            
            _showTrigger = Animator.StringToHash(_showAnimationName);
            _hideTrigger = Animator.StringToHash(_hideAnimationName);
        }
        
        public IEnumerator Show(BlurBehaviour blurBehaviour)
        {
            gameObject.SetActive(true);
            if (blurBehaviour != null)
            {
                StartCoroutine(blurBehaviour.Show());                
            }
            _animator.SetTrigger(_showTrigger);
            yield return _animator.WaitForAnimation(_showAnimationName);
        }
        
        public IEnumerator Hide(BlurBehaviour blurBehaviour)
        {
            if (blurBehaviour != null)
            {
                StartCoroutine(blurBehaviour.Hide());                
            }
            _animator.SetTrigger(_hideTrigger);
            yield return _animator.WaitForAnimation(_hideAnimationName);
            gameObject.SetActive(false);
        }
    }
}
