using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PagingTile : MonoBehaviour {

	public enum whatAmI
	{
		emptySpace,
		realPage, 
		realPageInMemory,
		realPageInDisk
	};

	public PagingTile page;
	public timersPaging timer;
	public bool empty;
	public PagingTile pageHeld;
	public whatAmI I;

	public bool inM;
	public int addr;
	public TextMesh inMemo, address;

	void Awake()
	{
//		if(I == whatAmI.emptySpace )	
//			empty = true;
//		if (I == whatAmI.realPage) {
//
//			inMemo.text = "N";
//		}

	}
	 
	public void toMemoTrasferInfo(int addressSetting)
	{
		inM = true;
		addr = addressSetting;
		address.text = addr.ToString ();
		inMemo.text = "Y";

	}
	public void outMemoTrasferInfo()
	{
		inM = false;
//		addr = 0;
		address.text = "-";
		inMemo.text = "N";
		
	}


	public void toDiskTransferInfo()
	{
		inM = false;
//		addr = ;
		address.text = "-";
		inMemo.text = "N";
//		realPageInDisk

	}

	public void removePaginMan()
	{
		Destroy (GetComponent<PagingTile>());
	}
	void Start () {

	}
	
	void Update () {
	
//		if (inMemo.text == "N")
//			inM = false;
	}

	public void switchPageLocation(whatAmI me)
	{
		switch((int)me){
		case 1:
			tag = "PageOfJob";
				break;
		case 2:
			tag = "pageInMemo";
			inM = true;
			break;
		case 3:
			tag = "pageInDisk";
			break;
		}

	}

	void OnCollisionStay(Collision collision){
		if (collision.collider.tag == "pageInMemo")
			page = collision.collider.gameObject.GetComponent<PagingTile>();
	}

}
