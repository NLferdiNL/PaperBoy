﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

using UnityEngine.Events;

using Extensions;

public class ScoreMenuHandlers : MonoBehaviour 
{
	private string HighscoreURL = "http://www.basegames.nl/highscores.pl";

	public Text EndScoreText;
	public Text DistanceScoreText;
	public Text PaperScoreText;

	public float StartDelay = 0.5F;
	public float TypeDelay = 0.01F;

    public Slider AudioSlider;

	private Animator Anim;

	public bool IsVisible = false;

	void Start()
	{
		Anim = GetComponent<Animator>();
        AudioSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("AudioVolume");
	}

	void Update()
	{
		DistanceScoreText.text = "Score: " + Mathf.RoundToInt(Global.Instance.DistanceScore);
		PaperScoreText.text = "Papers: " + Global.Instance.Dollars;
	}

	public void SubmitScore()
	{
		string Name = SystemInfo.deviceName;
		Debug.Log(Name + " Submited the score: " + Mathf.RoundToInt(Global.Instance.TotalScore) );
	}

	public void Home()
	{
        SceneManager.LoadScene("MainMenu");
	}
	public void Retry()
	{
        SceneManager.LoadScene("PlayScene");
	}

	public void ShowScoreMenu()
	{
		IsVisible = true;

		Anim.SetTrigger("StartScoreFadeIn");
		
		int CurrentTotalScore = Mathf.RoundToInt(Global.Instance.TotalScore);

		StartCoroutine(this.TypeIn("Score: " + Mathf.RoundToInt(Global.Instance.TotalScore), StartDelay, TypeDelay));

		if(CurrentTotalScore > PlayerPrefs.GetInt("Highscore", 0))
			PlayerPrefs.SetInt("Highscore", CurrentTotalScore);
	}
	public void ShowPauseMenu()
	{
		IsVisible = true;
		
		Anim.SetTrigger("StartPauseFadeIn");
	}
	public void HidePauseMenu()
	{
		IsVisible = false;
		
		Anim.SetTrigger("StartPauseFadeOut");
		
		Global.Instance.IsPlaying = true;
	}

    public void ShowOptionsMenu() {
        if (IsVisible) {
            Anim.SetTrigger("StartOptionsFadeIn");
        }
    }

    public void HideOptionsMenu() {
        Anim.SetTrigger("StartOptionsFadeOut");
    }

    public void OnAudioSliderChange() {
        PlayerPrefs.SetFloat("AudioVolume", AudioSlider.value); // Set the volume for the audio under the PlayerPrefs key 'AudioVolume'.
    }
}
