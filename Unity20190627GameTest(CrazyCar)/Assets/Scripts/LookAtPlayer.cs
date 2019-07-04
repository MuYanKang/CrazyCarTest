using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour {

    public GameObject player;
    public Vector3 lookVector3;
    public float radius;
    float targethorizontalVelocity;
    float time = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {     
        
    }
    private void FixedUpdate()
    {
        float horizontalVelocity = 0;
        if (Vector3.Distance(player.transform.position, transform.position) > radius+0.5)
        {
            horizontalVelocity = -90;
        }
        else if(Vector3.Distance(player.transform.position, transform.position) < radius-0.5)
        {
            horizontalVelocity = 90;
        }
        targethorizontalVelocity = Mathf.Lerp(targethorizontalVelocity, horizontalVelocity, 0.05f);
        transform.rotation = Quaternion.LookRotation(new Vector3(player.transform.position.x - transform.position.x, 0, player.transform.position.z - transform.position.z));
        transform.rotation = transform.rotation * Quaternion.Euler(lookVector3+new Vector3(0,0, targethorizontalVelocity));       
        GetComponent<Rigidbody>().velocity = transform.TransformVector(5, 0, 0) ;
    }
}
