using UnityEngine;
using UnityEngine.Tilemaps;

namespace Ghosts
{
    public class GhostOrderer : MonoBehaviour
    {


        [SerializeField] private Tilemap tileMap;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                var tpos = tileMap.WorldToCell(worldPoint);

                var tile = tileMap.GetTile(tpos);

                if (tile)
                {
                    Vector2 target = new Vector2(tpos.x, tpos.y);
                    GetComponent<GhostController>().StartMovement(target);
                }
                this.enabled = false;
            }
        }
    }
}
