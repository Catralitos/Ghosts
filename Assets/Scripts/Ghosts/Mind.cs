using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Ghosts
{
    public class Mind : MonoBehaviour
    {
        [SerializeField] private GameObject[] ghosts;
        private GameObject _currentGhost;

        private readonly Light2D[] _lights = new Light2D[4];

        private void Start() {
            for (int i = 0; i < ghosts.Length; i++)
            {
                ghosts[i].GetComponent<GhostOrderer>().enabled = false;
                _lights[i] = ghosts[i].GetComponentInChildren<Light2D>();
            }
            _currentGhost = ghosts[0];
        }

        public void ChangeGhost(GameObject newGhost)
        {
            _currentGhost.GetComponent<GhostOrderer>().enabled = false;
            _currentGhost = newGhost;
            for (int i = 0; i < ghosts.Length; i++)
            {
                _lights[i].intensity = _currentGhost == ghosts[i] ? 100 : 10;
            }
            
        }
    }
}
