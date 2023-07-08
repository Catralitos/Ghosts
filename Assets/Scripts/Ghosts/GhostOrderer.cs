using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Ghosts
{
    public class GhostOrderer : MonoBehaviour
    {
        public LayerMask obstacles;

        private GhostController _ghostController;
        
        private static float RoundToNearestHalf(float a)
        {
            return Mathf.Round(a * 2f) * 0.5f;
        }

        private void Start()
        {
            _ghostController = GetComponent<GhostController>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (Camera.main != null)
                {
                    Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    Vector2 actualPos = new Vector2(RoundToNearestHalf(worldPoint.x), RoundToNearestHalf(worldPoint.y));

                    Collider2D[] colliders = Physics2D.OverlapCircleAll(actualPos, 0.25f, obstacles);

                    if (colliders.Length == 0)
                    {
                        _ghostController.StartMovement(actualPos);
                    }

                }
            }
        }
    }
}
