using UnityEngine;

public class AutoProximityPickUp : MonoBehaviour
{
    public float pickUpRadius = 1.75f;
    public LayerMask whatIsPaper;
    public int paperCount { get; private set; } = 0;


    void Update()
    {
        // Automatically pick up the closest paper within pick-up radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pickUpRadius, whatIsPaper);
        if (colliders.Length > 0)
        {
            foreach (var collider in colliders)
            {
                PickUp(collider.gameObject); // Pick up each paper in range
            }
        }
    }

    void PickUp(GameObject Paper)
    {
        if (Paper != null)
        {
            Paper.SetActive(false); // Remove paper object from scene
            paperCount++; // Add paper to inventory
            //Debug.Log("Picked up a paper. Total papers: " + paperCount); //Print to console
        }
    }

    private void OnDrawGizmosSelected()
    {
        //visualize pick-up radius in the editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickUpRadius);
    }
}
