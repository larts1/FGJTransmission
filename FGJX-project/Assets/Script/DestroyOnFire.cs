using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnFire : MonoBehaviour {

    int minTime = 0;
	// Update is called once per frame
	void Update () {
		if ( Input.anyKeyDown && ++minTime > 3 ) {
            Destroy( gameObject );
        }
	}

}
