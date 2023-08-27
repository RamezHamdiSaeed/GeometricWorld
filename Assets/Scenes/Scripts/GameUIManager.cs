using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;

public class GameUIManager : MonoBehaviour
{
	public TextMeshProUGUI scoreText, HighScoreText;
	private float score = 0;
	private void Start()
	{
		HighScoreText.text = "High Score :" + PlayerPrefs.GetFloat("HIScore",0).ToString("0");
	}
	private void Update()
	{
		score += Time.deltaTime;
		scoreText.text ="Score : "+ score.ToString("0.00");
	}
	private void OnDestroy()
	{
		if (score > PlayerPrefs.GetFloat("HIScore"))
		{
			PlayerPrefs.SetFloat("HIScore", score);
		}
		
	}
}
