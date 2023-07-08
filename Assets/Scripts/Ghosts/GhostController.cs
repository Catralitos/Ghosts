using UnityEngine;

namespace Ghosts
{
    public class GhostController : MonoBehaviour
    {

        public Mind mind;
        public Vector2 lastMovingDirection = Vector2.zero;
        public float speed = 8.0f;
        public float speedMultiplier = 1.0f;
        public Vector2 initialDirection;
        public Vector2 direction { get; private set; }
        public LayerMask obstacleLayer;
        public Rigidbody2D rb { get; private set; }
        public Vector3 startingPosition { get; private set; }
        public bool moving = false;
        public Transform objective;


        private void Awake()
        {
            this.rb = GetComponent<Rigidbody2D>();
            this.startingPosition = this.transform.position;
        }

        public void SetDirection(Vector2 direction)
        {
            this.lastMovingDirection = this.direction;
            this.direction = direction;
        }

        public bool Occupied(Vector2 direction)
        {
            RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f,
                this.obstacleLayer);
            return hit.collider != null;
        }

        private void FixedUpdate()
        {
            if (this.rb.position.x == objective.position.x && this.rb.position.y == objective.position.y)
            {
                this.lastMovingDirection = Vector2.zero;
                this.direction = Vector2.zero;
            }

            Vector2 position = this.rb.position;
            Vector2 translation = this.direction * this.speed * this.speedMultiplier * Time.fixedDeltaTime;
            this.rb.MovePosition(position + translation);
        }

        private void OnMouseDown()
        {
            if (!moving)
            {
                mind.ChangeGhost(this.gameObject);
                GetComponent<GhostOrderer>().enabled = true;
            }
        }

        public void StartMovement(Transform target)
        {
            objective = target;
            float distance = 0.0f;
            Vector2 direction = Vector2.zero;

            if (this.rb.position.x != objective.position.x || this.rb.position.y != objective.position.y)
            {
                if (!Occupied(Vector2.right) && Vector2.right != -lastMovingDirection &&
                    (Vector2.Distance(this.rb.position + Vector2.right, objective.position) < distance ||
                     distance == 0))
                {
                    distance = Vector2.Distance(this.rb.position + Vector2.right, objective.position);
                    direction = Vector2.right;
                }

                if (!Occupied(Vector2.left) && Vector2.left != -lastMovingDirection &&
                    (Vector2.Distance(this.rb.position + Vector2.left, objective.position) < distance || distance == 0))
                {
                    distance = Vector2.Distance(this.rb.position + Vector2.left, objective.position);
                    direction = Vector2.left;
                }

                if (!Occupied(Vector2.up) && Vector2.up != -lastMovingDirection &&
                    (Vector2.Distance(this.rb.position + Vector2.up, objective.position) < distance || distance == 0))
                {
                    distance = Vector2.Distance(this.rb.position + Vector2.up, objective.position);
                    direction = Vector2.up;
                }

                if (!Occupied(Vector2.down) && Vector2.down != -lastMovingDirection &&
                    (Vector2.Distance(this.rb.position + Vector2.down, objective.position) < distance || distance == 0))
                {
                    distance = Vector2.Distance(this.rb.position + Vector2.down, objective.position);
                    direction = Vector2.down;
                }

                SetDirection(direction);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Node node = other.GetComponent<Node>();
            float distance = 0.0f;
            Vector2 direction = Vector2.zero;

            if (node != null &&
                (this.rb.position.x != objective.position.x || this.rb.position.y != objective.position.y))
            {
                if (!Occupied(Vector2.right) && Vector2.right != -lastMovingDirection &&
                    (Vector2.Distance(this.rb.position + Vector2.right, objective.position) < distance ||
                     distance == 0))
                {
                    distance = Vector2.Distance(this.rb.position + Vector2.right, objective.position);
                    direction = Vector2.right;
                }

                if (!Occupied(Vector2.left) && Vector2.left != -lastMovingDirection &&
                    (Vector2.Distance(this.rb.position + Vector2.left, objective.position) < distance || distance == 0))
                {
                    distance = Vector2.Distance(this.rb.position + Vector2.left, objective.position);
                    direction = Vector2.left;
                }

                if (!Occupied(Vector2.up) && Vector2.up != -lastMovingDirection &&
                    (Vector2.Distance(this.rb.position + Vector2.up, objective.position) < distance || distance == 0))
                {
                    distance = Vector2.Distance(this.rb.position + Vector2.up, objective.position);
                    direction = Vector2.up;
                }

                if (!Occupied(Vector2.down) && Vector2.down != -lastMovingDirection &&
                    (Vector2.Distance(this.rb.position + Vector2.down, objective.position) < distance || distance == 0))
                {
                    distance = Vector2.Distance(this.rb.position + Vector2.down, objective.position);
                    direction = Vector2.down;
                }

                SetDirection(direction);
            }
        }


    }
}
