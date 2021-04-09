using System.Collections.Generic;
using KelvarJadewalker.KaBreak.GameEvents;
using UnityEngine;

namespace KelvarJadewalker.KaBreak.templates
{
    [CreateAssetMenu(menuName = "My Custom Objects/Game Event", fileName = "New Game Event", order = 51)]
    public class GameEvent : ScriptableObject
    {
        private HashSet<GameEventListener> _listeners = new HashSet<GameEventListener>();
        
        public void Invoke()
        {
            foreach (var globalEventListener in _listeners)
            {
                globalEventListener.RaiseEvent();
                Debug.Log("Invoke called for : "  + this);
            }
        }

        public void Register(GameEventListener gameEventListener) => _listeners.Add(gameEventListener);
        public void Deregister(GameEventListener gameEventListener) => _listeners.Remove(gameEventListener);
        
    }
}
