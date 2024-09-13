using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MonsterHealthBar : MonoBehaviour
{
    public Slider healthBar; // Référence au Slider de la barre de vie du joueur
    public MonsterAI monsterAI; // Référence au script PlayerBehavior

    private void Start()
    {
        if (monsterAI != null && healthBar != null)
        {
            // Initialiser la barre de vie avec les valeurs du joueur
            healthBar.maxValue = monsterAI.maxHealth; // Assurez-vous que maxHealth est public ou exposez-le dans PlayerBehavior
            healthBar.value = monsterAI.health; // Assurez-vous que currentHealth est public ou exposez-le dans PlayerBehavior
        }
    }

    private void Update()
    {
        if (monsterAI != null && healthBar != null)
        {
            // Mettre à jour la barre de vie avec la vie actuelle du joueur
            healthBar.value = monsterAI.health;
        }
    }
}
