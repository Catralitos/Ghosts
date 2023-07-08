using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{

    public Mind mind;
    string lastMovingDirection;
    public float speed = 8.0f;
    public float speedMultiplier = 1.0f;
    public Vector2 initialDirection;
    public Vector2 direction { get; private set; }
    public LayerMask obstacleLayer;
    public Rigidbody2D rb { get; private set; }
    public Vector3 startingPosition { get; private set; }
    public bool moving = false;


    private void Awake() {
        this.rb = GetComponent<Rigidbody2D>();
        this.startingPosition = this.transform.position;
    }

    private void MoveToPosition(Transform objective)
    {
        moving = true;
        GetComponent<GhostOrderer>().enabled = false;
        while (transform.position != objective.position)
        {

        }
        moving = false;

    }

    public void SetDirection(Vector2 direction, bool forced)
    {
        this.direction = direction;
    }

    public bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f, this.obstacleLayer);
        return hit.collider != null;
    }

    private void FixedUpdate() {
        Vector2 position = this.rb.position;
        Vector2 translation = this.direction * this.speed * this.speedMultiplier * Time.fixedDeltaTime;
        this.rb.MovePosition(position + translation);
    }

    private void OnMouseDown() {
        if (!moving)
        {
        mind.ChangeGhost(this.gameObject);
        GetComponent<GhostOrderer>().enabled = true;
        }
    }

    

}
