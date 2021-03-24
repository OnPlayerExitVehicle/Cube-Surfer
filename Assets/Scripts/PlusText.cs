using UnityEngine;

public class PlusText : MonoBehaviour
{
    [SerializeField] private float duration = 0.05f;
    private float startTime;

    void Awake()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (Time.time - startTime > duration)
        {
            Destroy(gameObject);
        }
    }
}
