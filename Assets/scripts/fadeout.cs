using UnityEngine;
using System.Collections;

public class fadeout : MonoBehaviour {

	bool fade;
	Color _color;

	void OnEnable()
	{
		_color = gameObject.GetComponent<SpriteRenderer>().color;		
		fade = true;
	}

	void OnDisable()
	{
		fade = false;
		_color.a = 1;
		gameObject.GetComponent<SpriteRenderer>().color = _color;
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(fade){

			_color.a -= 0.01f;
			gameObject.GetComponent<SpriteRenderer>().color = _color;
		}

		if (_color.a < 0.05) {

			fade = false;
			gameObject.SetActive(false);
		}
	}
}
