using TripleDot.HomeScreen;
using TripleDot.Interfaces;

using UnityEngine;

namespace TripleDot
{
    public class Controller : MonoBehaviour
    {
        [field: SerializeField] public BottomViewBar BottomViewBar { get; private set; } = null;

        private void Awake()
        {
            if (BottomViewBar == null)
            {
                Debug.LogWarning($"BottomViewVar for {gameObject.name} is not assigned.", this);
            }
            
            BottomViewBar.Closed.AddListener(OnBottomBarClosed);
            BottomViewBar.ContentActivated.AddListener(OnBottomBarContentActivated);
        }

        private void OnBottomBarClosed()
        {
            Debug.Log($"BottomViewBar is closed.");
        }

        private void OnBottomBarContentActivated(IIdentifiableBehaviour behaviour)
        {
            Debug.Log($"Content activated for {behaviour.Identifier}.");
        }
    }
}
