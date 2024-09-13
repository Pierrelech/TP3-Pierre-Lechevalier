using UnityEngine;
using UnityEngine.UI; // N�cessaire pour utiliser UI components

public class PlayerBehavior : MonoBehaviour
{
    public float speed = 5f;          // Vitesse de d�placement
    public float jumpForce = 5f;     // Force du saut
    public Transform cameraTransform; // R�f�rence � la cam�ra

    private Rigidbody rb;            // R�f�rence au Rigidbody du joueur
    private Animator animator;       // R�f�rence � l'Animator du sous-objet Armature
    private bool isGrounded;         // V�rifie si le joueur est au sol

    public float health = 100f;      // Points de vie du joueur
    public float maxHealth = 100f;
    public float attackPower = 10f; // Force d'attaque du joueur

    // R�f�rences pour la mise � jour de l'UI
    public Slider healthBar;         // R�f�rence au Slider de la barre de vie

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // R�cup�re le Rigidbody du joueur
        animator = GetComponentInChildren<Animator>(); // R�cup�re l'Animator du sous-objet Armature

        // Initialise la barre de vie
        if (healthBar != null)
        {
            healthBar.maxValue = health; // Valeur maximale de la barre de vie
            healthBar.value = health;    // Valeur initiale de la barre de vie
        }
    }

    void Update()
    {
        // R�cup�re les entr�es clavier
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D ou fl�ches gauche/droite
        float moveVertical = Input.GetAxis("Vertical");     // W/S ou fl�ches haut/bas
        animator.SetBool("Clique", false);

        // D�termine la direction de mouvement bas�e sur la cam�ra
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Ignore le mouvement vertical (Y) de la cam�ra pour le d�placement du joueur
        forward.y = 0;
        right.y = 0;

        // Normalise les directions pour �viter des mouvements plus rapides en diagonale
        forward.Normalize();
        right.Normalize();

        // Calcule le vecteur de d�placement
        Vector3 movement = (forward * moveVertical + right * moveHorizontal).normalized * speed * Time.deltaTime;

        // Applique le d�placement
        rb.MovePosition(transform.position + movement);

        // Met � jour la variable speed de l'Animator
        animator.SetFloat("speed", movement.magnitude);

        // G�re l'animation de saut
        animator.SetBool("Jump", !isGrounded);

        // Si le joueur se d�place, change son orientation
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
            // Lorsque le joueur saute, on met � jour le param�tre de saut
            animator.SetBool("Jump", true);
        }

        // D�tecte le clic gauche de la souris
        if (Input.GetMouseButtonDown(0))
        {
            // Code � ex�cuter lorsque le bouton gauche de la souris est cliqu�
            HandleMouseClick();
            animator.SetBool("Clique", true);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // V�rifie si le personnage touche un objet avec le tag "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            // Lorsqu'il touche le sol, on met � jour le param�tre de saut
            animator.SetBool("Jump", false);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // V�rifie si le personnage quitte un objet avec le tag "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void HandleMouseClick()
    {
        // Code � ex�cuter lorsque le bouton gauche de la souris est cliqu�
        Debug.Log("Clique gauche de la souris d�tect� !");
        // Ajouter ici le comportement sp�cifique pour le clic gauche de la souris
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health < 0) health = 0;

        // Met � jour la barre de vie
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
        // Code pour g�rer la mort du joueur
        Debug.Log("Joueur est mort !");
        // Vous pouvez ajouter des animations de mort ou red�marrer le niveau ici
        // Exemple : Destroy(gameObject);
        // Exemple : SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
