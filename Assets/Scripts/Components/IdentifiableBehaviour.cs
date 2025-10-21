using TripleDot.Interfaces;

using UnityEngine;

namespace TripleDot.Components
{
    public class IdentifiableBehaviour : MonoBehaviour, IIdentifiableBehaviour
    {
        [field: SerializeField] public string Identifier { get; private set; } = string.Empty;
    }
}
