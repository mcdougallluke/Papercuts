using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] int currentHealth = 100;
    [SerializeField] int maxHealth = 100;
    [SerializeField] StatusBar healthBar;

    public UnityEvent<GameObject> OnHitWithReference, OnDeathWithReference;

    [SerializeField]
    private bool isDead = false;

    private void Awake()
    {
        healthBar = GetComponentInChildren<StatusBar>();
    }

    public void InitializeHealth(int healthValue)
    {
        currentHealth = healthValue;
        maxHealth = healthValue;
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
        healthBar.UpdateStatusBar(currentHealth, maxHealth);

        if (currentHealth > 0)
        {
            OnHitWithReference?.Invoke(sender);
        }
        else
        {
            OnDeathWithReference?.Invoke(sender);
            isDead = true;
            Destroy(gameObject);
        }
    }
}