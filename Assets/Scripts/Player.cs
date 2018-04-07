using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField]
	private float speed;

	private Vector2 direction;
	private Vector2 lastDirection;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetInput ();
		Move ();
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
    
	void OnCollisionEnter2D(Collision2D coll)
	{
		//Debug.Log("OnCollisionEnter2D");
		if (coll.gameObject.tag == "Fire") {
			Debug.Log("get damage on fire");

			if (lastDirection == Vector2.up) {
				direction = Vector2.down;
			}
            else if (lastDirection == Vector2.down) {
			    direction = Vector2.up;
			}
            else if (lastDirection == Vector2.left) {
			    direction = Vector2.right;
			}
            else if (lastDirection == Vector2.right) {
			    direction = Vector2.left;
			}

		    speed = 8;
            Move ();
		    speed = 5;
		}
	}
}
