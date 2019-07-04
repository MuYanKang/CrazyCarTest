using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Controller_3 : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
{
    public GameObject player;
    public GameObject body;
    
    bool pointDown;
    Vector3 hitPoint;
    Vector3 playPosition;

    public void OnPointerDown(PointerEventData eventData)
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, 1))
        {
            hitPoint = hit.point;
            pointDown = true;
            playPosition = player.transform.position;
        }
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pointDown = false;
    }

    // Use this for initialization
    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
        
        if (body)
        {
            body.transform.position = Vector3.Lerp(body.transform.position, player.transform.GetChild(0).position, 0.5f);
            body.transform.rotation = Quaternion.Lerp(body.transform.rotation, player.transform.rotation, 0.2f);
        }
        
    }
    private void FixedUpdate()
    {
        if (pointDown)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, 1))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.red, 3);
                player.GetComponent<Rigidbody>().velocity = Vector3.Lerp(player.GetComponent<Rigidbody>().velocity, Vector3.Normalize(hit.point - hitPoint) * Vector3.Distance(hit.point, hitPoint) * 80,0.3f) ; 
                //player.GetComponent<Rigidbody>().velocity = Vector3.Lerp(player.GetComponent<Rigidbody>().velocity, player.transform.forward*20,0.3f) ; 

                if (Vector3.Distance(hit.point , hitPoint) > 0.05f)
                {
                    player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.LookRotation(hit.point - hitPoint), 0.3f);
                }
                hitPoint = hit.point;
            }
        }
    }
}
