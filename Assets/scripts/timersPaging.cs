using UnityEngine;
using System.Collections;

public class timersPaging : MonoBehaviour {

//	public int min, sec;
//
//	float timer;
//	int minutes, seconds;
//
	public TextMesh timeText;
//	public bool resetTimer;


	void Start () {
//		timer = Time.time;
//		Debug.Log ("start "+ timer);
//
		timeText =  gameObject.GetComponent<TextMesh> ();
	}

	public void setOrder(int num)
	{
		timeText.text = num.ToString();
	}

	public void resetOrder()
	{
		timeText.text = "";
	}

	void FixedUpdate () {

	
//		if (!resetTimer) {
//			timer = Time.time;
//			Debug.Log ("upd:  " + timer);
//			minutes = (int)Mathf.Floor ((timer + (min * 60)) / 60) + (int)((timer + sec) / 60);
//			seconds = (int)((timer + sec) % 60);
//
//			Debug.Log (minutes + ":" + seconds);
//
//			timeText.text = (minutes) + ":" + (seconds);
//		} else {
//			min = sec = 0;
//			timeText.text = "0:0";
//		}
	}
}
