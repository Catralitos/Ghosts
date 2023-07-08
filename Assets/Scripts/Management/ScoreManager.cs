using System;
using Events.ScriptableObjects;
using Management.ScriptableObjects;
using UnityEngine;

namespace Management
{
    public class ScoreManager : MonoBehaviour
    {
        public ScoreHolderSO scoreHolder;
    
        [Header("Listening on")] 
        public VoidEventChannelSO pelletEatenEvent;
        public VoidEventChannelSO powerPelletEatenEvent;
        public VoidEventChannelSO ghostEatenEvent;

        private void OnEnable()
        {
            pelletEatenEvent.OnEventRaised += EatPellet;
            powerPelletEatenEvent.OnEventRaised += EatPowerPellet;

        }
    
        private void OnDisable()
        {
            pelletEatenEvent.OnEventRaised -= EatPellet;
            powerPelletEatenEvent.OnEventRaised -= EatPowerPellet;
        }

        private void Start()
        {
            scoreHolder.Init();
        }

        private void EatPellet()
        {
           scoreHolder.DecreaseScore(10);
        }

        private void EatPowerPellet()
        {
            scoreHolder.DecreaseScore(50);
        }
    }
}
