using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enable : MonoBehaviour
{
	public UnityEvent _Action;
	bool _enable = false;
	
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_enable)
		{

		}
    }

	public void SetEnable()
	{
		_enable = true;
	}

	
}
