using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainCameraShake : MonoBehaviour
{
    // Start is called before the first frame update
    public static MainCameraShake _inst;
	private Vector3 cameraPos;
	private void Awake()
	{
		if (_inst!=null && _inst!=this) {
		Destroy( _inst );
		}
		_inst = this;
		cameraPos = transform.position;
	}
	public void CameraShake(float duration=0.3f,float power=1f)
    {
        transform.DOShakePosition(duration,power).OnComplete(ResetCameraPos);
    }
	private void ResetCameraPos()
	{
		transform.position = cameraPos;
	}
}
