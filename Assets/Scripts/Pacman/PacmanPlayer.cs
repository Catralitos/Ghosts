using UnityEngine;
using Events.ScriptableObjects;

namespace Pacman
{
    [DefaultExecutionOrder(-10)]
    [RequireComponent(typeof(Movement))]
    public class PacmanPlayer : MonoBehaviour
    {
        public Movement movement { get; private set; }

        public float powerPelletTime = 8.0f;
        public LayerMask ghostLayer;
        
        [Header("Listening on ")] 
        public VoidEventChannelSO powerPelletEatenEvent;

        [Header("Broadcasting on ")]
        public VoidEventChannelSO pacmanEatenEvent;
        public IntEventChannelSO ghostEatenEvent;
        public VoidEventChannelSO pelletEnded;
        
        
        public int ghostMultiplier = 1;

        private float horizontal;
        private float vertical;
        private bool powerPelletEnabled;

        private void OnEnable()
        {
            powerPelletEatenEvent.OnEventRaised += EatPowerPellet;
        }
    
        private void OnDisable()
        {
            powerPelletEatenEvent.OnEventRaised -= EatPowerPellet;
        }

        private void Awake()
        {
            movement = GetComponent<Movement>();
        }

        private void Start()
        {
            ResetState();
        }

        public void ResetState()
        {
            gameObject.SetActive(true);
            movement.ResetState();
        }

        private void Update() {
            // Set the new direction based on the current input
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                movement.SetDirection(Vector2.up);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                movement.SetDirection(Vector2.down);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                movement.SetDirection(Vector2.left);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                movement.SetDirection(Vector2.right);
            }
        }

        public void SetPosition(Vector3 position)
        {
            // Keep the z-position the same since it determines draw depth
            position.z = transform.position.z;
            transform.position = position;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if ((ghostLayer & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
            {
                if (!powerPelletEnabled) {
                    pacmanEatenEvent.RaiseEvent();
                    gameObject.SetActive(false);
                }
                else {
                    ghostEatenEvent.RaiseEvent(ghostMultiplier);
                    ghostMultiplier *= 2;
                    Destroy(collision.gameObject);
                }
            }
        }

        private void EatPowerPellet() {
            powerPelletEnabled = true;
            Invoke(nameof(DisableChase), powerPelletTime);
        }

        private void DisableChase() {
            powerPelletEnabled = false;
        }
    }
}