using System.Collections;
using UnityEngine;

public class DamageEffect : MonoBehaviour
{
    public float flickerDuration = 1.0f;
    public float flickerSpeed = 0.1f;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Color damageColor = Color.red;

    void Awake()
    {
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Store the original color of the sprite
        originalColor = spriteRenderer.color;
    }

    // Call this method to start the damage effect
    public void TriggerDamageEffect()
    {
        StartCoroutine(FlickerEffect());
    }

    IEnumerator FlickerEffect()
    {
        float timer = 0;
        while (timer < flickerDuration)
        {
            // Apply the damage color
            spriteRenderer.color = damageColor;
            yield return new WaitForSeconds(flickerSpeed);
            // Revert to the original color
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(flickerSpeed);
            timer += flickerSpeed * 2;
        }
        // Ensure the sprite's color is reset to its original color after the effect
        spriteRenderer.color = originalColor;
    }
}