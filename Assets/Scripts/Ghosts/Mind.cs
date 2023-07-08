using UnityEngine;

namespace Ghosts
{
    public class Mind : MonoBehaviour
    {
        [SerializeField] private GameObject[] ghosts;
        private GameObject currentGhost;

        private void Start() {
            foreach (GameObject ghost in ghosts)
            {
                ghost.GetComponent<GhostOrderer>().enabled = false;
            }
            currentGhost = ghosts[0];
        }

        public void ChangeGhost(GameObject newGhost)
        {
            //currentGhost.GetComponent<GhostOrderer>().enabled = false;
            currentGhost = newGhost;
        }
    }
}
