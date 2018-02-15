// SPLASH_SCREEN.CS
// NATALIE BAKER-HALL 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Splash_Screen : MonoBehaviour 
{
	// Variables
	// Images for the team logo
	public Image logo_image_0;
	public Image logo_image_1;
	public Image logo_image_2;
	public Image logo_image_3;

	// Images for the game title
	public Image game_title_0;
	public Image game_title_1;
	public Image game_title_2;
	public Image game_title_3;

	IEnumerator Start()
	{
		// Initialise alphas of the images to 0.0f
		logo_image_0.canvasRenderer.SetAlpha (0.0f);
		logo_image_1.canvasRenderer.SetAlpha (0.0f);
		logo_image_2.canvasRenderer.SetAlpha (0.0f);
		logo_image_3.canvasRenderer.SetAlpha (0.0f);

		game_title_0.canvasRenderer.SetAlpha (0.0f);
		game_title_1.canvasRenderer.SetAlpha (0.0f);
		game_title_2.canvasRenderer.SetAlpha (0.0f);
		game_title_3.canvasRenderer.SetAlpha (0.0f);

		// Fade the logo in and out
		Fade_In_Logo ();
		yield return new WaitForSeconds (2.5f);
		Fade_Out_Logo ();
		yield return new WaitForSeconds (2.5f);
		// Fade the title in and out
		Fade_In_Title ();
		yield return new WaitForSeconds (2.5f);
		Fade_Out_Title ();
		yield return new WaitForSeconds (2.5f);
		// Load the next scene
		SceneManager.LoadScene (1);

	}

	void Fade_In_Logo()
	{
		// Fade the alpha from 0% to 100%, over the duration of 1.5 seconds
		logo_image_0.CrossFadeAlpha (1.0f, 1.5f, false);
		logo_image_1.CrossFadeAlpha (1.0f, 1.5f, false);
		logo_image_2.CrossFadeAlpha (1.0f, 1.5f, false);
		logo_image_3.CrossFadeAlpha (1.0f, 1.5f, false);
	}

	void Fade_Out_Logo()
	{
		// Fade the alpha from 100% to 0%, over the duration of 1.5 seconds
		logo_image_0.CrossFadeAlpha (0.0f, 2.5f, false);
		logo_image_1.CrossFadeAlpha (0.0f, 2.5f, false);
		logo_image_2.CrossFadeAlpha (0.0f, 2.5f, false);
		logo_image_3.CrossFadeAlpha (0.0f, 2.5f, false);
	}

	void Fade_In_Title()
	{
		// Fade the alpha from 0% to 100%, over the duration of 1.5 seconds
		game_title_0.CrossFadeAlpha (1.0f, 0.5f, false);
		game_title_1.CrossFadeAlpha (1.0f, 0.5f, false);
		game_title_2.CrossFadeAlpha (1.0f, 0.5f, false);
		game_title_3.CrossFadeAlpha (1.0f, 0.5f, false);
	}

	void Fade_Out_Title()
	{
		// Fade the alpha from 100% to 0%, over the duration of 1.5 seconds
		game_title_0.CrossFadeAlpha (0.0f, 2.5f, false);
		game_title_1.CrossFadeAlpha (0.0f, 2.5f, false);
		game_title_2.CrossFadeAlpha (0.0f, 2.5f, false);
		game_title_3.CrossFadeAlpha (0.0f, 2.5f, false);
	}

}
