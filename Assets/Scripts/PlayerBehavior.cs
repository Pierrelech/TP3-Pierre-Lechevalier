using UnityEngine;
using UnityEngine.UI; // Nécessaire pour utiliser UI components

public class PlayerBehavior : MonoBehaviour
{
    public float speed = 5f;          // Vitesse de déplacement
    public float jumpForce = 5f;     // Force du saut
    public Transform cameraTransform; // Référence à la caméra

    private Rigidbody rb;            // Référence au Rigidbody du joueur
    private Animator animator;       // Référence à l'Animator du sous-objet Armature
    private bool isGrounded;         // Vérifie si le joueur est au sol

    public float health = 100f;      // Points de vie du joueur
    public float maxHealth = 100f;
    public float attackPower = 10f; // Force d'attaque du joueur

    // Références pour la mise à jour de l'UI
    public Slider healthBar;         // Référence au Slider de la barre de vie

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Récupère le Rigidbody du joueur
        animator = GetComponentInChildren<Animator>(); // Récupère l'Animator du sous-objet Armature

        // Initialise la barre de vie
        if (healthBar != null)
        {
            healthBar.maxValue = health; // Valeur maximale de la barre de vie
            healthBar.value = health;    // Valeur initiale de la barre de vie
        }
    }

    void Update()
    {
        // Récupère les entrées clavier
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D ou flèches gauche/droite
        float moveVertical = Input.GetAxis("Vertical");     // W/S ou flèches haut/bas
        animator.SetBool("Clique", false);

        // Détermine la direction de mouvement basée sur la caméra
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Ignore le mouvement vertical (Y) de la caméra pour le déplacement du joueur
        forward.y = 0;
        right.y = 0;

        // Normalise les directions pour éviter des mouvements plus rapides en diagonale
        forward.Normalize();
        right.Normalize();

        // Calcule le vecteur de déplacement
        Vector3 movement = (forward * moveVertical + right * moveHorizontal).normalized * speed * Time.deltaTime;

        // Applique le déplacement
        rb.MovePosition(transform.position + movement);

        // Met à jour la variable speed de l'Animator
        animator.SetFloat("speed", movement.magnitude);

        // Gère l'animation de saut
        animator.SetBool("Jump", !isGrounded);

        // Si le joueur se déplace, change son orientation
        if (movement != Vector3.zero)
        {
            // Tourne le joueur dans la direction du mouvement
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f); // Lissage de la rotation
        }

        // Sauter
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            // Lorsque le joueur saute, on met à jour le paramètre de saut
            animator.SetBool("Jump", true);
        }

        // Détecte le clic gauche de la souris
        if (Input.GetMouseButtonDown(0))
        {
            // Code à exécuter lorsque le bouton gauche de la souris est cliqué
            HandleMouseClick();
            animator.SetBool("Clique", true);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Vérifie si le personnage touche un objet avec le tag "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            // Lorsqu'il touche le sol, on met à jour le paramètre de saut
            animator.SetBool("Jump", false);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Vérifie si le personnage quitte un objet avec le tag "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void HandleMouseClick()
    {
        // Code à exécuter lorsque le bouton gauche de la souris est cliqué
        Debug.Log("Clique gauche de la souris détecté !");
        // Ajouter ici le comportement spécifique pour le clic gauche de la souris
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health < 0) health = 0;

        // Met à jour la barre de vie
        if (healthBar != null)
        {
            healthBar.value = health;
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Code pour gérer la mort du joueur
        Debug.Log("Joueur est mort !");
        // Vous pouvez ajouter des animations de mort ou redémarrer le niveau ici
        // Exemple : Destroy(gameObject);
        // Exemple : SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
