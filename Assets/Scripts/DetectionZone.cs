using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public GameObject attackCollider; // Référence au GameObject AttackCollider

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (attackCollider != null)
            {
                attackCollider.SetActive(true); // Activer le collider d'attaque
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (attackCollider != null)
            {
                attackCollider.SetActive(false); // Désactiver le collider d'attaque
            }
        }
    }
}
