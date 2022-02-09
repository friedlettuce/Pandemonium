using UnityEngine;

public class HealthItem : MonoBehaviour
{
    [SerializeField] private float healthValue;

    [Header("SFX")]
    [SerializeField] private AudioClip pickupSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Health>().AddHealth(healthValue);
            SoundManager.instance.PlaySound(pickupSound);
            gameObject.SetActive(false);
        }
    }
}
