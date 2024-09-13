using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public Slider healthBar; // Référence au Slider de la barre de vie du joueur
    public PlayerBehavior playerBehavior; // Référence au script PlayerBehavior

    private void Start()
    {
        if (playerBehavior != null && healthBar != null)
        {
            // Initialiser la barre de vie avec les valeurs du joueur
            healthBar.maxValue = playerBehavior.maxHealth; // Assurez-vous que maxHealth est public ou exposez-le dans PlayerBehavior
            healthBar.value = playerBehavior.health; // Assurez-vous que currentHealth est public ou exposez-le dans PlayerBehavior
        }
    }

    private void Update()
    {
        if (playerBehavior != null && healthBar != null)
        {
            // Mettre à jour la barre de vie avec la vie actuelle du joueur
            healthBar.value = playerBehavior.health;
        }
    }
}
