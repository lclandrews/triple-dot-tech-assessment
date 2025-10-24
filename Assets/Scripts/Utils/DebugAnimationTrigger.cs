using UnityEngine;

namespace TripleDot.Utils
{
    public class DebugAnimationTrigger : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private string _triggerName;
        [SerializeField] private bool _trigger;
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_trigger)
            {
                _trigger = false;
                if (_animator != null)
                {
                    // For debug only so we don't bother with the hash...
                    _animator.SetTrigger(_triggerName);                    
                }
            }
        }
#endif
    }
}
