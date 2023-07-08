using Events.ScriptableObjects;
using Management.ScriptableObjects;
using UnityEngine;

namespace Management
{
    public class ScoreManager : MonoBehaviour
    {
        public ScoreHolderSO ScoreHolder;
    
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
    
        private void EatPellet()
        {
           ScoreHolder.DecreaseScore(10);
        }

        private void EatPowerPellet()
        {
            ScoreHolder.DecreaseScore(50);
        }
    }
}
