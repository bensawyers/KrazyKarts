using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAnimation : MonoBehaviour
{
    [System.Serializable]
    public class Wheel
    {
        public Transform wheelTransform;
        public Vector3 axelAxis;
        public Vector3 steeringAxis;

        Vector3 m_NormalizedAxelAxis;
        Vector3 m_NormalizedSteeringAxis;
        Quaternion m_SteerlessLocalRotation;

        public void Setup()
        {
            m_NormalizedAxelAxis = axelAxis.normalized;
            m_NormalizedSteeringAxis = steeringAxis.normalized;
            m_SteerlessLocalRotation = wheelTransform.localRotation;
        }

        public void StoreDefaultRotation()
        {
            m_SteerlessLocalRotation = wheelTransform.localRotation;
        }

        public void SetToDefaultRotation()
        {
            wheelTransform.localRotation = m_SteerlessLocalRotation;
        }

        public void TurnWheel(float rotationAngle)
        {
            wheelTransform.Rotate(m_NormalizedAxelAxis, rotationAngle, Space.Self);
        }

        public void SteerWheel(float rotationAngle)
        {
            wheelTransform.Rotate(m_NormalizedSteeringAxis, rotationAngle, Space.World);
        }
    }

    [Tooltip("What kart do we want to listen to?")]
    public RaceCar carController;

    [Space]
    [Tooltip("The damping for the appearance of steering compared to the input.  The higher the number the less damping.")]
    public float steeringAnimationDamping = 10f;

    [Space]
    public WheelCollider frontWheelCollider;
    public WheelCollider rearWheelCollider;
    [Tooltip("The maximum angle in degrees that the front wheels can be turned away from their default positions, when the Steering input is either 1 or -1.")]
    public float maxSteeringAngle;
    [Tooltip("Information referring to the front left wheel of the kart.")]
    public Wheel frontLeftWheel;
    [Tooltip("Information referring to the front right wheel of the kart.")]
    public Wheel frontRightWheel;
    [Tooltip("Information referring to the rear left wheel of the kart.")]
    public Wheel rearLeftWheel;
    [Tooltip("Information referring to the rear right wheel of the kart.")]
    public Wheel rearRightWheel;

    float m_InverseFrontWheelRadius;
    float m_InverseRearWheelRadius;
    float m_SmoothedSteeringInput;

    void Start()
    {
        frontLeftWheel.Setup();
        frontRightWheel.Setup();
        rearLeftWheel.Setup();
        rearRightWheel.Setup();

        m_InverseFrontWheelRadius = 1f / frontWheelCollider.radius;
        m_InverseRearWheelRadius = 1f / rearWheelCollider.radius;
    }

    void FixedUpdate()
    {
        m_SmoothedSteeringInput = Mathf.MoveTowards(m_SmoothedSteeringInput, carController.InputVars.x,
            steeringAnimationDamping * Time.deltaTime);
    }

    void LateUpdate()
    {
        RotateWheels();
    }

    void RotateWheels()
    {
        frontLeftWheel.SetToDefaultRotation();
        frontRightWheel.SetToDefaultRotation();

        float speed = carController.LocalSpeed() * 10f;
        float rotationAngle = speed * Time.deltaTime * m_InverseFrontWheelRadius * Mathf.Rad2Deg;
        frontLeftWheel.TurnWheel(rotationAngle);
        frontRightWheel.TurnWheel(rotationAngle);

        rotationAngle = speed * Time.deltaTime * m_InverseRearWheelRadius * Mathf.Rad2Deg;

        rearLeftWheel.TurnWheel(rotationAngle);
        rearRightWheel.TurnWheel(rotationAngle);

        frontLeftWheel.StoreDefaultRotation();
        frontRightWheel.StoreDefaultRotation();

        rotationAngle = m_SmoothedSteeringInput * maxSteeringAngle;
        frontLeftWheel.SteerWheel(rotationAngle);
        frontRightWheel.SteerWheel(rotationAngle);
    }
}
