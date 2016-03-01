using UnityEngine;
using System.Collections;

public class PagingProcess : MonoBehaviour {


	public GameObject [] pagesInOrder;

	// Use this for initialization
	IEnumerator StartPaging () {

	
		// put a text saying that the paging process is starting

		yield return new WaitForSeconds (3);


//		Debug.Log ("Starting 2 ");

		yield return new WaitForSeconds (5);
//		Debug.Log ("Starting 3 ");
	
	}


	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.Space)){
			StartCoroutine(StartPaging());
		}
	}
}
