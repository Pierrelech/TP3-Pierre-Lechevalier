using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterFollowPlayer : MonoBehaviour
{
    public Transform player;  // Référence vers l'objet Player

    // Update est appelé à chaque frame
    void Update()
    {
        // Synchroniser la position du Centre avec celle du Player
        if (player != null)
        {
            transform.position = player.position;
        }
    }
}
