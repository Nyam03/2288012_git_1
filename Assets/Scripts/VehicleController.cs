using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public float maxMotorTorque = 500f; // ������ �ִ� ���ӵ�
    public float maxSteeringAngle = 30f; // ������ �ִ� ���� ����

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
        rb.centerOfMass = new Vector3(0, -0.5f, 0); // ���� �߽��� ���� �Ʒ��� �̵�
    }

    void FixedUpdate()
    {
        // �Է� �ޱ�
        float motor = maxMotorTorque * Input.GetAxis("Vertical"); // ����/����
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal"); // �¿� ����

        // ���� ����
        frontLeftWheel.steerAngle = steering;
        frontRightWheel.steerAngle = steering;

        // ���� ����
        frontLeftWheel.motorTorque = motor;
        frontRightWheel.motorTorque = motor;

        // ���� �� ȸ��
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
