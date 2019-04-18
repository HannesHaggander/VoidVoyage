using UnityEngine;

namespace Resources.Items
{
    public interface IItem
    {
        void Activate();
        void Deactivate();
        string GetPrefabPath();
        GameObject GetGameObject();
        void SetStatsObject(Transform statsObject);
    }
}
