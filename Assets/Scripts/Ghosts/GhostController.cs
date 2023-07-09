using System;
using Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Ghosts
{
    public class GhostController : MonoBehaviour
    {

        public Mind mind;
        public Vector3 startingPosition { get; private set; }
        private GhostOrderer go;
        [SerializeField] private Transform target;
        [SerializeField] private Transform teleporter_l;
        [SerializeField] private Transform teleporter_r;

        public GameObject lightPrefab;
        private Light2D _instatiatedLight;
        private Animator _animator;
        
        
        [Header("Listening on")] 
        public VoidEventChannelSO pelletEatenEvent;
        public VoidEventChannelSO pelletEndedEvent;
        private static readonly int Scared = Animator.StringToHash("Scared");

        private void OnEnable()
        {
            pelletEatenEvent.OnEventRaised += GetScared;
            pelletEndedEvent.OnEventRaised += StopScared;
        }
        
        private void OnDisable()
        {
            pelletEatenEvent.OnEventRaised -= GetScared;
            pelletEndedEvent.OnEventRaised -= StopScared;
        }

        private void Awake()
        {
            this.startingPosition = this.transform.position;
            this.target.position = this.startingPosition;
            this.go = GetComponent<GhostOrderer>();
        }

        private void Start()
        {
            GameObject instantiated = Instantiate(lightPrefab, transform.position, Quaternion.identity);
            _instatiatedLight = instantiated.GetComponent<Light2D>();
            _instatiatedLight.color = GetComponentInChildren<Light2D>().color;
            _instatiatedLight.intensity = 0;
            _animator = GetComponentInChildren<Animator>();
        }

        public void StartMovement(Vector2 goal)
        {
            target.position = new Vector3(goal.x, goal.y, target.position.z);
        }
        
        private void OnMouseDown()
        {
            mind.ChangeGhost(gameObject);
            go.enabled = true;
            Debug.Log("Picked " + gameObject);
        }

        private void GetScared()
        {
            if (_animator != null) _animator.SetBool(Scared, true);
        }

        private void StopScared()
        {
            if (_animator != null) _animator.SetBool(Scared, false);
        }
        
    }
}