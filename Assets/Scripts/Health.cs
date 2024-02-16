using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] int currentHealth = 10;
    [SerializeField] int maxHealth = 10;
    private StatusBar healthBar;
    public HealthBar UIHealthBar;

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
            return;
        if (sender.layer == gameObject.layer)
            return;

        currentHealth = Mathf.Clamp(currentHealth - dmgAmount, 0, maxHealth);

        if (healthBar != null)
        {
            healthBar.UpdateStatusBar(currentHealth, maxHealth);
        } else
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
            if (gameObject.tag == "Player") //check if it's the player dying
            {
            // reload the level
            SceneManager.LoadScene("LevelOne[MODIFIED]");
            }
        }
    }

}