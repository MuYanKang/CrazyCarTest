using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class IEnumatorTest : MonoBehaviour {
    public GameObject[] objs;
	// Use this for initialization
	void Start () {
        StartCoroutine(Move(objs, 0));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator Move(GameObject[] obj,int index)
    {
        
        Debug.Log(index);
        while (obj[index].transform.position.y < 1)
        {
            obj[index].transform.position += Vector3.up * Time.deltaTime;
            yield return null;
        }
        if (index + 1 < obj.Length)
        {
            StartCoroutine(Move(obj, index + 1));
        }
    }
}
