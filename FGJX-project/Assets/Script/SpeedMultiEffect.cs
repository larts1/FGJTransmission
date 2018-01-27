using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "SpeedIncrease", menuName = "Effects/SpeedMulti", order = 1 )]
public class SpeedMultiEffect : BoostEffect {

    public float speedMultiplier = 2f;

    public override void EndEffect() {
        Debug.Log( "End" );

        HoverCarControl.i.m_forwardAcl /= speedMultiplier;
    }

    public override void StartEffect() {
        Debug.Log( "Start" );

        HoverCarControl.i.m_forwardAcl *= speedMultiplier;

    }
}
