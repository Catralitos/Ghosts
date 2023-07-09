using UnityEngine;

namespace Pacman
{
    [DefaultExecutionOrder(-10)]
    [RequireComponent(typeof(Movement))]
    public class Pacman : MonoBehaviour
    {
        public Movement movement { get; private set; }
        public PacmanScatter scatter { get; private set; }
        public PacmanChase chase { get; private set; }
        public Transform[] ghosts;
        public Transform pelletMap;
        public LayerMask ghostLayer;
        public LayerMask wallLayer;
        public LayerMask nodeLayer;
        public int points = 200;

        private void Awake()
        {
            movement = GetComponent<Movement>();
            scatter = GetComponent<PacmanScatter>();
            chase = GetComponent<PacmanChase>();
        }

        private void Start()
        {
            ResetState();
        }

        public void ResetState()
        {
            gameObject.SetActive(true);
            movement.ResetState();

            chase.Disable();
            scatter.Enable();
        }

        public void SetPosition(Vector3 position)
        {
            // Keep the z-position the same since it determines draw depth
            position.z = transform.position.z;
            transform.position = position;
        }

        /* private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pac-Man"))
        {
            if (frightened.enabled) {
                FindObjectOfType<GameManager>().GhostEaten(this);
            } else {
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    } */

    }
}