using UnityEngine;

public abstract class PacmanBehavior : MonoBehaviour
{
    public Pacman pacman { get; private set; }
    public float pelletWeight;
    public float ghostWeight;
    public float sameDirectionPenalty;

    private void Awake()
    {
        pacman = GetComponent<Pacman>();
    }

    public void Enable()
    {
        enabled = true;
    }

    public void Disable()
    {
        enabled = false;
    }
}