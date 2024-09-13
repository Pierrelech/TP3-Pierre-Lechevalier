using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    public Transform player; // R�f�rence au personnage

    void Update()
    {
        // Met � jour la position du point d'ancrage pour suivre le joueur
        if (player != null)
        {
            transform.position = player.position + new Vector3(0, 2, 0); // Ajustez la hauteur selon vos besoins
        }
    }
}
