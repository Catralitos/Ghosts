using UnityEngine;
using Events.ScriptableObjects;

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

        public float powerPelletTime = 8.0f;
        
        [Header("Listening on ")] 
        public VoidEventChannelSO powerPelletEatenEvent;

        [Header("Broadcasting on ")]
        public VoidEventChannelSO pacmanEatenEvent;
        public IntEventChannelSO ghostEatenEvent;
        public VoidEventChannelSO pelletEnded;
        
        public int ghostMultiplier = 1;

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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if ((ghostLayer & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer)
            {
                if (scatter.enabled) {
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
            scatter.Disable();
            Invoke(nameof(DisableChase), powerPelletTime);
        }

        private void DisableChase() {
            pelletEnded.RaiseEvent();
            ghostMultiplier = 1;
            chase.Disable();
        }

    }
}