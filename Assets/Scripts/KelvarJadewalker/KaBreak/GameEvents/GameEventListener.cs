using System;
using KelvarJadewalker.KaBreak.templates;
using UnityEngine;
using UnityEngine.Events;

namespace KelvarJadewalker.KaBreak.GameEvents
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] protected GameEvent gameEvent;
        [SerializeField] protected UnityEvent unityEvent;
        
        

        private void Awake() => gameEvent.Register(this);
        private void OnDestroy() => gameEvent.Deregister(this);

        public void RaiseEvent() => unityEvent.Invoke();

    }
}
