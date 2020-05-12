using System;
using System.Collections.Generic;

namespace ScriptableEvenetSystem
{
    [Path("ScriptableEvents")]
    public abstract class ScriptableEventBase<T> : ScriptableObjectSingleton<T> where T : ScriptableEventBase<T>
    {
        protected List<Action> Listeners;

        public virtual void Raise()
        {
            if (Listeners.Count == 0)
                return;

            for (int i = Listeners.Count; i > 0; i--)
            {
                Listeners[i].Invoke();
            }
        }

        public virtual void RegisterListener(Action listener)
        {
            if (Listeners.Contains(listener))
                return;

            Listeners.Add(listener);
        }

        public virtual void UnregisterListener(Action listener)
        {
            if (Listeners.Count == 0)
                return;

            for (int i = Listeners.Count; i > 0; i--)
            {
                if (Listeners[i] == listener)
                    Listeners.Remove(listener);
            }
        }
    }
}