using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterFollowPlayer : MonoBehaviour
{
    public Transform player;  // R�f�rence vers l'objet Player

    // Update est appel� � chaque frame
    void Update()
    {
        // Synchroniser la position du Centre avec celle du Player
        if (player != null)
        {
            transform.position = player.position;
        }
    }
}
