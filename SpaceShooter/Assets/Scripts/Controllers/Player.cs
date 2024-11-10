using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Transform> asteroidTransforms;
    public Transform enemyTransform;
    public GameObject bombPrefab;
    public Transform bombsTransform;

    public GameObject bouncyBomb;
    public Transform bombSpawnPoint;

    public float accelerationTime = 1f;
    public float decelerationTime = 1f;
    public float maxSpeed = 7.5f;
    public float turnSpeed = 180f;

    private float acceleration;
    private float deceleration;
    private Vector3 currentVelocity;
    private float maxSpeedSqr;

    Rigidbody2D rb;

    private void Start()
    {
        acceleration = maxSpeed / accelerationTime;
        deceleration = maxSpeed / decelerationTime;
        maxSpeedSqr = maxSpeed * maxSpeed;

        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject spawnedBomb = Instantiate(bouncyBomb);

            spawnedBomb.transform.position = bombSpawnPoint.position;
        }

        CheckIfOnEdge();

        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
            moveDirection += Vector3.up;
        if (Input.GetKey(KeyCode.S))
            moveDirection += Vector3.down;
        
        if (Input.GetKey(KeyCode.D))
            moveDirection += Vector3.right;
        if (Input.GetKey(KeyCode.A))
            moveDirection += Vector3.left;

        if (moveDirection.sqrMagnitude > 0)
        {
            currentVelocity += Time.deltaTime * acceleration * moveDirection;
            if (currentVelocity.sqrMagnitude > maxSpeedSqr)
            {
                currentVelocity = currentVelocity.normalized * maxSpeed;
            }
        }
        else
        {
            Vector3 velocityDelta = Time.deltaTime * deceleration * currentVelocity.normalized;
            if (velocityDelta.sqrMagnitude > currentVelocity.sqrMagnitude)
            {
                currentVelocity = Vector3.zero;
            }
            else
            {
                currentVelocity -= velocityDelta;
            }
        }

         rb.AddForce(currentVelocity * Time.deltaTime, ForceMode2D.Force);
    }

    void CheckIfOnEdge()
    {
        float maxY = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0f)).y;
        float minY = -maxY;

        float maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0f)).x;
        float minX = -maxX;

        if(transform.position.y > maxY) { transform.position = new Vector3(transform.position.x, minY, transform.position.z); }
        if(transform.position.y < minY) { transform.position = new Vector3(transform.position.x, maxY, transform.position.z); }

        if(transform.position.x > maxX) { transform.position = new Vector3(minX, transform.position.y, transform.position.z); }
        if(transform.position.x < minX) { transform.position = new Vector3(maxX, transform.position.y, transform.position.z); }
    }

}
