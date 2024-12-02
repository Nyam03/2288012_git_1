using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public float maxMotorTorque = 500f; // 차량의 최대 가속도
    public float maxSteeringAngle = 30f; // 차량의 최대 조향 각도

    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    public Transform frontLeftTransform;
    public Transform frontRightTransform;
    public Transform rearLeftTransform;
    public Transform rearRightTransform;

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0); // 무게 중심을 차량 아래로 이동
    }

    void FixedUpdate()
    {
        // 입력 받기
        float motor = maxMotorTorque * Input.GetAxis("Vertical"); // 전진/후진
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal"); // 좌우 조향

        // 바퀴 조향
        frontLeftWheel.steerAngle = steering;
        frontRightWheel.steerAngle = steering;

        // 바퀴 구동
        frontLeftWheel.motorTorque = motor;
        frontRightWheel.motorTorque = motor;

        // 바퀴 모델 회전
        UpdateWheelPose(frontLeftWheel, frontLeftTransform);
        UpdateWheelPose(frontRightWheel, frontRightTransform);
        UpdateWheelPose(rearLeftWheel, rearLeftTransform);
        UpdateWheelPose(rearRightWheel, rearRightTransform);
    }

    void UpdateWheelPose(WheelCollider collider, Transform transform)
    {
        Vector3 position;
        Quaternion rotation;

        collider.GetWorldPose(out position, out rotation);
        transform.position = position;
        transform.rotation = rotation;
    }
}
