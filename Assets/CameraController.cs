using CodeExtensions;
using StaticObjects;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    private Camera _camera;
    private Transform _target;

    public float CameraSpeed = 5;
    
    protected void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    protected void Start()
    {
        if (tag.Equals("MainCamera"))
        {
            ItemCache.Instance.SetMainCamera(_camera);
        }
    }

    protected void Update()
    {
        if(_target == null) { _target = ItemCache.Instance.Player; }
        else { FollowTarget(); }
    }

    private void FollowTarget()
    {
        var tmpPos = _target.position;
        tmpPos.z = transform.position.z;
        if (tmpPos.IsClose(transform.position, 0.5f)) { return; }
        transform.position = Vector3.Slerp(transform.position, tmpPos,
            (CameraSpeed + tmpPos.Distance(transform.position)) * Time.deltaTime);
    }
}
