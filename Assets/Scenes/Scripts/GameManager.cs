using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager _inst;
    public float globalSpeed;
    public SpriteRenderer m1spriteRenderer;
    public SpriteRenderer m2spriteRenderer;
    public Color[] colors = new Color[5];
    private int colorIndex = 0;
	// Start is called before the first frame update

    private void Awake()
	{
        if (_inst!=null&&_inst!=this) {
            Destroy(this.gameObject);
        }
        else{ _inst = this; }
	}
	private void Start()
	{
        InvokeRepeating(nameof(ChangeSpriteColor), 5f, 30f);
	}
    private void ChangeSpriteColor() {
        colorIndex++;
        if(colorIndex==colors.Length) colorIndex = 0;
        m1spriteRenderer.DOColor( colors[colorIndex],0.7f);
        m2spriteRenderer.DOColor( colors[colorIndex],0.7f);
    }
	// Update is called once per frame
	void Update()
    {
       if(globalSpeed<3f) globalSpeed += Time.deltaTime*0.01f;
        
    }
   public void GameOver() {
        Invoke(nameof(RestartScene),3f);
    }
    void RestartScene() {
		SceneManager.LoadScene(0);
	}
}
