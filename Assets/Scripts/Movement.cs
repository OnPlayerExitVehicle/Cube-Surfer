using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    private Rigidbody rigidBody;
    private GameObject lastCollision = null;
    private Transform characterTransform;
    private bool inCollision = false;
    [SerializeField] private GameObject plusText;
    private bool isMoving = true;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        characterTransform = transform.GetChild(1);
    }

    void Update()
    {
        if (isMoving)
        {
            rigidBody.transform.position += Vector3.left * (Time.deltaTime * speed);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Pickup") && !inCollision)
        {
            if (other.gameObject != lastCollision)
            {
                other.transform.position = new Vector3(transform.position.x, other.transform.position.y, transform.position.z);
                transform.position += (Vector3.up * (other.transform.localScale.y / 2));
                characterTransform.position += Vector3.up * 0.25f;
                other.transform.parent = transform;
                lastCollision = other.gameObject;
                Instantiate(plusText, transform.position, transform.rotation);

                #region Platform Dependent Compilation

#if UNITY_ANDROID && !UNITY_EDITOR
                Vibration.Vibrate(50);
#endif

                #endregion
            }
        }
        else if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Multiplier"))
        {
            
            var thisCollider = other.contacts[0].thisCollider.transform;
            if (Math.Abs(thisCollider.position.y - other.transform.position.y) < 0.1f)
            {
                thisCollider.parent = null;
                lastCollision = thisCollider.gameObject;
            }

            inCollision = true;

            if (transform.childCount < 3)
            {
                isMoving = false;
                rigidBody.useGravity = false;
                if (other.gameObject.CompareTag("Wall"))
                {
                    EventManager.InvokeLevelFailedEvent();
                }
                else // Multiplier
                {
                    int multiplier;
                    if (Int32.TryParse(other.gameObject.name.Split('X')[0], out multiplier))
                    {
                        EventManager.InvokeLevelFinishEvent(multiplier);
                    }
                }
            }
        }
        else if (other.gameObject.CompareTag("Plumbob") && other.gameObject != lastCollision)
        {
            EventManager.InvokePlumbobCollectEvent();
            Destroy(other.gameObject);
            lastCollision = other.gameObject;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            inCollision = false;
        }
    }
}
