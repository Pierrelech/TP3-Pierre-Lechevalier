using UnityEngine;
using UnityEngine.AI; // Nécessaire si vous utilisez NavMeshAgent

public class MonsterAI : MonoBehaviour
{
    public Animator animator; // Référence à l'Animator du monstre
    public Transform player;  // Référence au joueur
    public float attackRange = 5f; // Distance à laquelle le monstre commence à attaquer
    public float moveSpeed = 2f; // Vitesse de déplacement du monstre
    public float rotationSpeed = 5f; // Vitesse de rotation du monstre
    public NavMeshAgent navMeshAgent; // Référence au NavMeshAgent (si utilisé)

    public float health = 100f; // Points de vie du monstre
    public float maxHealth = 100f;
    public float attackPower = 10f; // Force d'attaque du monstre

    private void Update()
    {
        if (animator.GetBool("IsAttacking"))
        {
            if (player != null)
            {
                // Tourner vers le joueur
                Vector3 direction = (player.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

                // Déplacer vers le joueur
                if (navMeshAgent != null)
                {
                    navMeshAgent.SetDestination(player.position);
                }
                else
                {
                    transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                StartAttacking();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAttacking();
        }
    }

    private void StartAttacking()
    {
        // Active l'animation d'attaque
        animator.SetBool("IsAttacking", true);
    }

    private void StopAttacking()
    {
        // Désactive l'animation d'attaque
        animator.SetBool("IsAttacking", false);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Code pour gérer la mort du monstre
        Debug.Log("Monstre est mort !");
        // Vous pouvez ajouter des animations de mort ou détruire l'objet ici
        Destroy(gameObject);
    }
}
