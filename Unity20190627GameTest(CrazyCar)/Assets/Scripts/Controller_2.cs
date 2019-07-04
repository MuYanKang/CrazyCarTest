using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Controller_2 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject player;
    public GameObject body;
    public GameObject joyStick;
    public GameObject mainCamera;
    Vector3 mainCameraWithPlayerDistanceOffset;
    bool pointDown;
    Vector3 pointerDownCenter;

    float speed = 400;
    bool recoverSpeed = true;
    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDownCenter = Input.mousePosition;
        pointDown = true;
        joyStick.transform.position = Input.mousePosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pointDown = false;
        joyStick.transform.GetChild(0).transform.localPosition = Vector3.zero;
    }


    // Use this for initialization
    void Start()
    {
        joyStick = transform.GetChild(0).gameObject;
        mainCameraWithPlayerDistanceOffset = mainCamera.transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (pointDown)
        {
            //player.GetComponent<Rigidbody>().velocity=(player.transform.TransformVector(new Vector3(0, 0, 500 * Time.deltaTime)));
            //player.GetComponent<Rigidbody>().MovePosition(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 100 * Time.deltaTime));
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, Quaternion.LookRotation(new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y) - new Vector3(pointerDownCenter.x, 0, pointerDownCenter.y)), 0.2f);
            joyStick.transform.GetChild(0).transform.position =  Vector3.Normalize(Input.mousePosition-pointerDownCenter)*Mathf.Clamp(Vector3.Distance(Input.mousePosition,pointerDownCenter),0,200)+pointerDownCenter;
            //joyStick.transform.GetChild(0).transform.position=Vector3.clam
        }
        if (recoverSpeed)
        {
            speed = Mathf.Lerp(speed, 300, 0.1f);
        }
        if (body)
        {
            body.transform.position = Vector3.Lerp(body.transform.position, player.transform.GetChild(0).position,0.8f);
            body.transform.rotation = Quaternion.Lerp(body.transform.rotation, player.transform.rotation, 0.2f);
        }
    }
    private void FixedUpdate()
    {
        player.GetComponent<Rigidbody>().velocity = player.transform.forward * 9;
        if (pointDown)
        {
            if (Vector3.Distance(new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y) , new Vector3(pointerDownCenter.x, 0, pointerDownCenter.y)) < 0.8f) return;
            //player.GetComponent<Rigidbody>().velocity = (Vector3.Normalize(new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y) - new Vector3(pointerDownCenter.x, 0, pointerDownCenter.y)) * speed * Time.fixedDeltaTime);

        }
        if (mainCamera)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, player.transform.position + mainCameraWithPlayerDistanceOffset, 0.05f);
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
