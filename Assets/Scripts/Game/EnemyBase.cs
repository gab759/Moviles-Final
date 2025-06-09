using UnityEngine;

public class EnemyBase : PoolObject
{
    [SerializeField] private float moveSpeed = 5f;

    protected Rigidbody rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
    {
        rb.linearVelocity = new Vector3(0f, 0f, -moveSpeed);
    }

    public virtual void OnTriggerEnter(Collider other)  
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Una vegetal menos");
        }
    }
}