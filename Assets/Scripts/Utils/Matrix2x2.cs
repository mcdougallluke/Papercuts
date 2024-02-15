using UnityEngine;

public class Matrix2x2
{
    public float x1, y1;
    public float x2, y2;

    public Matrix2x2() 
    {
        x1 = y1 = 0;
    }

	private Vector2 RotateVector(Vector2 toRotate, float angle) 
	{
		angle = Mathf.Deg2Rad * angle;

        x1 = Mathf.Cos(angle);
        y1 = -Mathf.Sin(angle);
        x2 = Mathf.Sin(angle);
        y2 = Mathf.Cos(angle);

		float newX = (x1 * toRotate.x) + (y1 * toRotate.y);
		float newY = (x2 * toRotate.x) + (y2 * toRotate.y);

		return new Vector2(newX, newY).normalized;
	}
}
