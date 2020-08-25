using UnityEngine;
using System.Collections;

public class MoveLeft : MonoBehaviour {

    //So unity could use it from editor
    public float speed = 10;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //When you multiply with Time.deltaTime you essentially express: 
        //I want to move this object 10 meters per second instead of 10 meters per frame.
        transform.position += Vector3.left * speed * Time.deltaTime;
	
	}
}
