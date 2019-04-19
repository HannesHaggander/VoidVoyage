using System;
using System.Collections.Generic;
using System.Linq;
using Assets.StaticObjects;
using UnityEngine;

namespace StaticObjects
{
    public class ObjectMessenger : MonoBehaviour
    {
        public static ObjectMessenger Instance { get; private set; }
        private readonly Dictionary<Type, List<Subscriber>> _subCache = new Dictionary<Type, List<Subscriber>>();

        protected void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }

        public Guid Subscribe(Type message, IObjectReceiver receiver)
        {
            var sub = new Subscriber
            {
                Receiver = receiver
            };

            if (!_subCache.ContainsKey(message))
            {
                _subCache.Add(message, new List<Subscriber>());
            }

            _subCache[message].Add(sub);
            return sub.Id;
        }

        public void Unsubscribe(Guid id)
        {
            foreach (var item in _subCache)
            {
                if (!item.Value.Any()) { continue; }
                var subId = item.Value.FirstOrDefault(s => s.Id.Equals(id));
                if (subId == null) { continue; }
                item.Value.Remove(subId);
                return;
            }
        }

        public void Unsubscribe(ref Guid? id)
        {
            if (!id.HasValue) { return; }
            Unsubscribe(id.Value);
            id = null;
        }

        public void Publish(object message)
        {
            if (!_subCache.TryGetValue(message.GetType(), out var subs)) { return; }

            subs.ForEach(x => x.Receiver.ReceiveObject(message));
        }
    }

    internal class Subscriber
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public IObjectReceiver Receiver { get; set; }
    }
}
