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
        bool timerStarted = false;
        [SerializeField] private LayerMask obstacles;
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
            CancelInvoke("SetRandomTarget");
            timerStarted = false;
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
        
        private static float RoundToNearestHalf(float a)
        {
            return Mathf.Round(a + 0.5f) - 0.5f;
        }

        private void Update() {
            if (target != null)
            {
                _instatiatedLight.transform.position = target.position;
                _instatiatedLight.intensity = 100;
            }
            else
            {
                _instatiatedLight.transform.position = transform.position;
                _instatiatedLight.intensity = 0;
            }
            
            if (Mathf.Abs(transform.position.x - target.position.x) <= 0.1f &&
                Mathf.Abs(transform.position.y - target.position.y) <= 0.1f)
            {
                ScatterTimer();
            }
        }

        private void ScatterTimer()
        {
            if (!timerStarted)
            {
            Invoke(nameof(SetRandomTarget), UnityEngine.Random.Range(7,13));
            timerStarted = true;
            }
        }

        public void SetRandomTarget()
        {
            
            bool looping = true;
            int counter = 0;
            float x;
            float y;
            Vector2 randomTarget;
            while (looping && counter < 1000)
            {
                counter++;
                x = RoundToNearestHalf(UnityEngine.Random.Range(-12.99f, 12.99f));
                y = RoundToNearestHalf(UnityEngine.Random.Range(-14.99f, 14.00f));
                randomTarget = new Vector2(x, y);

                Collider2D[] colliders = Physics2D.OverlapCircleAll(randomTarget, 0.25f);
                if (colliders.Length == 0)
                {
                    looping = false;
                    target.position = new Vector3(x, y, target.position.z);
                }
            }
            timerStarted = false;
        }
        
    }
}