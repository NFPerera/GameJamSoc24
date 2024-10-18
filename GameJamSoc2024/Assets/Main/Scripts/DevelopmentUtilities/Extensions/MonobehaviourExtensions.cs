using System;
using System.Collections;
using UnityEngine;

namespace _Main.Scripts.DevelopmentUtilities.Extensions
{
    public static class MonobehaviourExtensions
    {
        public static void ActionAfterFrame(this MonoBehaviour p_monoBehaviour, Action p_action)
        {
            p_monoBehaviour.StartCoroutine(IEActionAfterFrame(p_action));
        }

        private static IEnumerator IEActionAfterFrame(Action p_action)
        {
            yield return new WaitForEndOfFrame();
            p_action?.Invoke();
        }
    }
}