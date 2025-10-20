using System.Collections;

using TripleDot.Utils;

using UnityEngine;
using UnityEngine.UI;

namespace TripleDot.Blur
{
    [RequireComponent(typeof(RawImage), typeof(CanvasGroup))]
    public class BlurBehaviour : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RawImage _rawImage;
        
        [SerializeField] private Animator _animator;

        private int _showAnimatorTrigger;
        private int _hideAnimatorTrigger;
        private Texture2D _texture;
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rawImage = GetComponent<RawImage>();
        }
#endif

        private void Awake()
        {
            _canvasGroup.alpha = 0f;
            _showAnimatorTrigger = Animator.StringToHash("Show");
            _hideAnimatorTrigger = Animator.StringToHash("Hide");
        }
        
        public IEnumerator Show()
        {
            if (_texture != null)
            {
                Destroy(_texture);
            }
            
            yield return new WaitForEndOfFrame();
            _texture = ScreenCapture.CaptureScreenshotAsTexture();
            _rawImage.texture = _texture;
            _animator.SetTrigger(_showAnimatorTrigger);
            yield return _animator.WaitForAnimation("Show");
        }
        
        public IEnumerator Hide()
        {
            _animator.SetTrigger(_hideAnimatorTrigger);
            yield return _animator.WaitForAnimation("Hide");
        }
        
        private void OnDestroy()
        {
            if (_texture != null)
            {
                Destroy(_texture);                
            }
        }
    }
}
