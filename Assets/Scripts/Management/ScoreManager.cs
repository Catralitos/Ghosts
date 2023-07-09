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
        public VoidEventChannelSO pacmanEatenEvent;

        private void OnEnable()
        {
            pelletEatenEvent.OnEventRaised += EatPellet;
            powerPelletEatenEvent.OnEventRaised += EatPowerPellet;
            pacmanEatenEvent.OnEventRaised += EatPacman;

        }
    
        private void OnDisable()
        {
            pelletEatenEvent.OnEventRaised -= EatPellet;
            powerPelletEatenEvent.OnEventRaised -= EatPowerPellet;
            pacmanEatenEvent.OnEventRaised -= EatPacman;
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

        private void EatPacman()
        {
            //scoreHolder.IncreaseScore(1000); Would be cool to give extra score depending on how fast you eat pac-man
            scoreHolder.EndGame();
        }
    }
}
