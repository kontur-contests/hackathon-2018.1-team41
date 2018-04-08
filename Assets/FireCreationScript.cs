using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCreationScript : MonoBehaviour {

	private static bool _created;

	private void Start()
	{
		CreateFires();
	}

	private static void CreateFires()
	{
		if (_created)
			return;
		_created = true;
		
		var firstFire = GameObject.Find("fire");
		var fireTransform = firstFire.GetComponent<Transform>();
		for (var i = 0; i < 50; ++i)
		{
			var x = (int)(Random.value * 100.0f) % 40 - 10;
			Debug.Log("x: " + x);
			var y = (int)(Random.value * 100.0f) % 20 - 5;
			Debug.Log("y: " + y);
			var existingFire = Instantiate(firstFire);
			var transform = existingFire.GetComponent<Transform>();
			transform.SetPositionAndRotation(new Vector3(x, y), fireTransform.rotation);
			existingFire.name = "fire_" + i;
		}
	}
}
