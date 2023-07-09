using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 8.0f;
    public float speedMultiplier = 1.0f;
    public Vector2 initialDirection;
    public LayerMask obstacleLayer;

    public new Rigidbody2D rigidbody { get; private set; }
    public Vector2 direction { get; private set; }
    public Vector2 nextDirection { get; private set; }
    public Vector3 startingPosition { get; private set; }


    public Transform spriteChild;
    private Vector3 _originalScale;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
    }

    private void Start()
    {
        ResetState();
        _originalScale = spriteChild.localScale;
    }

    public void ResetState()
    {
        speedMultiplier = 1f;
        direction = initialDirection;
        nextDirection = Vector2.zero;
        transform.position = startingPosition;
        rigidbody.isKinematic = false;
        enabled = true;
    }

    private void Update()
    {
        // Try to move in the next direction while it's queued to make movements
        // more responsive
        if (nextDirection != Vector2.zero) {
            SetDirection(nextDirection);
        }
        
        int multiplier = direction.x < 0 ? -1 : 1;
        Vector3 localScale = new Vector3(multiplier * _originalScale.x, _originalScale.y);
        spriteChild.localScale = localScale;

        int rotation = 0;
        if (Math.Abs(direction.y - 1) <= 0.01f) rotation = 90;
        if (Math.Abs(direction.y - -1) <= 0.01f) rotation = -90;
        spriteChild.rotation = Quaternion.Euler(0,0,rotation);
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * speedMultiplier * Time.fixedDeltaTime;
        rigidbody.MovePosition(position + translation);
    }

    public void SetDirection(Vector2 direction, bool forced = false)
    {
        // Only set the direction if the tile in that direction is available
        // otherwise we set it as the next direction so it'll automatically be
        // set when it does become available
        if (forced || !Occupied(direction))
        {
            this.direction = direction;
            nextDirection = Vector2.zero;
        }
        else
        {
            nextDirection = direction;
        }
    }

    public bool Occupied(Vector2 direction)
    {
        // If no collider is hit then there is no obstacle in that direction
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.6f, 0f, direction, 1.5f, obstacleLayer);
        return hit.collider != null;
    }
    
    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position + Vector3.up * 1.5f, Vector3.one * 0.75f);
        Gizmos.DrawCube(transform.position + Vector3.down * 1.5f, Vector3.one * 0.75f);
        Gizmos.DrawCube(transform.position + Vector3.left * 1.5f, Vector3.one * 0.75f);
        Gizmos.DrawCube(transform.position + Vector3.right * 1.5f, Vector3.one * 0.75f);
    }

}
