using UnityEngine;
using System.Collections;

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

  int m_layerMask;

    private void Awake() {
        i = this; //Set singleton
    }

    void Start()
  {
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
	
  void Update()
  {

    // Main Thrust
    m_currThrust = 0.0f;
    float aclAxis = ( Input.GetAxis( "Vertical" ) * m_forwardAcl_P1 ) + ( Input.GetAxis( "Vertical_P2" ) * m_forwardAcl_P2 );
    if ( aclAxis > m_deadZone )
      m_currThrust = aclAxis;
    else if ( aclAxis < -m_deadZone )
      m_currThrust = aclAxis * m_backwardAcc;

    // Turning
    CurrentTurnAngle = 0.0f;
    float turnAxis = ( Input.GetAxis("Horizontal") * m_turnStrength_P1 ) + + ( Input.GetAxis( "Horizontal_P2" ) * m_turnStrength_P1 );
    if ( Mathf.Abs(turnAxis) > m_deadZone)
      CurrentTurnAngle = turnAxis;
  }

  void FixedUpdate()
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
        }
    }
}
