using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
    }
}
