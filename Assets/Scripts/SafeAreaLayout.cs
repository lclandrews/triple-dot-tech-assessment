using System;

using UnityEngine;
using UnityEngine.UI;

namespace TripleDot
{
    [RequireComponent(typeof(RectTransform))]
    public class SafeAreaLayout : MonoBehaviour
    {
        [SerializeField] private bool _padTop = true;
        [SerializeField] private bool _padBottom = true;
        [SerializeField] private bool _padLeft = true;
        [SerializeField] private bool _padRight = true;

        private CanvasScaler _canvasScaler;
        private LayoutGroup _layoutGroup;
        private RectTransform _rectTransform;
        private Rect _lastSafeArea = new Rect(0, 0, 0, 0);
        private bool _initialized = false;
    
#if UNITY_EDITOR
        private void OnValidate()
        {
            _rectTransform = GetComponent<RectTransform>();
        }
#endif

        private void Awake()
        {
            _canvasScaler = GetComponentInParent<CanvasScaler>();
            _layoutGroup = GetComponent<LayoutGroup>();   
        }
        
        private void Start()
        {
            ApplySafeArea();
            _initialized = true;
        }
    
        private void OnRectTransformDimensionsChange()
        {
            if (_initialized && Screen.safeArea != _lastSafeArea)
            {
                ApplySafeArea();   
            }
        }
    
        private void ApplySafeArea()
        {
            Rect safeArea = Screen.safeArea;
            float scaleFactor = _canvasScaler != null ? _canvasScaler.scaleFactor : 1f;
            float xScale = scaleFactor * (Screen.width / _canvasScaler.referenceResolution.x);
            float yScale = scaleFactor * (Screen.height / _canvasScaler.referenceResolution.y);
            
            float left = safeArea.xMin / xScale;
            float right = (Screen.width  - safeArea.xMax) / xScale;
            float bottom = safeArea.yMin / yScale;
            float top = (Screen.height - safeArea.yMax) / yScale;
            
            if (_layoutGroup != null)
            {
                _layoutGroup.padding = new RectOffset(
                    _padLeft ? Mathf.Max(Mathf.RoundToInt(left), _layoutGroup.padding.left) : _layoutGroup.padding.left,
                    _padRight ? Mathf.Max(Mathf.RoundToInt(right), _layoutGroup.padding.right) : _layoutGroup.padding.right,
                    _padTop ? Mathf.Max(Mathf.RoundToInt(top), _layoutGroup.padding.top) : _layoutGroup.padding.top,
                    _padBottom ? Mathf.Max(Mathf.RoundToInt(bottom), _layoutGroup.padding.bottom) : _layoutGroup.padding.bottom
                );
            }
            else
            {
                _rectTransform.offsetMin = new Vector2(
                    _padLeft ? left : _rectTransform.offsetMin.x,
                    _padBottom ? bottom : _rectTransform.offsetMin.y
                );
                _rectTransform.offsetMax = new Vector2(
                    _padRight ? -right : _rectTransform.offsetMax.x,
                    _padTop ? -top : _rectTransform.offsetMax.y
                );
            }
            _lastSafeArea = safeArea;
        }
    }
}
