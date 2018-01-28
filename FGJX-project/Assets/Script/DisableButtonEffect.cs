using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu( fileName = "DisableRNGAxis", menuName = "Effects/DisableRNGAxis", order = 1 )]
public class DisableButtonEffect : BoostEffect {

    public GameObject animation;

    public override void EndEffect() {
        if ( axel == 0 ) {
            if ( player == 0 ) {
                HoverCarControl.i.m_forwardAcl_P1 = oldValue;
                HoverCarControl.i.m_turnStrength_P2 = oldValue2;
            } else {
                HoverCarControl.i.m_forwardAcl_P2 = oldValue;
                HoverCarControl.i.m_turnStrength_P1 = oldValue2;
            }
        } else {
            if ( player == 0 ) {
                HoverCarControl.i.m_turnStrength_P1 = oldValue;
                HoverCarControl.i.m_forwardAcl_P2 = oldValue2;
            } else {
                HoverCarControl.i.m_turnStrength_P2 = oldValue;
                HoverCarControl.i.m_forwardAcl_P1 = oldValue2;
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
                oldValue = HoverCarControl.i.m_forwardAcl_P1; //P1A1
                HoverCarControl.i.m_forwardAcl_P1 = 0;

                oldValue2 = HoverCarControl.i.m_turnStrength_P2; //P2A2
                HoverCarControl.i.m_turnStrength_P2 = 0;

            } else {
                oldValue = HoverCarControl.i.m_forwardAcl_P2; //P2A1
                HoverCarControl.i.m_forwardAcl_P2 = 0;

                oldValue2 = HoverCarControl.i.m_turnStrength_P1; //P1A1
                HoverCarControl.i.m_turnStrength_P1 = 0;
            }
        } else {
            if ( player == 0 ) {
                oldValue = HoverCarControl.i.m_turnStrength_P1; //P1A2
                HoverCarControl.i.m_turnStrength_P1 = 0;

                //P2A1
                oldValue2 = HoverCarControl.i.m_forwardAcl_P2; //P2A1
                HoverCarControl.i.m_forwardAcl_P2 = 0;
            } else {
                oldValue = HoverCarControl.i.m_turnStrength_P2; //P2A2
                HoverCarControl.i.m_turnStrength_P2 = 0;

                //P1A1
                oldValue2 = HoverCarControl.i.m_forwardAcl_P1; //P1A1
                HoverCarControl.i.m_forwardAcl_P1 = 0;
            }
        }

        Instantiate(animation);
    }

}
