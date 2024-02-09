using UnityEngine;


public class Bullet : MonoBehaviour
{
    public float speed { get; set; }
    public Vector2 direction { get; set; }
    private float time = 0f;
    public GameObject clone = null;

    public void Initialize(float speed, Vector2 direction, Texture2D texture, GameObject clone)
    {
        //Sets speed and direction
        this.speed = speed;
        this.direction = direction;
        this.clone = clone;

        //Assigns texture
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
		Sprite newSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
		spriteRenderer.sprite = newSprite;
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
	    Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        if (enemy != null)
        {
            Debug.Log(enemy.ToString());
            enemy.TakeDamage(25f);
        }

        Destroy(clone);
	}

	void Update()
    {
       Vector2 newPosition = direction * speed * Time.deltaTime;



       this.transform.Translate(newPosition);
       time += Time.deltaTime;
       
        //After 3 seconds destroy bullet
        if (time > 3f)
        {
            Destroy(clone);
        }
    }
}
