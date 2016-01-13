using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	enum Menus{MAIN, SYSTEMSELECTIONMENU};
	static Menus currentMenu;

	public GameObject mainMenu, settingsMenu, systemSelectionMenu, LoadingMenu;

	public Text togglefx, toggleMsic;

	public void Start () {
	
		if (currentMenu == Menus.MAIN) {
			mainMenu.SetActive (true);
			settingsMenu.SetActive (false);
			systemSelectionMenu.SetActive (false);
			LoadingMenu.SetActive (false);
		} else if (currentMenu == Menus.SYSTEMSELECTIONMENU) {
			mainMenu.SetActive (false);
			settingsMenu.SetActive (false);
			systemSelectionMenu.SetActive (true);
			LoadingMenu.SetActive (false);
		}
	}

	public void TurnOn()
	{
		//load 
		mainMenu.SetActive(false);
		systemSelectionMenu.SetActive (true);
		currentMenu = Menus.SYSTEMSELECTIONMENU;
	}

	public void Settings()
	{
		mainMenu.SetActive(false);
		settingsMenu.SetActive (true);
	}


	public void toggleSfx()
	{
		if(togglefx.text == "Turn off  sfx") togglefx.text = "Turn on  sfx";
		else if(togglefx.text == "Turn on  sfx") togglefx.text = "Turn off  sfx";
	}
	public void toggleMusic()
	{
		if(toggleMsic.text == "Turn off music") toggleMsic.text = "Turn on music";
		else if(toggleMsic.text == "Turn on music") toggleMsic.text = "Turn off music";
	}

	public void loadScene(int num)
	{
		StartCoroutine (loading(num));
	}

	IEnumerator loading(int num)
	{
		systemSelectionMenu.SetActive (false);
		LoadingMenu.SetActive (true);
		yield return new WaitForSeconds (2);
		
		Application.LoadLevel (num);
		yield return null;
	}
}
