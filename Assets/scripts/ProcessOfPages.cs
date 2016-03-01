using UnityEngine;
using System.Collections;

public class ProcessOfPages : MonoBehaviour {

	public GameObject [] pages;
	public int numOfPages;

	void Awake()
	{
		numOfPages = pages.Length;

	}

	void Start () {
	
		numOfPages = pages.Length;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
