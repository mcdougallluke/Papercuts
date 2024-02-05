using UnityEngine;


public class Bullet : MonoBehaviour
{
    public float speed { get; set; }
    public Vector2 direction { get; set; }
    private float time = 0f;
    public Bullet clone = null;

    public void Initialize(float speed, Vector2 direction, Texture2D texture, Bullet clone)
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

    void Update()
    {
       this.transform.Translate(direction * speed * Time.deltaTime);
        time += Time.deltaTime;
       
        if (time > 3f)
        {
            Destroy(clone);
        }
    }
}
