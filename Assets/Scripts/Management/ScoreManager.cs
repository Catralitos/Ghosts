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
        public IntEventChannelSO ghostEatenEvent;
        public VoidEventChannelSO pacmanEatenEvent;

        [Header("Broadcasting on")] 
        public VoidEventChannelSO startJingleEvent;
        public VoidEventChannelSO winJingleEvent;
        public VoidEventChannelSO loseJingleEvent;
        public VoidEventChannelSO gameLoadedEvent;
        public VoidEventChannelSO startGameEvent;
        public VoidEventChannelSO stopGameEvent;

        private int _pelletsEaten;
        private int _powerPelletsEaten;
        private int _ghostsEaten;
        
        private void OnEnable()
        {
            pelletEatenEvent.OnEventRaised += EatPellet;
            powerPelletEatenEvent.OnEventRaised += EatPowerPellet;
            ghostEatenEvent.OnEventRaised += EatGhost;
            pacmanEatenEvent.OnEventRaised += EatPacman;

        }
    
        private void OnDisable()
        {
            pelletEatenEvent.OnEventRaised -= EatPellet;
            powerPelletEatenEvent.OnEventRaised -= EatPowerPellet;
            ghostEatenEvent.OnEventRaised -= EatGhost;
            pacmanEatenEvent.OnEventRaised -= EatPacman;
        }

        private void Start()
        {
            scoreHolder.Init();
            stopGameEvent.RaiseEvent();
            startJingleEvent.RaiseEvent();
            Invoke(nameof(StartGame), 2f);
        }

        private void StartGame()
        {
            startGameEvent.RaiseEvent();
            gameLoadedEvent.RaiseEvent();
        }

        private void EatPellet()
        {
           scoreHolder.DecreaseScore(10);
           _pelletsEaten++;
           if (_pelletsEaten >= 244 && _powerPelletsEaten >= 4)
           {
               stopGameEvent.RaiseEvent();
               loseJingleEvent.RaiseEvent();
               Invoke(nameof(EndGame), 8f);
           }
        }

        private void EatPowerPellet()
        {
            scoreHolder.DecreaseScore(50);
            _powerPelletsEaten++;
            if (_pelletsEaten >= 244 && _powerPelletsEaten >= 4)
            {
                stopGameEvent.RaiseEvent();
                loseJingleEvent.RaiseEvent();
                Invoke(nameof(EndGame), 8f);
            }
        }

        private void EatPacman()
        {
            stopGameEvent.RaiseEvent();
            winJingleEvent.RaiseEvent();
            Invoke(nameof(EndGame), 8f);
        }

        private void EatGhost(int multiplier)
        {
            scoreHolder.DecreaseScore(200 * multiplier);
            _ghostsEaten++;
            if (_ghostsEaten >= 4)
            {
                stopGameEvent.RaiseEvent();
                loseJingleEvent.RaiseEvent();
                Invoke(nameof(EndGame), 8f);
            }
        }

        private void EndGame()
        {
            scoreHolder.EndGame();
        }
    }
}
