using UnityEngine;

namespace Ghosts
{
    public class GhostController : MonoBehaviour
    {

        public Mind mind;
        public Vector2 lastMovingDirection = Vector2.zero;
        public float speed = 8.0f;
        public float speedMultiplier = 1.0f;
        public Vector2 direction; //{ get; private set; }
        public Vector2 nextDirection;
        public LayerMask obstacleLayer;
        public Rigidbody2D rb { get; private set; }
        public Vector3 startingPosition { get; private set; }
        public Vector2 objective;
        private GhostOrderer go;


        private void Awake()
        {
            this.rb = GetComponent<Rigidbody2D>();
            this.startingPosition = this.transform.position;
            this.go = GetComponent<GhostOrderer>();
        }

        public void SetDirection(Vector2 direction)
        {
            // Only set the direction if the tile in that direction is available
            // otherwise we set it as the next direction so it'll automatically be
            // set when it does become available
            if (!Occupied(direction))
            {
                lastMovingDirection = this.direction;
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
            RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f,
                this.obstacleLayer);
            return hit.collider != null;
        }

        private void Update() {
            if (nextDirection != Vector2.zero)
            {
                SetDirection(nextDirection);
            }
            if ((Vector2)transform.position == objective)
            {
                this.lastMovingDirection = Vector2.zero;
                this.direction = Vector2.zero;
                this.nextDirection = Vector2.zero;
            }
        }

        private void FixedUpdate()
        {

            Vector2 position = this.rb.position;
            Vector2 translation = this.direction * this.speed * this.speedMultiplier * Time.fixedDeltaTime;
            this.rb.MovePosition(position + translation);
        }

        private void OnMouseDown()
        {
                Debug.Log("BananaMan");
                //mind.ChangeGhost(this.gameObject);
                go.enabled = true;
        }

        public void StartMovement(Vector2 target)
        {
            objective = target;
            float distance = 0.0f;
            Vector2 direction = Vector2.zero;

            if (this.rb.position != objective)
            {
                if (!Occupied(Vector2.right) && 
                    (Vector2.Distance(this.rb.position + Vector2.right, objective) < distance ||
                        distance == 0.0f))
                {
                    distance = Vector2.Distance(this.rb.position + Vector2.right, objective);
                    direction = Vector2.right;
                }

                if (!Occupied(Vector2.left) &&
                    (Vector2.Distance(this.rb.position + Vector2.left, objective) < distance || distance == 0.0f))
                {
                    distance = Vector2.Distance(this.rb.position + Vector2.left, objective);
                    direction = Vector2.left;
                }

                if (!Occupied(Vector2.up) &&
                    (Vector2.Distance(this.rb.position + Vector2.up, objective) < distance || distance == 0.0f))
                {
                    distance = Vector2.Distance(this.rb.position + Vector2.up, objective);
                    direction = Vector2.up;
                }

                if (!Occupied(Vector2.down) &&
                    (Vector2.Distance(this.rb.position + Vector2.down, objective) < distance || distance == 0.0f))
                {
                    distance = Vector2.Distance(this.rb.position + Vector2.down, objective);
                    direction = Vector2.down;
                }

                SetDirection(direction);
            }
        }

        

        private void OnTriggerEnter2D(Collider2D other) {

            Node node = other.GetComponent<Node>(); 

            if (node != null && this.rb.position != objective)
            {
                Vector2 direction = Vector2.zero;
                float distance = 0.0f;

                // Find the available direction that moves farthest from pacman
                foreach (Vector2 availableDirection in node.availableDirections)
                {
                    Debug.Log("availableDirection is: " + availableDirection);
                    // If the distance in this direction is greater than the current
                    // max distance then this direction becomes the new farthest
                    if (availableDirection != lastMovingDirection * -1.0f && 
                        Vector2.Distance(this.rb.position + availableDirection, objective) < distance || distance == 0.0f)
                    {
                        distance = Vector2.Distance(this.rb.position + availableDirection, objective);
                        direction = availableDirection;
                    }
                }
            }
        }

    }
}
