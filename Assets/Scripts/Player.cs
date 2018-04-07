using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	[SerializeField]
	private float speed;

	private float damage = 0;

    private const float fireDamage = 30;
    private const float criticalDamage = 300;

    private const int playerDoorShift = 2;

	private Vector2 direction;
	private Vector2 lastDirection;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetInput ();
		Move ();
	    updateDamage();
	}

	void Move() {
		transform.Translate (direction*speed*Time.deltaTime);
	}

	private void GetInput()
	{
		direction = Vector2.zero;

		if (Input.GetKey (KeyCode.W)) {
			direction += Vector2.up;
		}
		if (Input.GetKey (KeyCode.S)) {
			direction += Vector2.down;
		}
		if (Input.GetKey (KeyCode.A)) {
			direction += Vector2.left;
		}
		if (Input.GetKey (KeyCode.D)) {
			direction += Vector2.right;
		}

		lastDirection = direction;
	}


    private void updateDamage() {
        var damageBar = GameObject.Find("healthFade");
        damageBar.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, damage);
        //damageBar.transform.position = new Vector3(390 - damage/2, damageBar.transform.position.y, damageBar.transform.position.z);
       
        
    }

    private void addDamage(float volume) {
        damage += volume;
        if (damage >= criticalDamage)
        {
            var gameOver = GameObject.Find("gameOver");
            var image = gameOver.GetComponent<Image>();
            var tempColor = image.color;
            tempColor.a = 1f;
            image.color = tempColor;
        }
    }

	void OnCollisionEnter2D(Collision2D coll)
	{
	    if (coll.gameObject.tag == "Fire")
		{
		    FireEvent();
		    return;
		}

	    var module = lastDirection == Vector2.up || lastDirection == Vector2.right ? 1 : -1;
	    if (coll.gameObject.tag == "Door_Vertical" && (lastDirection == Vector2.up || lastDirection == Vector2.down))
	    {
	        GoToNextRoom(0, 9 * module, 0, playerDoorShift * module);
	    }
	    else if (coll.gameObject.tag == "Door_Horizontal" && (lastDirection == Vector2.right || lastDirection == Vector2.left))
	    {
	        GoToNextRoom(21 * module, 0, playerDoorShift * module, 0);
	    }
	}

    private void FireEvent()
    {
        addDamage(fireDamage);

        if (lastDirection == Vector2.up)
        {
            direction = Vector2.down;
        }
        else if (lastDirection == Vector2.down)
        {
            direction = Vector2.up;
        }
        else if (lastDirection == Vector2.left)
        {
            direction = Vector2.right;
        }
        else if (lastDirection == Vector2.right)
        {
            direction = Vector2.left;
        }

        speed = 8;
        Move();
        speed = 5;
    }

    private void GoToNextRoom(int cameraShiftX, int cameraShiftY, int playerShiftX, int playerShiftY)
    {
        var cameraTransform = FindObjectOfType<Camera>().GetComponent<Transform>();
        var currentCameraPosition = cameraTransform.position;
        var currentPlayerPosition = transform.position;

        cameraTransform.SetPositionAndRotation(new Vector3(currentCameraPosition.x + cameraShiftX, currentCameraPosition.y + cameraShiftY, currentCameraPosition.z), cameraTransform.rotation);
        transform.SetPositionAndRotation(new Vector3(currentPlayerPosition.x + playerShiftX, currentPlayerPosition.y + playerShiftY, currentPlayerPosition.z), transform.rotation);
    }

}
