using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] int currentHealth = 100;
    [SerializeField] int maxHealth = 100;
    public Slider healthSlider; // Ensure this is assigned via the Inspector

    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;

    [SerializeField]
    private bool isDead = false;

    private void Awake()
    {
        // If you're still using StatusBar for other purposes, keep this line; otherwise, it can be removed.
        // Initialize healthSlider properties in Awake or Start as needed
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

    public void InitializeHealth(int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = 100; // Consider using the parameter or a constant value if always 100
        healthSlider.maxValue = maxHealth; // Ensure slider's max value matches maxHealth
        healthSlider.value = currentHealth; // Set slider's current value to match health

        isDead = false;
    }

    public void GetHit(int dmgAmount, GameObject sender)
    {
        Debug.Log("HIT");
        if (isDead)
            return;
        if (sender.layer == gameObject.layer)
            return;

        currentHealth = Mathf.Clamp(currentHealth - dmgAmount, 0, maxHealth);
        healthSlider.value = currentHealth;

        if (currentHealth > 0)
        {
            OnHitWithReference?.Invoke(sender);
        }
        else
        {
            if (!isDead) // Check if not already dead to prevent multiple death events
            {
                OnDeathWithReference?.Invoke(sender);
                isDead = true;
                // Consider disabling the game object instead of destroying it, depending on your game's needs
                // Destroy(gameObject);
                gameObject.SetActive(false); // An alternative to destroying the object
            }
        }
    }
}
