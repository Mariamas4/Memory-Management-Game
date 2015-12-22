using UnityEngine;
using System.Collections;

public class EmptySpace : MonoBehaviour {

	public Transform Block, positionOfNewProcess;
	public int valueSpace;
	public TextMesh text;

	void Awake()
	{
		valueSpace = (int)(Block.localScale.y * 1000);

	}

	void Start () {
	
		text.text = valueSpace.ToString();

	}

	public void UpdateText()
	{
		valueSpace = (int)(Block.localScale.y * 1000);
		text.text = valueSpace.ToString();

	}


	void Update () {
	
	}


	void OnMouseDown ()
	{
		Debug.Log ("Screen: world: ");
	}

	void OnCollisionEnter(Collision col)
	{

	}
}
