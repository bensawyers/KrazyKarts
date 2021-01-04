using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceCar : MonoBehaviour
{
    [System.Serializable]
    public struct Stats
	{
        public float TopSpeed;

        public float Acceleration;

        public float ReverseSpeed;

        public float ReverseAcceleration;

        [Range(0.2f, 1)]
        public float AccelerationCurve;

        public float Braking;

        public float CoastingDrag;

        [Range(0, 1)]
        public float Grip;

        public float Steer;

        [Range(0, 1)]
        public float Suspension;
	}

    public Rigidbody Rigidbody { get; private set; }
    public Vector2 InputVars { get; private set; }

    public RaceCar.Stats presetStats = new RaceCar.Stats
    {
        TopSpeed = 10f,
        Acceleration = 5f,
        ReverseAcceleration = 5f,
        ReverseSpeed = 5f,
        AccelerationCurve = 4f,
        Braking = 10f,
        CoastingDrag = 4f,
        Grip = 0.95f,
        Steer = 5f,
        Suspension = 0.2f
    };

    [Header("Vehicle Physics")]
    public Transform CenterOfMass;
    public Transform[] Wheels;
    public Transform SuspensionBody;

    GameInput[] gameInputs;
    Vector3 suspensionNeutralPos;
    Quaternion suspensionNeutralRot;
    bool canMove = false;

    // Start is called before the first frame update
    void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        gameInputs = GetComponents<GameInput>();
        suspensionNeutralPos = SuspensionBody.transform.localPosition;
        suspensionNeutralRot = SuspensionBody.transform.localRotation;
        StartCoroutine(waitForCountdown());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        InputVars = Vector2.zero;
        for(int i = 0; i < gameInputs.Length; i++)
        {
            InputVars = gameInputs[i].GetInputs();
        }
        

        Rigidbody.centerOfMass = Rigidbody.transform.InverseTransformPoint(CenterOfMass.position);

        float accel = InputVars.y;
        float turn = InputVars.x;

        MoveVehicle(accel, turn);

    }

    void MoveVehicle(float accelInput, float turnInput)
    {
        if (canMove)
        {
        // manual acceleration curve coefficient scalar
        float accelerationCurveCoeff = 5;
        Vector3 localVel = transform.InverseTransformVector(Rigidbody.velocity);

        bool accelDirectionIsFwd = accelInput >= 0;
        bool localVelDirectionIsFwd = localVel.z >= 0;

        // use the max speed for the direction we are going--forward or reverse.
        float maxSpeed = accelDirectionIsFwd ? presetStats.TopSpeed : presetStats.ReverseSpeed;
        float accelPower = accelDirectionIsFwd ? presetStats.Acceleration : presetStats.ReverseAcceleration;

        float accelRampT = Rigidbody.velocity.magnitude / maxSpeed;
        float multipliedAccelerationCurve = presetStats.AccelerationCurve * accelerationCurveCoeff;
        float accelRamp = Mathf.Lerp(multipliedAccelerationCurve, 1, accelRampT * accelRampT);

        bool isBraking = accelDirectionIsFwd != localVelDirectionIsFwd;

        // if we are braking (moving reverse to where we are going)
        // use the braking accleration instead
        float finalAccelPower = isBraking ? presetStats.Braking : accelPower;

        float finalAcceleration = finalAccelPower * accelRamp;

        // apply inputs to forward/backward
        float turningPower = turnInput * presetStats.Steer;

        Quaternion turnAngle = Quaternion.AngleAxis(turningPower, Rigidbody.transform.up);
        Vector3 fwd = turnAngle * Rigidbody.transform.forward;

        Vector3 movement = fwd * accelInput * finalAcceleration;

        // simple suspension allows us to thrust forward even when on bumpy terrain
        fwd.y = Mathf.Lerp(fwd.y, 0, presetStats.Suspension);

        // forward movement
        float currentSpeed = Rigidbody.velocity.magnitude;
        bool wasOverMaxSpeed = currentSpeed >= maxSpeed;

        // if over max speed, cannot accelerate faster.
        if (wasOverMaxSpeed && !isBraking) movement *= 0;

        Vector3 adjustedVelocity = Rigidbody.velocity + movement * Time.deltaTime;

        adjustedVelocity.y = Rigidbody.velocity.y;

        //  clamp max speed if we are on ground
        if (adjustedVelocity.magnitude > maxSpeed && !wasOverMaxSpeed)
        {
            adjustedVelocity = Vector3.ClampMagnitude(adjustedVelocity, maxSpeed);
        }

        // coasting is when we aren't touching accelerate
        bool isCoasting = Mathf.Abs(accelInput) < .01f;

        if (isCoasting)
        {
            Vector3 restVelocity = new Vector3(0, Rigidbody.velocity.y, 0);
            adjustedVelocity = Vector3.MoveTowards(adjustedVelocity, restVelocity, Time.deltaTime * presetStats.CoastingDrag);
        }

        Rigidbody.velocity = adjustedVelocity;

        ApplyAngularSuspension();

        // manual angular velocity coefficient
        float angularVelocitySteering = .4f;
        float angularVelocitySmoothSpeed = 20f;

        // turning is reversed if we're going in reverse and pressing reverse
        if (!localVelDirectionIsFwd && !accelDirectionIsFwd) angularVelocitySteering *= -1;
        var angularVel = Rigidbody.angularVelocity;

        // move the Y angular velocity towards our target
        angularVel.y = Mathf.MoveTowards(angularVel.y, turningPower * angularVelocitySteering, Time.deltaTime * angularVelocitySmoothSpeed);

        // apply the angular velocity
        Rigidbody.angularVelocity = angularVel;

        // rotate rigidbody's velocity as well to generate immediate velocity redirection
        // manual velocity steering coefficient
        float velocitySteering = 25f;
        // rotate our velocity based on current steer value
        Rigidbody.velocity = Quaternion.Euler(0f, turningPower * velocitySteering * Time.deltaTime, 0f) * Rigidbody.velocity;
    }

    void ApplyAngularSuspension()
        {
            // simple suspension dampens x and z angular velocity while on the ground
            Vector3 suspendedX = transform.right;
            Vector3 suspendedZ = transform.forward;
            suspendedX.y *= 0f;
            suspendedZ.y *= 0f;
            var sX = Vector3.Dot(Rigidbody.angularVelocity, suspendedX) * suspendedX;
            var sZ = Vector3.Dot(Rigidbody.angularVelocity, suspendedZ) * suspendedZ;
            var sXZ = sX + sZ;
            var sCoeff = 10f;

            Vector3 suspensionRotation;
            float minimumSuspension = 0.5f;
            if (presetStats.Suspension < minimumSuspension)
            {
                suspensionRotation = sXZ * presetStats.Suspension * sCoeff * Time.deltaTime;
            }
            else
            {
                suspensionRotation = sXZ * minimumSuspension * sCoeff * Time.deltaTime;
            }

            Vector3 suspendedAngular = Rigidbody.angularVelocity - suspensionRotation;

            // apply the adjusted angularvelocity
            Rigidbody.angularVelocity = suspendedAngular;
        }
    }

    void AnimateSuspension()
    {
        // simple suspension animation
        var suspensionTargetPos = suspensionNeutralPos;
        var suspensionTargetRot = suspensionNeutralRot;
        var bodyRot = transform.rotation.eulerAngles;

        var maxXTilt = presetStats.Suspension * 45;
        var closestNeutralRot = Mathf.Abs(360 - bodyRot.x) < Mathf.Abs(bodyRot.x) ? 360 : 0;
        var xTilt = Mathf.DeltaAngle(closestNeutralRot, bodyRot.x);

        var suspensionT = Mathf.InverseLerp(0, maxXTilt, xTilt);
        suspensionT = suspensionT * suspensionT;

        //Debug.Log("Suspension: " + suspensionT + " bodyRot: "  + bodyRot.x + " neutral: " + closestNeutralRot);
        bodyRot.x = Mathf.Lerp(closestNeutralRot, bodyRot.x, suspensionT);

        // transform bodyRot to suspension local space
        suspensionTargetRot = Quaternion.Inverse(SuspensionBody.transform.rotation) * Quaternion.Euler(bodyRot);

        // apply the new transforms
        SuspensionBody.transform.localPosition = Vector3.Lerp(SuspensionBody.transform.localPosition, suspensionTargetPos, Time.deltaTime * 5f);
        SuspensionBody.transform.localRotation = Quaternion.Slerp(SuspensionBody.transform.localRotation, suspensionTargetRot, Time.deltaTime * 5f);
    }

    public float LocalSpeed()
    {
        if (canMove)
        {
            float dot = Vector3.Dot(transform.forward, Rigidbody.velocity);
            if (Mathf.Abs(dot) > 0.1f)
            {
                float speed = Rigidbody.velocity.magnitude;
                return dot < 0 ? -(speed / presetStats.ReverseSpeed) : (speed / presetStats.TopSpeed);
            }
            return 0f;
        }
        else
        {
            // use this value to play kart sound when it is waiting the race start countdown.
            return InputVars.y;
        }
    }

    public void SetCanMove(bool move)
    {
        canMove = move;
    }

    public bool getCanMove()
    {
        return canMove;
    }

    IEnumerator waitForCountdown()
    {
        yield return new WaitForSeconds(2.5f);
        SetCanMove(true);
    }
}
