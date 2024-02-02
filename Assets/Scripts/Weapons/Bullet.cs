using UnityEngine;


public class Bullet : MonoBehaviour
{
    public float speed { get; set; }
    public Vector2 direction { get; set; }

    public Bullet(float speed, Vector2 direction)
    {
        this.speed = speed;
        this.direction = direction;
    }

    void Update()
    {
       this.transform.Translate(direction * speed * Time.deltaTime);
       
    }
}
