using System;
using Audio;
using Events.ScriptableObjects;
using UnityEngine;

namespace Management
{
    public class MusicManager : MonoBehaviour
    {

        public AudioManager audioManager;

        [Header("Listening on")] 
        public VoidEventChannelSO pelletEatenEvent;
        public VoidEventChannelSO powerPelletEatenEvent;
        public VoidEventChannelSO powerPelletEndedEvent;
        public IntEventChannelSO ghostEatenEvent;
        public VoidEventChannelSO menuOpenEvent;
        public VoidEventChannelSO gameOpenEvent;
        
        #region SingleTon
     
        /// <summary>
        /// Gets the sole instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static MusicManager Instance { get; private set; }
     
        /// <summary>
        /// Awakes this instance (if none has been created).
        /// </summary>
        private void Awake()
        {
            // Needed if we want the audio manager to persist through scenes
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
     
            DontDestroyOnLoad(gameObject);
            
        }
     
        #endregion
        
        private void OnEnable()
        {
            pelletEatenEvent.OnEventRaised += PlayPelletSound;
            powerPelletEatenEvent.OnEventRaised += PlayPowerPellet;
            powerPelletEndedEvent.OnEventRaised += StopPowerPellet;
            ghostEatenEvent.OnEventRaised += PlayGhostEaten;
            menuOpenEvent.OnEventRaised += LoadMenu;
            gameOpenEvent.OnEventRaised += LoadGame;
        }

        private void OnDisable()
        {
            pelletEatenEvent.OnEventRaised += PlayPelletSound;
            powerPelletEatenEvent.OnEventRaised += PlayPowerPellet;
            powerPelletEndedEvent.OnEventRaised += StopPowerPellet;
            ghostEatenEvent.OnEventRaised += PlayGhostEaten;
            menuOpenEvent.OnEventRaised += LoadMenu;
            gameOpenEvent.OnEventRaised += LoadGame;
        }

        /*private void Start()
        {
            _audioManager = GetComponent<AudioManager>();
        }*/

        private void Update()
        {
            if (audioManager == null)
            {
                Debug.Log("Audiomanager is null for some fucking reason");
            }
        }

        private void PlayPelletSound()
        {
            audioManager.Play("PelletEaten");
        }

        private void PlayPowerPellet()
        {
            audioManager.Stop("GameMusic");
            audioManager.Play("RampageMusic");
        }

        private void StopPowerPellet()
        {
            audioManager.Stop("RampageMusic");
            audioManager.Play("GameMusic");
        }

        private void PlayGhostEaten(int score)
        {
            audioManager.Play("GhostEaten");
        }

        private void LoadMenu()
        {
            audioManager.Stop("GameMusic");
            audioManager.Play("MenuMusic");
        }

        private void LoadGame()
        {
            audioManager.Stop("MenuMusic");
            audioManager.Play("GameMusic");
        }

    }
}