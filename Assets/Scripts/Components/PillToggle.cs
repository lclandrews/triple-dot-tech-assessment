using UnityEngine;
using UnityEngine.UIElements.Experimental;

using System.Collections;

using UnityEngine.UI;

namespace TripleDot.Components
{
    [RequireComponent(typeof(Toggle))]
    public class PillToggle : MonoBehaviour
    {
        [SerializeField] private RectTransform _handle;
        [SerializeField] private Image _background;

        [SerializeField] private float _animationDuration = 0.25f;

        [SerializeField] private Vector2 _onPosition = Vector2.zero;
        [SerializeField] private Vector2 _offPosition = Vector2.zero;
        
        [SerializeField] private Color _onColor = new Color(0.2f, 0.7f, 0.3f);
        [SerializeField] private Color _offColor = new Color(0.7f, 0.7f, 0.7f);

        private Toggle _toggle;
        private float _animTime = 1.0F;
        private Coroutine _animRoutine;

#if UNITY_EDITOR
        private void OnValidate()
        {
            _toggle = GetComponent<Toggle>();   
        }
#endif
        
        private void Awake()
        {
            if (_handle == null)
            {
                Debug.LogWarning("Handle is not assigned for PillToggle.", this);
                return;
            }
            
            if (_background == null)
            {
                Debug.LogWarning("Background is not assigned for PillToggle.", this);
                return;
            }
            
            _handle.anchoredPosition = _toggle.isOn ? _onPosition : _offPosition;
            _background.color = _toggle.isOn ? _onColor : _offColor;

            _toggle.onValueChanged.AddListener(OnToggleChanged);
        }

        private void OnDestroy()
        {
            _toggle.onValueChanged.RemoveListener(OnToggleChanged);
        }

        private void OnToggleChanged(bool isOn)
        {
            if (_animRoutine != null)
            {
                StopCoroutine(_animRoutine);
            }
            _animTime = 1 - _animTime;
            _animRoutine = StartCoroutine(AnimateToggle(isOn));
        }

        private IEnumerator AnimateToggle(bool isOn)
        {
            Vector2 startPos = _handle.anchoredPosition;
            Vector2 endPos = isOn ? _onPosition : _offPosition;

            Color startColor = _background.color;
            Color endColor = isOn ? _onColor : _offColor;
            
            while (_animTime < 1f)
            {
                _animTime += Time.deltaTime / _animationDuration;
                float easedTime = Easing.OutBounce(_animTime);

                _handle.anchoredPosition = Vector2.Lerp(startPos, endPos, easedTime);
                _background.color = Color.Lerp(startColor, endColor, easedTime);

                yield return null;
            }
            _handle.anchoredPosition = endPos;
            _background.color = endColor;
            _animRoutine = null;
        }
    }
}