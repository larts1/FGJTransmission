using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "SpeedIncrease", menuName = "Effects/RotationMulti", order = 1 )]
public class RotationMultiEffect : BoostEffect {

    public float speedMultiplier = 2f;

    public GameObject animation;

    public override void EndEffect() {
        Debug.Log( "End" );

        HoverCarControl.i.m_turnStrength_P2 /= speedMultiplier;
        HoverCarControl.i.m_turnStrength_P1 /= speedMultiplier;
    }

    public override void StartEffect() {
        Debug.Log( "Start" );

        HoverCarControl.i.m_turnStrength_P2 *= speedMultiplier;
        HoverCarControl.i.m_turnStrength_P1 *= speedMultiplier;

        Instantiate( animation );

    }
}
