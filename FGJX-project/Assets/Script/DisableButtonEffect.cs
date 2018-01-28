using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu( fileName = "DisableRNGAxis", menuName = "Effects/DisableRNGAxis", order = 1 )]
public class DisableButtonEffect : BoostEffect {

    public GameObject animation;

    public override void EndEffect() {
        if ( axel == 0 ) {
            if ( player == 0 ) {
                HoverCarControl.i.m_forwardAcl_P1 = oldValue;
            } else {
                HoverCarControl.i.m_forwardAcl_P2 = oldValue;
            }
        } else {
            if ( player == 0 ) {
                HoverCarControl.i.m_turnStrength_P1 = oldValue;
            } else {
                HoverCarControl.i.m_turnStrength_P2 = oldValue;
            }
        }
    }

    int player = 0;
    int axel = 0;

    float oldValue = 0;
    float oldValue2 = 0;
    public override void StartEffect() {
        player = Random.value < 0.5f ? 1 : 0;
        axel = Random.value < 0.5f ? 1 : 0;

        if ( axel == 0 ) {
            if ( player == 0 ) {
                oldValue = HoverCarControl.i.m_forwardAcl_P1;
                HoverCarControl.i.m_forwardAcl_P1 = 0;
            } else {
                oldValue = HoverCarControl.i.m_forwardAcl_P2;
                HoverCarControl.i.m_forwardAcl_P2 = 0;
            }
        } else {
            if ( player == 0 ) {
                oldValue = HoverCarControl.i.m_turnStrength_P1;
                HoverCarControl.i.m_turnStrength_P1 = 0;
            } else {
                oldValue = HoverCarControl.i.m_turnStrength_P2;
                HoverCarControl.i.m_turnStrength_P2 = 0;
            }
        }

        Instantiate(animation);
    }

}
