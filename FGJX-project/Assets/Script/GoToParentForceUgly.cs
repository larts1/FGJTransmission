using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToParentForceUgly : MonoBehaviour {

    private void Awake() {
        transform.SetParent( transform.Find( "/UI/Canvas/UIElements" ), false );
    }

}
