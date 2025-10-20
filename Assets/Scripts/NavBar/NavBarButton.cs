using TripleDot.Interfaces;

using UnityEngine;
using UnityEngine.UI;

namespace TripleDot.NavBar
{
    [RequireComponent(typeof(Toggle))]
    public class NavBarButton  : MonoBehaviour, IIdentifiableBehaviour
    {
        [field: SerializeField] public string Identifier { get; private set; } = string.Empty;

        [SerializeField] private LayoutElement _layoutElement;
        [SerializeField] private Toggle _toggle;
        
        private CanvasScaler _canvasScaler;
        private AnimationTriggers _cachedTriggers;
        private AnimationTriggers _selectedTriggers;
        private Rect _lastSafeArea = new Rect(0, 0, 0, 0);
        private bool _initialized = false;
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            _toggle = GetComponent<Toggle>();
            _cachedTriggers = _toggle.animationTriggers;
            _selectedTriggers = new AnimationTriggers
            {
                disabledTrigger = _cachedTriggers.disabledTrigger,
                normalTrigger = _cachedTriggers.selectedTrigger,
                selectedTrigger = _cachedTriggers.selectedTrigger,
                pressedTrigger = _cachedTriggers.pressedTrigger
            };
        }
#endif

        private void Awake()
        {
            _canvasScaler = GetComponentInParent<CanvasScaler>();
            _layoutElement = GetComponent<LayoutElement>();
            _toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        private void Start()
        {
            ApplySafeArea();
            _initialized = true;
        }

        private void OnToggleValueChanged(bool value)
        {
            if (value)
            {
                _toggle.animationTriggers = _selectedTriggers;
            }
            else
            {
                _toggle.animationTriggers = _cachedTriggers;
            }
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
            if (_layoutElement == null) return;
            
            Rect safeArea = Screen.safeArea;
            float scaleFactor = _canvasScaler != null ? _canvasScaler.scaleFactor : 1f;
            float yScale = scaleFactor * (Screen.height / _canvasScaler.referenceResolution.y);
            
            float bottom = safeArea.yMin / yScale;
            
            _layoutElement.preferredHeight += bottom;
            _lastSafeArea = safeArea;
        }
    }
}
