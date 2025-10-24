using UnityEngine;
using UnityEngine.UI;

namespace TripleDot.LevelCompleted
{
    [ExecuteAlways]
    public class ExposeBackgroundFade : MonoBehaviour
    {
        private static readonly int _Fade = Shader.PropertyToID("_Fade");
        
        [SerializeField] private Graphic _graphic;
        [SerializeField, Range(0, 1)] private float _fade;

        private float _fadeValue;

        private void Awake() 
        {
            if (_graphic != null)
            {
                _fadeValue = _graphic.material.GetFloat(_Fade);
            }
        }

        private void LateUpdate() 
        {
            if (_graphic != null && !Mathf.Approximately(_fadeValue, _fade))
            {
                _graphic.material.SetFloat(_Fade, _fade);   
            }
        }
    }
}
