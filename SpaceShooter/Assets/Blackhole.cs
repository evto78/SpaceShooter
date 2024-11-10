using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : MonoBehaviour
{

    private Rigidbody2D objectRB;

    public float pullForce;
    public float destroyDistance;

    float baseDestroyDistance;

    private void Start()
    {
        baseDestroyDistance = destroyDistance;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject objectCollidedWith = collision.gameObject;

        Pull(objectCollidedWith);
        Consume(objectCollidedWith);
    }

    void Pull(GameObject objectCollidedWith)
    {
        if (objectCollidedWith.TryGetComponent<Rigidbody2D>(out objectRB))
        {
            objectRB.AddForce(GetForce(objectCollidedWith, Vector3.Distance(transform.position, objectCollidedWith.transform.position)), ForceMode2D.Force);
        }
    }

    void Consume(GameObject objectCollidedWith)
    {
        if (Vector3.Distance(transform.position, objectCollidedWith.transform.position) < destroyDistance)
        {
            if (objectCollidedWith.TryGetComponent<Rigidbody2D>(out objectRB))
            {
                objectRB.angularVelocity += 360f * Time.deltaTime;
                Destroy(objectCollidedWith, 1f);
                transform.localScale = transform.localScale * (1f + objectRB.mass * Time.deltaTime / 10f);
                destroyDistance = baseDestroyDistance * transform.localScale.x;
            }
        }
    }

    Vector2 GetForce(GameObject objectCollidedWith, float distance)
    {
        Vector2 my2DPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 object2DPosnew = new Vector2(objectCollidedWith.transform.position.x, objectCollidedWith.transform.position.y);

        return (my2DPos - object2DPosnew) * distance * Time.deltaTime * pullForce;
    }
}
