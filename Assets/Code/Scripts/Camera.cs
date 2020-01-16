using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
	public GameObject _targetToFollow;
	public float _offsetY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		transform.position = new Vector3(_targetToFollow.transform.position.x, _targetToFollow.transform.position.y + _offsetY, transform.position.z);
    }
}
