using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "WindowsUpdates", menuName = "Effects/WindowsUpdates", order = 1 )]
public class WindowsUpdatesEffect : BoostEffect {

    public GameObject animation;

    GameObject window;
    public override void EndEffect() {
		EffectManager.i.MuteMainAudio ( false );
        if ( window != null )
            Destroy( window );
    }

    public override void StartEffect() {
        EffectManager.i.MuteMainAudio( true );
        window = Instantiate( animation );
        window.transform.SetParent( window.transform.Find( "/UI/Canvas/" ), false );

    }

}

