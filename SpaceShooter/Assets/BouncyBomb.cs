using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBomb : MonoBehaviour
{
    public float explosionForce;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionObject;
        Rigidbody2D collisionRb;
        collisionObject = collision.gameObject;

        if (collisionObject.TryGetComponent<Rigidbody2D>(out collisionRb))
        {
            collisionRb.AddForce((collisionObject.transform.position - transform.position) * explosionForce, ForceMode2D.Impulse);

            Destroy(gameObject);
        }
    }
}
