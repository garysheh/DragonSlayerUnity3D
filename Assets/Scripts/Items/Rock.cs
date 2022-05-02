using UnityEngine;
using UnityEngine.AI;

public class Rock : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Setting")]
    public float force;
    public GameObject target;
    private Vector3 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player");
        FlyToTarget();
    }

    public void FlyToTarget()
    {
        direction = (target.transform.position - transform.position + Vector3.up).normalized;
        rb.AddForce(direction * force, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            Destroy(gameObject, 3f);
        }

        if (collision.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            collision.gameObject.GetComponent<Animator>().SetTrigger("Dizzy");
        }
    }
}
