using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] private float destructCounter = 20f;

    // Update is called once per frame
    void Update()
    {
        destructCounter -= Time.deltaTime;
        if(destructCounter <= 0f)
		{
            Destruct();
		}
    }

    void Destruct()
	{
        Destroy(gameObject);
	}
}
