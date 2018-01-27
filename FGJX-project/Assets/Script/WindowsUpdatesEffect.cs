using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "WindowsUpdates", menuName = "Effects/WindowsUpdates", order = 1 )]
public class WindowsUpdatesEffect : BoostEffect {

    public new float chance = 0.3f;
    public GameObject animation;

    GameObject window;
    public override void EndEffect() {
        if ( window != null )
            Destroy( window );
    }

    public override void StartEffect() {

        window = Instantiate( animation );
        window.transform.SetParent( window.transform.Find( "/UI/Canvas/" ), false );

    }

}

