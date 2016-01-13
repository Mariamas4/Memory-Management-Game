using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public GameObject spawn, rightFreeSpace;
	public GameObject []EmptySpaces;
	public GameObject[]newProcesses;
	public GameObject [] arrangedEmptySpaces;
	enum GameType {MEMORYALLOCATION, PAGING};
	public enum AllocationType {FIRSTFIT, BESTFIT, WORSEFIT}
	public enum State {GENERATE, WAIT, ASSES, FINALSCORE};
	GameType gameType;
	public static AllocationType allocType;
	public State currentSate;
	bool gameOver, lastProcess;

	public GameObject WrongWord;

	public int numberOfNewProcesses, currentProcessNum;
	public GameObject currentProcessToAllocate, finalScore;

	public float Score = 100, time;
	public Text scoreText, final, timeText;
	public Dropdown drop;
	RaycastHit hit;

	void Start () {
		arrangedEmptySpaces = new GameObject[EmptySpaces.Length];
		for(int i =0; i < EmptySpaces.Length; ++i)
			arrangedEmptySpaces[i] = EmptySpaces[i];
		numberOfNewProcesses = newProcesses.Length;

		RandomizeNewProcesses ();
		drop.value = (int)allocType;
		time = Time.time;
		Debug.Log ("time "+time);

	}

	public void RandomizeNewProcesses()
	{
		for (int i = newProcesses.Length-1; i>0; --i)
		{
			int num = Random.Range(0, i);
			GameObject tempProcess = newProcesses[i];
			newProcesses[i] = newProcesses[num];
			newProcesses[num] = tempProcess;
		}
		Debug.Log (allocType);
	}

	void Update () {
	
		if (currentSate == State.WAIT) {
			if (Input.GetMouseButtonDown (0)) {
				if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100.0f)) {
					Debug.Log (hit.collider.name);

					if(hit.collider.tag == "FreeSpace"){

						if(hit.collider.gameObject == rightFreeSpace)
						{
//							Debug.Log("Same" + rightFreeSpace.transform.localScale.y);
							currentProcessToAllocate.transform.position = rightFreeSpace.GetComponent<EmptySpace> ().positionOfNewProcess.position;
							Vector3 newVector =  rightFreeSpace.transform.localScale;
							float newFreeSpaceamount = rightFreeSpace.transform.localScale.y - currentProcessToAllocate .transform.localScale.y;
							newVector.y = newFreeSpaceamount; 
							rightFreeSpace.transform.localScale = newVector;
							rightFreeSpace.GetComponent<EmptySpace>().UpdateText();
		
							ArrangeEmptySpaces();

							if (lastProcess){
								time = Time.time;
								currentSate = State.FINALSCORE;
							}else {
								currentSate = State.GENERATE;

							}

						}else if(hit.collider.gameObject != rightFreeSpace){
							Debug.Log("wrong space");
							WrongWord.SetActive(true);
//							currentProcessToAllocate = GameObject.Instantiate (WrongWord,
//							                                                   WrongWord.transform.position,
//							                                                   WrongWord.transform.rotation) as GameObject;
							Score -= 10;
							scoreText.text = "Score: " + Score.ToString()+"%";

							// Wrong get less score
						}
					}
				}
			}
		}

		switch (currentSate) {		
		case State.GENERATE:
			//Generating
			currentProcessToAllocate = GameObject.Instantiate (newProcesses [currentProcessNum],
						                       spawn.transform.position,
			                                   newProcesses [currentProcessNum].transform.rotation) as GameObject;


			currentProcessNum++;
			if (currentProcessNum == 4)
				lastProcess = true;
			Debug.Log(currentProcessToAllocate);
			Debug.Log(currentProcessToAllocate.GetComponent<EmptySpace> ().valueSpace);

			FirstFitAllocation ();
			
			currentSate = State.WAIT;
			break;
		case State.WAIT:
			
			break;
		case State.ASSES:
		

			break;
		case State.FINALSCORE:
			finalScore.SetActive(true);
			final.text = "Score: "+Score.ToString()+"%";

			timeText.text = (System.Math.Round(time, 2)).ToString() + "sec";
			break;
		}
	}

	public void changeAllocType(int type)
	{
		allocType = (AllocationType)type;
		if(allocType== AllocationType.BESTFIT || allocType == AllocationType.WORSEFIT)
			ArrangeEmptySpaces ();
		else if(allocType == AllocationType.FIRSTFIT)
			for(int i =0; i < EmptySpaces.Length; ++i)
				arrangedEmptySpaces[i] = EmptySpaces[i];

		Destroy (currentProcessToAllocate);
		currentSate = State.GENERATE;
		RandomizeNewProcesses ();
		currentProcessNum = 0;

		Application.LoadLevel (Application.loadedLevel);
//		finalScore.SetActive (false);
//		Score = 100;

	}

	public void ArrangeEmptySpaces()
	{
		GameObject temp;
		if (allocType == AllocationType.BESTFIT) {
			Debug.Log ("Hey U!");

			for (int i = 0; i < arrangedEmptySpaces.Length; i++) {
				for (int sort = 0; sort < arrangedEmptySpaces.Length - 1; sort++) {
					if (arrangedEmptySpaces[sort].GetComponent<EmptySpace>().valueSpace > arrangedEmptySpaces[sort + 1].GetComponent<EmptySpace>().valueSpace) {
						temp = arrangedEmptySpaces[sort + 1];
						arrangedEmptySpaces[sort + 1] = arrangedEmptySpaces[sort];
						arrangedEmptySpaces[sort] = temp;
					}
				}
			}

		} else if (allocType == AllocationType.WORSEFIT) {
			for (int i = 0; i < arrangedEmptySpaces.Length; i++) {
				for (int sort = 0; sort < arrangedEmptySpaces.Length - 1; sort++) {
					if (arrangedEmptySpaces[sort].GetComponent<EmptySpace>().valueSpace < arrangedEmptySpaces[sort + 1].GetComponent<EmptySpace>().valueSpace) {
						temp = arrangedEmptySpaces[sort + 1];
						arrangedEmptySpaces[sort + 1] = arrangedEmptySpaces[sort];
						arrangedEmptySpaces[sort] = temp;
					}
				}
			}
		}
	
		FirstFitAllocation ();
	}
	void FirstFitAllocation ()
	{
		for (int i = 0; i<arrangedEmptySpaces.Length; ++i) {
			if (currentProcessToAllocate.GetComponent<EmptySpace> ().valueSpace < arrangedEmptySpaces [i].GetComponent<EmptySpace> ().valueSpace) 
			{
				//	Handle moving
//				Debug.Log(currentProcessToAllocate.GetComponent<EmptySpace> ().valueSpace);
//				Debug.Log(arrangedEmptySpaces [i].GetComponent<EmptySpace> ().valueSpace);
				rightFreeSpace = arrangedEmptySpaces [i];
				Debug.Log("Choosen space: "+i);
				break;
			}
			Debug.Log ("No enough Space!!");

		}

	}

	public void replay()
	{
		Application.LoadLevel(Application.loadedLevel);
	}
	public void back()
	{
		Application.LoadLevel (0);
	}


}
