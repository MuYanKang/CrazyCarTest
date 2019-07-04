using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Controller : MonoBehaviour,IPointerDownHandler,IPointerUpHandler {
    public GameObject player;


    bool pointDown;
    Vector3 pointerDownCenter;

    float speed = 400;
    bool recoverSpeed=true;
    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDownCenter = Input.mousePosition;
        pointDown = true;
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
        if (pointDown)
        {
            //player.GetComponent<Rigidbody>().velocity=(player.transform.TransformVector(new Vector3(0, 0, 500 * Time.deltaTime)));
            //player.GetComponent<Rigidbody>().MovePosition(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 100 * Time.deltaTime));
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.LookRotation(new Vector3(Input.mousePosition.x,0, Input.mousePosition.y) - new Vector3(pointerDownCenter.x,0, pointerDownCenter.y)), 0.2f);
        }
        if (recoverSpeed)
        {      
            speed=Mathf.Lerp(speed, 500, 0.1f);
        }
    }
    private void FixedUpdate()
    {
        if (pointDown)
        {
            player.GetComponent<Rigidbody>().velocity=(Vector3.Normalize(new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y)-new Vector3(pointerDownCenter.x,0,pointerDownCenter.y)) *speed * Time.fixedDeltaTime);          
        }
    }
    public void SubSpeed()
    {
        StopAllCoroutines();
        StartCoroutine(WaitToRecover());
        if (speed <= 50) return;
        speed -= 300 * Time.deltaTime / 2;     
    }
    IEnumerator WaitToRecover()
    {
        recoverSpeed = false;
        yield return new WaitForSeconds(0.3f);
        recoverSpeed = true
            ;
    }

}
