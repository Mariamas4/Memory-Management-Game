using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PagingManager : MonoBehaviour
{
	public int orderInMemo;
	public GameObject[] EmptyPages;
	public GameObject spawn, finalScore, proccess, helpPanl, task;
	public GameObject[] newJobs; //new Process with several pages each
	public enum State
	{
		GENERATE,
		WAIT,
		ASSES,
		FINALSCORE
	}
	;
	public State currentSate;
	RaycastHit hit;
	public ProcessOfPages currentProcessToAllocate;
	public PagingTile SelectedPage, SelectedPage2, selectedEmptySpace;
	public float Score = 0, time = 60, timer, calculatedTime;
	int timeInt, min, sec;
	public Text scoreText, final, timeText, finalTime;
	bool gameOver, lastProcess;
	int currentJobNum = 0, currentProcessPagesNumber;
	float theTimeTaken; //= (int)Time.time;

	bool xTime, check;


	void Start ()
	{
		check = true;
		time = Time.time;
		Debug.Log ("time " + time);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (xTime && (currentSate == State.WAIT)) {
			calculatedTime = time - (Time.time - timer) ;

			min = (int)Mathf.Floor (calculatedTime / 60);
			sec = (int)Mathf.Floor (calculatedTime % 60);

			
			if(calculatedTime <= 0 && check) {
				xTime = false;
				currentSate = State.FINALSCORE;
				min = sec = 0;
				theTimeTaken = (int)Time.time;

			}
			timeText.text = min + ":" + sec;
		}
		if (Score == 100 && check) {
			currentSate = State.FINALSCORE;
			theTimeTaken = (int)Time.time;

		}

		if (currentSate == State.WAIT) {
			if (Input.GetMouseButtonDown (0)) {
				if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100.0f)) {
														
					if (hit.collider.tag == "PageOfJob") {
						Debug.Log("the tag: " + "PageOfJob ++  "+ hit.collider.GetComponent<PagingTile> ().inMemo.text);
						if ((hit.collider.GetComponent<PagingTile> ().inMemo.text == "N")) {
							Debug.Log (".........selected" + hit.collider.tag);
							SelectedPage = hit.collider.GetComponent<PagingTile> ();							
						}
					} else if (hit.collider.tag == "FreePage") {
						Debug.Log ("selected .." + hit.collider.tag);

						if (hit.collider.GetComponent<PagingTile> ().empty) {
							//page is empty
							Debug.Log (".........selected" + hit.collider.tag);

							if (SelectedPage != null) {
								SelectedPage.switchPageLocation (PagingTile.whatAmI.realPageInMemory);

//								Debug.Log(hit.collider.name);
								SelectedPage.toMemoTrasferInfo (int.Parse (hit.collider.name));
//								Debug.Log(SelectedPage.addr);

								Object x = GameObject.Instantiate (SelectedPage,	
								                                   hit.collider.transform.position,	
								                                   hit.collider.transform.rotation);
//								GameObject copy = x as GameObject;
								if(SelectedPage.gameObject.name == "pageJob7") {
									Score+=20;
									scoreText.text = Score.ToString () + "%";
								}

								hit.collider.GetComponent<PagingTile> ().empty = false;
								hit.collider.GetComponent<PagingTile> ().timer.setOrder(orderInMemo);
								orderInMemo++; 
								SelectedPage.switchPageLocation (PagingTile.whatAmI.realPage);
								SelectedPage = null;
							}
						}							
					} else if (hit.collider.tag == "pageInMemo") {
						Debug.Log ("selected" + hit.collider.tag);

						if(hit.collider.name == "pageJob4") {
							Score+=20;
							scoreText.text = Score.ToString () + "%";
						}else{
							Score-=5;
							scoreText.text = Score.ToString () + "%";
						}
						int addr = hit.collider.GetComponent<PagingTile> ().addr;
						Debug.Log(addr);
						EmptyPages[addr].GetComponent<PagingTile>().timer.resetOrder();
						EmptyPages[addr].GetComponent<PagingTile>().empty = true;
						hit.collider.GetComponent<PagingTile> ().outMemoTrasferInfo();

						Destroy(hit.collider.gameObject);
//						if (hit.collider.GetComponent<PagingTile> ().inM) {hit.collider
//							Debug.Log ("selected" + hit.collider.tag);
//							SelectedPage2 = hit.collider.GetComponent<PagingTile> ();							
//						}					
					}
//						else if (hit.collider.tag == "Disk") {
//
//						//page is empty
//						if (SelectedPage2 != null) {
//							Debug.Log ("selected: " + hit.collider.tag);
//
//							int i = hit.collider.GetComponent<Disk>().i;
//							SelectedPage2.transform.position = hit.collider.GetComponent<Disk>().spaces[i].transform.position;
//							SelectedPage.switchPageLocation (PagingTile.whatAmI.realPageInDisk);
//
//							//SelectedPage.transform.position = hit.collider.transform.position;
//							hit.collider.GetComponent<Disk>().i++;
//								
//							SelectedPage2.toMemoTrasferInfo (int.Parse (hit.collider.name));
//							hit.collider.GetComponent<PagingTile> ().empty = false;
//							hit.collider.GetComponent<PagingTile> ().timer.resetTimer = false;
//							SelectedPage2.switchPageLocation (PagingTile.whatAmI.realPage);
//							SelectedPage2 = null;
//						}
//					}
				}
			}
		}
	
		switch (currentSate) {	
		case State.GENERATE:
			currentSate = State.WAIT;
			break;
		case State.WAIT:
			
			break;
		case State.ASSES:
			
			
			break;
		case State.FINALSCORE:
			finalScore.SetActive (true);
			check = false;
			final.text = "Score: " + Score.ToString () + "%";
//			finalTime.text = (System.Math.Round (theTimeTaken, 2)).ToString () + "sec";
			break;
		}
	}

	public void replay ()
	{
		Application.LoadLevel (Application.loadedLevel);
	}

	public void back ()
	{
		Application.LoadLevel (0);
	}

	public void help(){
			if (helpPanl.activeSelf) {
				helpPanl.SetActive(false);
			}else{
				helpPanl.SetActive(true);
		}
	}
	public void showTask(){
		if (task.activeSelf) {
			task.SetActive(false);
			if(!xTime){ time = 60; xTime = true; timer = Time.time;}
		}else{
			task.SetActive(true);
		}
	}
}
