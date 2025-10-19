using System.Collections.Generic;

using TripleDot.Interfaces;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace TripleDot.HomeScreen
{
    public class BottomViewBar : MonoBehaviour 
    {
        [field: SerializeField] public UnityEvent<IIdentifiableBehaviour> ContentActivated { get; private set; } = new ();
        [field: SerializeField] public UnityEvent Closed { get; private set; } = new ();
        
        [SerializeField] private ToggleGroup _toggleGroup;
        private readonly List<Toggle> _toggles = new();

        private Toggle _activeToggle;
        
        private void Awake()
        {
            if (_toggleGroup == null)
            {
                Debug.LogWarning($"Toggle group for {gameObject.name} is not assigned.", this);
                return;
            }
            
            GetComponentsInChildren(true, _toggles);
            for (int i = 0; i < _toggles.Count; i++)
            {
                int index = i;
                _toggles[i].onValueChanged.AddListener(delegate {
                    OnToggleValueChanged(_toggles[index]);
                });

                // Make sure we capture an initially activated toggle
                if (_activeToggle == null && _toggles[index].isOn)
                {
                    OnToggleValueChanged(_toggles[index]);
                }
            }
        }

        private void OnToggleValueChanged(Toggle toggle)
        {
            if (toggle.isOn)
            {
                if (_activeToggle != toggle)
                {
                    _activeToggle = toggle;
                    IIdentifiableBehaviour identifiableBehaviour = toggle.GetComponent<IIdentifiableBehaviour>();
                    if (identifiableBehaviour != null)
                    {
                        ContentActivated.Invoke(identifiableBehaviour);
                    }
                    else
                    {
                        Debug.LogWarning($"Toggle: {toggle.name} for {gameObject.name} is not identifiable.", this);
                    }   
                }
            }
            else if(!_toggleGroup.AnyTogglesOn())
            {
                Closed.Invoke();  
            }
        }
    }
}
