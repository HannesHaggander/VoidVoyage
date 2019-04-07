using UnityEngine;

namespace Assets.StaticObjects
{
    public interface IObjectReceiver
    {
        GameObject GetGameObject();
        void ReceiveObject(object message);
    }
}
