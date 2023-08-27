using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainCameraOrthographicSizeNRotation : MonoBehaviour
{
	// Start is called before the first frame update
	public static MainCameraOrthographicSizeNRotation _inst;
	public Camera mainCameraObject;
	public  float currentCameraAngle;
	private Vector3 targetRotation;
	private float cameraDefaultOrthographicsSiz = 7f;
	private void Start()
	{
		if (_inst!=null && _inst!=this) { Destroy(_inst); } else { _inst = this; }
		InvokeRepeating(nameof(ChangeCameraOrthographicSizeandRotation), 10f, 10f);
	}
	private void ChangeCameraOrthographicSizeandRotation()
	{

		Debug.Log("change the camera rotation and the orthographicSize");
		targetRotation = new Vector3(0, 0, Random.Range(-45, 45));
		transform.DOLocalRotate(targetRotation, 2f, RotateMode.Fast).OnUpdate(UpdateMainCameraRotation).OnComplete(SetMainCameraToTargetRotation);
		



	}
	void UpdateMainCameraRotation()
	{
	currentCameraAngle=	transform.rotation.eulerAngles.z;

		// we need to use the system of angle which is 0(direction of 3:00 o'clock) 90 (direction of 12:00 o'clock) 180 (direction of 9:00 o'clock), 270 instead of -90 (direction of 6:00 o'clock) then back to the first direction
		float correctedAngle = Mathf.Repeat(180f + currentCameraAngle, 360f) - 180f;
		Debug.Log("correctedAngle = " + correctedAngle);
		currentCameraAngle= correctedAngle;
		mainCameraObject.orthographicSize = cameraDefaultOrthographicsSiz+Mathf.Abs(correctedAngle * 0.15f);
	}
	void SetMainCameraToTargetRotation()
	{
		mainCameraObject.orthographicSize = cameraDefaultOrthographicsSiz + Mathf.Abs(targetRotation.z * 0.15f);

	}	



}

