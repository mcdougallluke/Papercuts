using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject weapon;
    private bool attacking;


    // Start is called before the first frame update
    void Start()
    {
        weapon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate the weapon to look at mouse
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
		Vector3 direction = (mousePos - weapon.transform.position).normalized;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		weapon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

		if (Input.GetKey(KeyCode.LeftShift))
        {
            //Execute attack animation
            weapon.SetActive(true);
        }

        //If weapon active, execute attack animation
        if(weapon.activeSelf)
        {
            //weapon.SetActive(false);
        }

	}
}
