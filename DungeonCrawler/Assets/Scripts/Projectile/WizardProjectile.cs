using UnityEngine;

public class WizardProjectile : Projectile
{
    [SerializeField] float speed = 8f;
    [SerializeField] float destroyAfter = 4f;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(gameObject, destroyAfter);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.forward * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damageData);
            }

            Destroy(gameObject);
        }
    }
}
