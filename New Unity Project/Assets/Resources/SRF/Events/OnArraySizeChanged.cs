using UnityEngine;
using UnityEditor;

namespace ScriptableEvenetSystem
{
    //[InitializeOnLoad]
    [CreateAssetMenu(fileName = "OnArraySizeChanged", menuName = "ScriptableObjects/OnArraySizeChanged", order = 2)]
    public class OnArraySizeChanged : ScriptableEventBase<OnArraySizeChanged>
    {
        static OnArraySizeChanged()
        {
            CheckForInstanceEditor();
        }
    }
}