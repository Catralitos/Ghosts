using System;
using Maze_Elements;
using UnityEngine;

namespace Ghosts
{
    public class GhostController : MonoBehaviour
    {

        public Mind mind;
        public float speed = 8.0f;
        public float speedMultiplier = 1.0f;
        public Vector2 direction; //{ get; private set; }
        public Vector2 nextDirection;
        public LayerMask obstacleLayer;
        public Rigidbody2D rb { get; private set; }
        public Vector3 startingPosition { get; private set; }
        public Vector2 objective;
        private GhostOrderer go;
        private bool corner = false;
        [SerializeField] private Transform teleporter_l;
        [SerializeField] private Transform teleporter_r;


        private void Awake()
        {
            this.rb = GetComponent<Rigidbody2D>();
            this.startingPosition = this.transform.position;
            this.objective = this.startingPosition;
            this.go = GetComponent<GhostOrderer>();
        }

        public void SetDirection(Vector2 direction)
        {
            // Only set the direction if the tile in that direction is available
            // otherwise we set it as the next direction so it'll automatically be
            // set when it does become available
            corner = true;

            if (Mathf.Abs(transform.position.x - RoundToNearestHalf(transform.position.x)) <= 0.2f && 
                Mathf.Abs(transform.position.y - RoundToNearestHalf(transform.position.y)) <= 0.2f)
            {
                Debug.Log("The position was:" + transform.position);
                transform.position = new Vector3(RoundToNearestHalf(transform.position.x), RoundToNearestHalf(transform.position.y), transform.position.z);
            }

            if (!Occupied(direction))
            {
                this.direction = direction;
                nextDirection = Vector2.zero;
            }
            else
            {
                nextDirection = direction;
            }
            corner = false;
        }

        private static float RoundToNearestHalf(float a)
        {
            return Mathf.Round(a + 0.5f) - 0.5f;
        }

        public bool Occupied(Vector2 direction)
        {
            Vector3 position = transform.position;
            RaycastHit2D hit = Physics2D.BoxCast(position, Vector2.one * 0.75f, 0.0f, direction, 1.5f,
                obstacleLayer);
            return hit.collider != null;
        }

        private void Update() {
            if (nextDirection != Vector2.zero)
            {
                SetDirection(nextDirection);
            }
            if (Math.Abs(transform.position.x - objective.x) <= 0.1f && Math.Abs(transform.position.y - objective.y) <= 0.1f)
            {
                this.direction = Vector2.zero;
                this.nextDirection = Vector2.zero;
                rb.velocity = Vector2.zero;
                rb.position = objective;
                transform.position = objective;
            }
        }

        private void FixedUpdate()
        {
            //Vector2 translation = this.direction * this.speed * this.speedMultiplier * Time.fixedDeltaTime;
            if (!corner)
            {
                Transform t = transform;
                Vector2 position = t.position;
                Vector2 moveTowards = Vector2.MoveTowards(position, position + direction,
                    speed * speedMultiplier * Time.fixedDeltaTime);
                rb.MovePosition(moveTowards);
            }
        }

        private void OnMouseDown()
        {
                mind.ChangeGhost(this.gameObject);
                go.enabled = true;
        }

        private float DistanceToPoint(Vector2 pos, Vector2 target)
        {
            float min_dist = Mathf.Min(Vector2.Distance(pos, target),
                Vector2.Distance(pos, (Vector2)teleporter_l.position) + Vector2.Distance((Vector2)teleporter_r.position, target),
                Vector2.Distance(pos, (Vector2)teleporter_r.position) + Vector2.Distance((Vector2)teleporter_l.position, target));

            return min_dist;
        }

        public void StartMovement(Vector2 target)
        {
            objective = target;
            float distance = 0.0f;
            Vector2 direction = Vector2.zero;

            if (this.rb.position != objective)
            {
                if (!Occupied(Vector2.right) && 
                    (DistanceToPoint(this.rb.position + Vector2.right, objective) < distance ||
                        distance == 0.0f))
                {
                    distance = DistanceToPoint(this.rb.position + Vector2.right, objective);
                    direction = Vector2.right;
                }

                if (!Occupied(Vector2.left) &&
                    (DistanceToPoint(this.rb.position + Vector2.left, objective) < distance || distance == 0.0f))
                {
                    distance = DistanceToPoint(this.rb.position + Vector2.left, objective);
                    direction = Vector2.left;
                }

                if (!Occupied(Vector2.up) &&
                    (DistanceToPoint(this.rb.position + Vector2.up, objective) < distance || distance == 0.0f))
                {
                    distance = DistanceToPoint(this.rb.position + Vector2.up, objective);
                    direction = Vector2.up;
                }

                if (!Occupied(Vector2.down) &&
                    (DistanceToPoint(this.rb.position + Vector2.down, objective) < distance || distance == 0.0f))
                {
                    distance = DistanceToPoint(this.rb.position + Vector2.down, objective);
                    direction = Vector2.down;
                }
                SetDirection(direction);
            }
        }

        

        private void OnTriggerEnter2D(Collider2D other) {

            Node node = other.GetComponent<Node>(); 

            if (node != null && Math.Abs(transform.position.x - objective.x) > 0.1f && Math.Abs(transform.position.y - objective.y) > 0.1f)
            {
                Debug.Log("Entrou no node" + node.transform.position);
                Vector2 d = Vector2.zero;
                float distance = 0.0f;
                
                foreach (Vector2 availableDirection in node.availableDirections)
                {
                    Debug.Log("availableDirection is: " + availableDirection);
                    // If the distance in this direction is greater than the current
                    // max distance then this direction becomes the new farthest
                    if (availableDirection != this.direction * -1 && 
                        (DistanceToPoint(this.rb.position + availableDirection, objective) < distance || distance == 0.0f))
                    {
                        distance = DistanceToPoint(this.rb.position + availableDirection, objective);
                        d = availableDirection;
                    }
                }

                SetDirection(d);
            }
        }

    }
}
