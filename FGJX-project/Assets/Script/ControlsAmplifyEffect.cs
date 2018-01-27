using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "SpeedIncrease", menuName = "Effects/AmplifyControls", order = 1 )]
public class ControlsAmplifyEffect : BoostEffect {

    public float speedMultiplier = 2f;

    public GameObject animation;


    public override void EndEffect() {

        HoverCarControl.i.AmplifyMode = false;
    }

    public override void StartEffect() {

        HoverCarControl.i.AmplifyMode = true;

        Instantiate( animation );

    }
}
