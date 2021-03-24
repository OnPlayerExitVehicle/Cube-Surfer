using System;
using UnityEngine;

#if UNITY_EDITOR
public class Controller : MonoBehaviour
{

    [SerializeField] private float sensitivity;
    private bool controllable = true;

    void Awake()
    {
        EventManager.OnLevelFinished += (a) =>
        {
            controllable = false;
        };
        EventManager.OnLevelFailed += () =>
        {
            controllable = false;
        };
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && controllable)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y,
                Mathf.Clamp(transform.position.z + (Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime),
                    GlobalData.LeftBorder, GlobalData.RightBorder));
            Mathf.
        }
    }
}

#elif UNITY_ANDROID
public class Controller : MonoBehaviour
{
    private float mobileSensitivity = 50;
    private Touch touch;
    private bool controllable = true;

    void Awake()
    {
        EventManager.OnLevelFinished += (a) =>
        {
            controllable = false;
        };
        EventManager.OnLevelFailed += () =>
        {
            controllable = false;
        };
    }

    void Update(){

        if (Input.touchCount > 0 && controllable)
        {
            touch = Input.GetTouch(0);
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z + (touch.deltaPosition.x * (mobileSensitivity / 100) * Time.deltaTime), GlobalData.LeftBorder, GlobalData.RightBorder));
        }
    }
}
#endif

