using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FriendMeterEffect : MonoBehaviour {

	public Image face;
	public Image meter;
	public Text label;
	public float fillAmount; 
	public Sprite[] jpFaces;
	public Sprite [] leeFaces;
	public float startingValue;
	public float endValue;
	bool happy;
	public Vector3 startPos;
	public float moveSpeed;
	public float numSpeed;
	public Transform onScreen;
	GlobalManager gm;
	RelationShipStatManager rsm;

	// Use this for initialization
	void Start () 
	{
		gm = (GlobalManager)FindObjectOfType(typeof(GlobalManager));
		rsm = (RelationShipStatManager)FindObjectOfType(typeof(RelationShipStatManager));
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		meter.fillAmount = fillAmount / 100;
	}

	public void startProcess (bool happy, string name, float value)
	{
		if (name == "J-Pe") 
		{
			label.text = "J-Pe Relationship";
			if (happy) {
				fillAmount = value - rsm.value;
				face.sprite = jpFaces [0];
			} else {
				fillAmount = value + rsm.value;
				face.sprite = jpFaces [1];
			}
		}
		if (name == "Lee") 
		{
			label.text = "Lee Relationship";
			if (happy) {
				fillAmount = value - rsm.value;
				face.sprite = leeFaces [0];
			} else {
				fillAmount = value + rsm.value;
				face.sprite = leeFaces [1];
			}
		}
		startingValue = fillAmount;
		transform.position = startPos;
		endValue = value;
		transform.DOMove (onScreen.position, moveSpeed).OnComplete(fill);
	}

	public void fill ()
	{ 
		DOTween.To(()=>fillAmount, x=> fillAmount = x, endValue, numSpeed).OnComplete(moveOff); 
	}

	public void moveOff()
	{
		transform.DOMove (startPos, moveSpeed);
	}
}
