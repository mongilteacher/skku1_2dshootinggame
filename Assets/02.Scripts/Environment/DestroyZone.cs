using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.SetActive(false);
    }
}
