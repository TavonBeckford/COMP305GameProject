using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public float minimum_distance;
    private Rigidbody rb;

    // Start is called before the first frame update

    Vector2 targetPos;
    private GameObject PlayerObject;

    void Start()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        targetPos = PlayerObject.transform.position;

    }

    private void FixedUpdate()
    {
        Vector2 currentPosition = transform.position;
        Vector2 targetDirection = targetPos - currentPosition;

        if (targetDirection.magnitude > minimum_distance)
        {
            Vector2 newPosition = currentPosition + targetDirection.normalized * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);
        }
    }
}
