using Maze_Elements;
using UnityEngine;

namespace Pacman
{
    public class PacmanScatter : PacmanBehavior
    {
        private float ghostSightCooldown;

        private void OnDisable()
        {
            pacman.chase.Enable();
        }

        private void FixedUpdate() {
            if (ghostSightCooldown > 0.0f)
                ghostSightCooldown -= Time.fixedDeltaTime;

            RaycastHit2D ghostHit = Physics2D.BoxCast(transform.position, Vector2.one * 0.35f, 0f, pacman.movement.direction, 5.0f, pacman.ghostLayer | pacman.wallLayer | pacman.nodeLayer);

            if (ghostSightCooldown <= 0.0f && ghostHit.collider != null && (pacman.ghostLayer & 1 << ghostHit.collider.gameObject.layer) == 1 << ghostHit.collider.gameObject.layer) {
                //Debug.Log("AHHHHH a ghost");
                pacman.movement.SetDirection(pacman.movement.direction * -1.0f);
                ghostSightCooldown = 0.5f;
            }
        }
    
        private static float RoundToNearestHalf(float a)
        {
            return Mathf.Round(a * 2f) * 0.5f;
        }


        private void OnTriggerEnter2D(Collider2D other) {
            Node node = other.GetComponent<Node>();

            if (node != null && enabled)
            {
                Vector2 direction = Vector2.zero;
                transform.position = other.transform.position;
                float maxScore = float.MinValue;

                Transform closestPellet = pacman.pelletMap.GetChild(0);
                float distanceToClosestPellet = float.MaxValue;

                foreach(Transform pellet in pacman.pelletMap) {
                    float distanceToPellet = (pellet.position - transform.position).sqrMagnitude;
                    if (pellet.gameObject.activeSelf
                    && distanceToPellet < distanceToClosestPellet) {
                        closestPellet = pellet;
                        distanceToClosestPellet = distanceToPellet;
                    }
                }

                //Debug.Log("-------------------------------------------------------");
                // Find the available direction that moves farthest from pacman
                foreach (Vector2 availableDirection in node.availableDirections)
                {
                    //Debug.Log("availableDirection: " + availableDirection);
                    // If the distance in this direction is greater than the current
                    // max distance then this direction becomes the new farthest
                    Vector3 position = transform.position;
                    //Vector3 newPosition = new Vector3(RoundToNearestHalf(position.x), RoundToNearestHalf(position.y), position.z) + new Vector3(availableDirection.x, availableDirection.y);
                    Vector3 newPosition = position + new Vector3(availableDirection.x, availableDirection.y);
                    float score = 0;

                    foreach(Transform pellet in pacman.pelletMap) {
                        if (pellet.gameObject.activeSelf)
                            score += pelletWeight / (pellet.position - newPosition).sqrMagnitude;
                    }
                    foreach(Transform ghost in pacman.ghosts) {
                        if (ghost != null) {
                            float test = ghostWeight / Mathf.Pow((ghost.position - newPosition).sqrMagnitude, 1.0f);
                            //Debug.Log("Ghost score: " + test);
                            score -= test;
                        }
                    }

                    score += targetPelletWeight * distanceToClosestPellet * distanceToClosestPellet / (closestPellet.position - newPosition).sqrMagnitude;

                    if (availableDirection == pacman.movement.direction * -1.0f) {
                        score -= sameDirectionPenalty;
                        //Debug.Log("Punishment " + score);
                    }

                    if (score > maxScore)
                    {
                        direction = availableDirection;
                        maxScore = score;
                    }
                    //Debug.Log(score);
                }

                //Debug.Log(direction);
                pacman.movement.SetDirection(direction);
            }
        }
    }
}
