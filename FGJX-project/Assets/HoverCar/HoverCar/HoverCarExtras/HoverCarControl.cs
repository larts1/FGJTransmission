﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class HoverCarControl : MonoBehaviour
{

    #region Public variables
    public float m_hoverForce = 9.0f;
    //Force of hover
    public float m_StabilizedHoverHeight = 2.0f;
    public float m_backwardAcc = 50000f;

    public float m_forwardAcl_P1 = 100.0f;
    public float m_forwardAcl_P2 = 100.0f;

    public float m_turnStrength_P1 = 10f;
    public float m_turnStrength_P2 = 10f;

    public bool AmplifyMode = false;

    public static HoverCarControl i;
    #endregion

    //Strength of the turn
    float CurrentTurnAngle = 0.0f;
    //Backwords/reverse Acceleration of car
    float m_currThrust = 0.0f;

    Rigidbody m_body;
    float m_deadZone = 0.1f;

    //Height of hover
    public GameObject[] HoverPointsGameObjects;

    //Boost controls
    public ParticleSystem left_boost;
    public ParticleSystem right_boost;

    //flags
    private bool flg1 = true;
    public bool flg2 = false;

    GameObject Light1;
    GameObject Light2;
    int lightState = 0;
    float iDontknowDontAsk = 0;
    int[] ChkPoints = {0, 0, 0};
    public Text winTxt;

    Vector3 OldPosition;

  int m_layerMask;

    private void Awake() {
        i = this; //Set singleton
    }

    void Start()
  {
        Light1 = GameObject.Find("left_light");
        Light2 = GameObject.Find("right_light");
        m_body = GetComponent<Rigidbody>();

    m_layerMask = 1 << LayerMask.NameToLayer("Characters");
    m_layerMask = ~m_layerMask;
  }

  void OnDrawGizmos()
  {

    //  Hover Force
    RaycastHit hit;
    for (int i = 0; i < HoverPointsGameObjects.Length; i++)
    {
      var hoverPoint = HoverPointsGameObjects [i];
      if (Physics.Raycast(hoverPoint.transform.position, 
                          -Vector3.up, out hit,
                          m_StabilizedHoverHeight, 
                          m_layerMask))
      {
        Gizmos.color = Color.green;
				//Color if correctly alligned
        Gizmos.DrawLine(hoverPoint.transform.position, hit.point);
        Gizmos.DrawSphere(hit.point, 0.5f);
      } else
      {
        Gizmos.color = Color.red;
				//Color if incorrectly alligned
        Gizmos.DrawLine(hoverPoint.transform.position, 
                       hoverPoint.transform.position - Vector3.up * m_StabilizedHoverHeight);
      }
    }
  }

    float turnAxis, aclAxis;
  void Update()
  {

        if ( AmplifyMode ) {

            turnAxis = ( ( Input.GetAxis("Horizontal") * m_turnStrength_P1 ) * Mathf.Abs( Input.GetAxis( "Horizontal_P2" ) ) ) 
                + ( Mathf.Abs( Input.GetAxis( "Horizontal" ) ) * Input.GetAxis( "Horizontal_P2" ) * m_turnStrength_P1 );

            aclAxis = ( Input.GetAxis( "Vertical" ) * m_forwardAcl_P1 ) + ( Input.GetAxis( "Vertical_P2" ) * m_forwardAcl_P2 );

        } else {

            aclAxis = ( Input.GetAxis( "Vertical" ) * m_forwardAcl_P1 ) + ( Input.GetAxis( "Vertical_P2" ) * m_forwardAcl_P2 );

            turnAxis = ( Input.GetAxis("Horizontal") * m_turnStrength_P1 ) + ( Input.GetAxis( "Horizontal_P2" ) * m_turnStrength_P2 );

        }

    // Main Thrust
    m_currThrust = 0.0f;
        if (aclAxis > m_deadZone)
        {
            m_currThrust = aclAxis;
            left_boost.Play();
            right_boost.Play();
        }
        else if (aclAxis < -m_deadZone)
            m_currThrust = aclAxis * m_backwardAcc;
        else
        {
            left_boost.Stop();
            right_boost.Stop();
        }

    // Turning
    CurrentTurnAngle = 0.0f;
    if ( Mathf.Abs(turnAxis) > m_deadZone)
      CurrentTurnAngle = turnAxis;
  }

  void FixedUpdate()
  {
    if (flg1)
    {         
        flg1 = false;
        StartCoroutine(SavePLayerPosition());
    }
    if (flg2)
    {
            if(iDontknowDontAsk < Time.time)
            {
                iDontknowDontAsk = Time.time + (float)0.25;
                if (lightState % 2 == 0)
                {
                    if (Light1.activeSelf)
                        Light1.SetActive(false);
                    else
                        Light1.SetActive(true);
                }
                else
                {
                    if (Light2.activeSelf)
                        Light2.SetActive(false);
                    else
                        Light2.SetActive(true);
                }
                lightState = lightState == 0 ? 1 : 0;
            }
            
        }
        //  Hover Force
        RaycastHit hit;
    for (int i = 0; i < HoverPointsGameObjects.Length; i++)
    {
      var hoverPoint = HoverPointsGameObjects [i];
      if (Physics.Raycast(hoverPoint.transform.position, 
                          -Vector3.up, out hit,
                          m_StabilizedHoverHeight,
                          m_layerMask))
        m_body.AddForceAtPosition(Vector3.up 
          * m_hoverForce
          * (1.0f - (hit.distance / m_StabilizedHoverHeight)), 
                                  hoverPoint.transform.position);
      else
      {
        if (transform.position.y > hoverPoint.transform.position.y)
          m_body.AddForceAtPosition(
            hoverPoint.transform.up * m_hoverForce,
            hoverPoint.transform.position);
        else
					//adding force to car
          m_body.AddForceAtPosition(
            hoverPoint.transform.up * -m_hoverForce,
            hoverPoint.transform.position);
      }
    }

    // Forward
    if (Mathf.Abs(m_currThrust) > 0)
      m_body.AddForce(transform.forward * m_currThrust);

    // Turn
    if (CurrentTurnAngle > 0)
    {
      m_body.AddRelativeTorque(Vector3.up * CurrentTurnAngle );
    } else if (CurrentTurnAngle < 0)
    {
      m_body.AddRelativeTorque(Vector3.up * CurrentTurnAngle );
    }
  }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUpTag"))
        {
            other.gameObject.SetActive(false);
            EffectManager.i.NextPickUpEffect();
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("cba");
            GameObject.FindGameObjectWithTag("Player").transform.position = OldPosition;
        }
        else if (other.gameObject.CompareTag("CheckPoint"))
        {       
            switch(other.gameObject.name)
            {
                case "CheckPoint1":
                    ChkPoints[0] = 1;
                    break;
                case "CheckPoint2":
                   if(ChkPoints[0]==1)
                       ChkPoints[1] = 1;
                    break;
                case "CheckPoint3":
                    if (ChkPoints[0] == 1 && ChkPoints[1] == 1)
                        WinTxt();
                    break;    
            } 
        } 

    }
    private void WinTxt()
    {
        winTxt.text = "Your time was " + Time.time;
    }
    IEnumerator SavePLayerPosition()
    {
        Vector3 dwn = GameObject.FindGameObjectWithTag("Player").transform.TransformDirection(Vector3.down);
        if (Physics.Raycast(transform.position, dwn, 10))
        {
            OldPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            yield return new WaitForSeconds(5);
        }          
        flg1 = true;
    }

}
