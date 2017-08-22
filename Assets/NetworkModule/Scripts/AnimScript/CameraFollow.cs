using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    public GameObject RedBear;
    public GameObject BlueBear;

    private Transform target;
    public float distance=10;
    public float height=5;
    public float heightDemp=2;
    public float roationDemp=3;
       
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
    void LateUpdate()
    {
        if (GameData.RedOrBlue == 0)
        {
            target = RedBear.transform;
        }
        if (GameData.RedOrBlue == 1)
        {
            target = BlueBear.transform;
        }
        if (target == null)
        {
            return;
        }
        float wantedRoationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;

        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle,wantedRoationAngle,roationDemp*Time.deltaTime);
        currentHeight = Mathf.Lerp(currentHeight,wantedHeight,heightDemp*Time.deltaTime);
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * distance;
        transform.position = new Vector3( transform.position.x,currentHeight,transform.position.z);

        transform.LookAt(target);
	}
}
