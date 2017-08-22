using UnityEngine;

/// <summary>
///     摄像机跟随脚本
/// </summary>
public class CameraFollow : MonoBehaviour
{
    public GameObject BlueBear;
    public GameObject RedBear;

    public float Distance = 10;
    public float Height = 5;
    public float HeightDamp = 2;
    public float RoationDamp = 3;

    private Transform _target;

    private void LateUpdate()
    {
        if (GameData.RedOrBlue == 0)
        {
            _target = RedBear.transform;
        }
        else if (GameData.RedOrBlue == 1)
        {
            _target = BlueBear.transform;
        }

        if (_target == null)
        {
            return;
        }

        var wantedRoationAngle = _target.eulerAngles.y;
        var wantedHeight = _target.position.y + Height;

        var currentRotationAngle = transform.eulerAngles.y;
        var currentHeight = transform.position.y;

        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRoationAngle, RoationDamp * Time.deltaTime);
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, HeightDamp * Time.deltaTime);

        var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        transform.position = _target.position;
        transform.position -= currentRotation * Vector3.forward * Distance;
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        transform.LookAt(_target);
    }
}