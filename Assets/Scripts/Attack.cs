using UnityEngine;

public class Attack : MonoBehaviour
{
    public float damage = 10f; // D�g�ts inflig�s par l'attaque
    [SerializeField]
    private GameObject charPerso;
    


    // M�thode appel�e par un �v�nement d'animation
    public void PerformAttack()
    {
        // D�tection des collisions avec les ennemis
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player")&& charPerso.CompareTag("Monstre"))
            {
                PlayerBehavior playerBehavior = hitCollider.GetComponent<PlayerBehavior>();
                if (playerBehavior != null)
                {
                    playerBehavior.TakeDamage(damage);
                }
            }
            if (hitCollider.CompareTag("Monstre") && charPerso.CompareTag("Player"))
            {
                MonsterAI monsterAI = hitCollider.GetComponent<MonsterAI>();
                if (monsterAI != null)
                {
                    monsterAI.TakeDamage(damage);
                }
            }
        }
    }
}
