using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SimpleCollectibleScript : MonoBehaviour 
{
	public bool rotate; // do you want it to rotate?

	public float rotationSpeed;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (rotate)
			transform.Rotate (Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			CoinManager.instance.AddCoins(10);
			Collect ();
		}
	}

	public void Collect()
	{
		Destroy (gameObject);
	}
}
