using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    //we can use attributes like header and range to make some arrangements and limits on public arributes
    [Header("feedBack")]
    // we need to manage the feedback from the player using audio effects
	public GameObject playerSprite;
	public AudioSource dash;
	public AudioSource die;
    // we actually make the player invisible when dead so we need to disable the dash and dire vfx and animation
    [Header("dieFeedBack")]
    public bool isAlive=true;
    public ParticleSystem diefx, dashfx;
    //! we need to make shake scale when the player jump and insure that the scale of it after the shake will get back to the original scale
    [Header("dotweenDashShakeScale")]
    public Vector3 baseScale;
    [Range(0f,10f)]
    public float dotWeenShakeScalePower, dotWeenShakeScaleDuration;
    // controle the camera shake feedback according to player dash or die
    [Header("camera shake feedback")]
    [Range(0f, 5f)]
    public float cameraShakeDuration;
    [Range(0f, 5f)]
    public float cameraShakePower;
	// the below line to create trigger to be checked whether the space button is active or not to preform its action
	// and it's by default -2f to enable the space button action after the scene has been loaded 
	private float lastPressedSpaceButton;
	// Start is called before the first frame update
	private float dashDirection;
	// we need to make the player unable to reach the roof by multiple jumping till the above the middle of the screen
	private float screenHight;
	private float objectHight;

    public  float linearGravityVelocityPower=5f;
    public float sideGravity = 1f;
	void Start()
    {
		rb = GetComponent<Rigidbody2D>();
        Debug.Log(rb.velocity);
        lastPressedSpaceButton = -2f;
        screenHight = Screen.height;
       // we can get the current object hight which the this script is attached to 
        objectHight = Camera.main.WorldToScreenPoint(transform.position).y;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive && Input.GetKeyDown(KeyCode.Space) && Time.time - lastPressedSpaceButton > 0.5f)
        {
            objectHight = Camera.main.WorldToScreenPoint(transform.position).y;
            if (objectHight > screenHight / 2f)
            {
                rb.gravityScale = 5f;
            }
            else
            {
                rb.gravityScale = 3f;
            }
            // get 0 or -1 or 1
            Jump();
        
           
   
        }

	}
	private void FixedUpdate()
	{
        LinearGravityVelocity();
	}
	void LinearGravityVelocity()
	{
		float cameraAngle = MainCameraOrthographicSizeNRotation._inst.currentCameraAngle;
		float power = Mathf.Abs(sideGravity * cameraAngle);
		if (power > 2.5f) power = 2.5f;
		if (power < 1f) power = 1f;
		if (linearGravityVelocityPower < 0.1f) linearGravityVelocityPower = 2.5f;
		if (cameraAngle > 8)
		{
            Debug.Log("cameraAngle>8");
			rb.velocity += new Vector2(1, 0) * Time.deltaTime * linearGravityVelocityPower * power;
		}
		else if (cameraAngle < -8)
		{
            Debug.Log("cameraAngle< -8");
			rb.velocity += new Vector2(-1, 0) * Time.deltaTime * linearGravityVelocityPower * power;
		}
	}
	void Jump()
    {
		dashDirection = Input.GetAxisRaw("Horizontal");
		lastPressedSpaceButton = Time.time;
		dash.Play();
		dashfx.Play();
		rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
		rb.AddTorque(dashDirection * -5f, ForceMode2D.Impulse);
		rb.AddForce(Vector2.right * dashDirection * 10f, ForceMode2D.Impulse);
        //change the scale of object when jumping (dashing)
        transform.DOShakeScale(dotWeenShakeScaleDuration,dotWeenShakeScalePower).OnComplete(ResetScale);
        // do camera shake
        MainCameraShake._inst.CameraShake(cameraShakeDuration,cameraShakePower);

	}
    void ResetScale()
    {

        transform.localScale=baseScale;

	}
	private void OnTriggerEnter2D(Collider2D collision)
	{

        if (!isAlive) return;
        // camera shake feedback when die
        MainCameraShake._inst.CameraShake(cameraShakeDuration*2f,cameraShakePower*2f);
        die.Play();
        diefx.Play();
		isAlive = false;
        GetComponent<Renderer>().enabled = false;
        //! we need to make the player unvisible but the nested objects like particle system effect 
        //! still visible so instead of setactive we used Renderer component of this object which this script is attached to
        GameManager._inst.GameOver();
	}
}
