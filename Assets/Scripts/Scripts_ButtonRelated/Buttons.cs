using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

	public void Button1Logic()
	{
		print ("Vai pro menu carai");
	}

	public void Button2Logic()
	{
		SceneManager.LoadScene ("Wolfy alpha");
	}

	public void PlayGame()
	{
		SceneManager.LoadScene ("Wolfy alpha");
	}

	public void QuitGame()
	{
		Application.Quit ();
	}

	public void Menu()
	{
		SceneManager.LoadScene ("MenuPrincipalAlpha");
	}
}
