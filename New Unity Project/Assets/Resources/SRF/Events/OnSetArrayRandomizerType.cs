using UnityEngine;
using UnityEditor;
using ScriptableEvenetSystem;
using System.Collections.Generic;
using System.Collections;

namespace ScriptableEvenetSystem
{
    //[InitializeOnLoad]
    [CreateAssetMenu(fileName = "OnSetArrayRandomizerType", menuName = "ScriptableObjects/OnSetArrayRandomizerType", order = 2)]
    public class OnSetArrayRandomizerType : ScriptableEventBase<OnSetArrayRandomizerType>
    {
        private void OnEnable()
        {
            CheckForInstanceEditor();
        }
        static OnSetArrayRandomizerType()
        {
            //CheckForInstanceRuntime();
        }
    }
}