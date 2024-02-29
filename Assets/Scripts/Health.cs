using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int currentHealth = 10;
    public int maxHealth = 10;
    private StatusBar healthBar;
    public HealthBar UIHealthBar;

    public GameObject gameOverCanvas; 

    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;

    [SerializeField]
    private bool isDead = false;
    private void Awake()
    {
        healthBar = GetComponentInChildren<StatusBar>();
         if (UIHealthBar != null) UIHealthBar.SetMaxHealth(maxHealth);
    }

    public void InitializeHealth(int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;
        isDead = false;
    }

    public void GetHit(int dmgAmount, GameObject sender)
    {
        if (isDead)
        {
            if (gameOverCanvas != null) gameOverCanvas.SetActive(true);
        }
        if (!sender.CompareTag("Health") && sender.layer == gameObject.layer)
            return;

        currentHealth = Mathf.Clamp(currentHealth - dmgAmount, 0, maxHealth);

        if (healthBar != null)
        {
            healthBar.UpdateStatusBar(currentHealth, maxHealth);
        } else if (UIHealthBar != null)
        {
            UIHealthBar.SetHealth(currentHealth);
        }

        if (currentHealth > 0)
        {
            OnHitWithReference?.Invoke(sender);
        }
        else
        {
            OnDeathWithReference?.Invoke(sender);
            isDead = true;
            Destroy(gameObject);

            if (gameObject.tag == "Player") 
            {
                gameOverCanvas.SetActive(true);
            }
        }
    }

}