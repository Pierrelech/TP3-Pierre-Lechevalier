using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    public Transform player; // Référence au personnage

    void Update()
    {
        // Met à jour la position du point d'ancrage pour suivre le joueur
        if (player != null)
        {
            transform.position = player.position + new Vector3(0, 2, 0); // Ajustez la hauteur selon vos besoins
        }
    }
}
