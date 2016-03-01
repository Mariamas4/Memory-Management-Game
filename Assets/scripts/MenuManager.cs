using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	enum Menus{MAIN, SYSTEMSELECTIONMENU};
	static Menus currentMenu;
	RaycastHit hit;
	public GameObject mainMenu, settingsMenu, systemSelectionMenu, LoadingMenu;
	public GameObject InstAlloc, Paging;

	public Text togglefx, toggleMsic;
	public TextMesh togglefxmesh, toggleMusicmesh;

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

	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 200.0f)) {
				if(hit.collider.name == "TurnOn")
					TurnOn();
				else if(hit.collider.name == "sett")
					Settings();
				else if(hit.collider.name == "paging")
					loadScene(2);
				else if(hit.collider.name == "memo alloc")
					loadScene(1);					
				else if(hit.collider.name == "sfxOff")
					toggleSfx();
				else if(hit.collider.name == "musicOff")
					toggleMusic();
				else if(hit.collider.name == "back")
					backToMain();
				
			}
		}
	}

	public void backToMain()
	{
		mainMenu.SetActive (true);
		settingsMenu.SetActive (false);
		systemSelectionMenu.SetActive (false);
		LoadingMenu.SetActive (false);
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
		if(togglefxmesh.text == "sfx off") togglefxmesh.text = "sfx on";
		else if(togglefxmesh.text == "sfx on") togglefxmesh.text = "sfx off";

		
//		if(togglefx.text == "Turn off  sfx") togglefx.text = "Turn on  sfx";
//		else if(togglefx.text == "Turn on  sfx") togglefx.text = "Turn off  sfx";
	}
	public void toggleMusic()
	{
		if(toggleMusicmesh.text == "Music Off") toggleMusicmesh.text = "Music On";
		else if(toggleMusicmesh.text == "Music On") toggleMusicmesh.text = "Music Off";

//		if(toggleMsic.text == "Turn off music") toggleMsic.text = "Turn on music";
//		else if(toggleMsic.text == "Turn on music") toggleMsic.text = "Turn off music";
	}

	public void loadScene(int num)
	{
		StartCoroutine (loading(num));
	}

	IEnumerator loading(int num)
	{
//		if (num == 1)
//			InstAlloc.SetActive (true);
//		else if (num == 2)
//			Paging.SetActive (true);
		systemSelectionMenu.SetActive (false);
		LoadingMenu.SetActive (true);
		yield return new WaitForSeconds (4);
		
		Application.LoadLevel (num);
		yield return null;
	}
}
