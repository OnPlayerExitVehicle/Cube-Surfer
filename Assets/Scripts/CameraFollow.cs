using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform followObject;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float height;
    private float horizontalDistance;

    void Start()
    {
        height = transform.position.y;
        horizontalDistance = transform.position.z;
        offset = followObject.position - transform.position;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(followObject.position.x - offset.x, height, horizontalDistance);
    }
}
