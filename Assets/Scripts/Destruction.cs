using UnityEngine;

public class Destruction : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter(Collision other)
    {
        Debug.Log(true);
        if (other.gameObject.CompareTag("Wall"))
        {
            Debug.Log($"{other.transform.position.y} - {transform.position.y}");
        }
    }
}
