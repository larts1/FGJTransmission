using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "SpeedIncrease", menuName = "Effects/SpeedMulti", order = 1 )]
public class SpeedMultiEffect : BoostEffect {

    public float speedMultiplier = 2f;

    public GameObject animation;

    GameObject window;

    public override void EndEffect() {
        Debug.Log( "End" );
		EffectManager.i.MuteMainAudio ( false );
        HoverCarControl.i.m_forwardAcl_P2 /= speedMultiplier;
        HoverCarControl.i.m_forwardAcl_P1 /= speedMultiplier;

        if ( window != null )
        {
            Destroy(window);
        }

    }

    public override void StartEffect() {
        Debug.Log( "Start" );
		EffectManager.i.MuteMainAudio ( true );
        HoverCarControl.i.m_forwardAcl_P2 *= speedMultiplier;
        HoverCarControl.i.m_forwardAcl_P1 *= speedMultiplier;

        window = Instantiate( animation );

    }
}
