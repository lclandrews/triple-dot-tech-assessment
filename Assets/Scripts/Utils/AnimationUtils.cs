using System.Collections;

using UnityEngine;

namespace TripleDot.Utils
{
    public static class AnimationUtils
    {
        public static IEnumerator WaitForAnimation(this Animator animator, string stateName, int layer = 0)
        {
            while (!animator.GetCurrentAnimatorStateInfo(layer).IsName(stateName))
                yield return null;
            
            while (animator.GetCurrentAnimatorStateInfo(layer).normalizedTime < 1.0f)
                yield return null;
        }    
    }
}
