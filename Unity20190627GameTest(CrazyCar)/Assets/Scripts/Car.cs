using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour {
    //public Controller controller;

    GameObject player;

    //private Queue
	// Use this for initialization
	void Start () {
        StartCoroutine(WaitToContect());
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player = gameObject;
        }
	}
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.collider.name);
    }
    private void OnCollisionStay(Collision collision)
    {
        //controller.SubSpeed();
    }
    IEnumerator WaitToContect()
    {
        while (player == null)
        {
            Debug.Log("没有值");
            yield return new WaitForSeconds(2);
        }
        if(player)
        Debug.Log(player.name);
    }
}
