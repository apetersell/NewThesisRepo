using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;
using DG.Tweening;

public class RelationShipStatManager : MonoBehaviour 
{
	public float value;
	public Text namey;
	public CharacterSpriteController[] characters;
	public Color aigColor;
	public Color jpeColor;
	public Color leeColor;
	public Color kentColor;
	public Color defaultColor;
	public FriendMeterEffect fme;

	[YarnCommand("statUp")]
	public void increaseFriendship (string name)
	{
		GlobalManager globe = GameObject.Find ("GlobalStats").GetComponent<GlobalManager> ();
		if (name == "Lee") {
			globe.leeRelationship += value;
			GameObject.Find ("Lee").GetComponent<CharacterSpriteController> ().doFriendEffect (true);
			fme.startProcess (true, "Lee", globe.leeRelationship);
		} else {
			globe.jPeRelationship += value;
			GameObject.Find ("J-Pe").GetComponent<CharacterSpriteController> ().doFriendEffect (true);
			fme.startProcess (true, "J-Pe", globe.jPeRelationship);
		}
	}

	[YarnCommand("statDown")]
	public void decreaseFriendship (string name)
	{
		GlobalManager globe = GameObject.Find ("GlobalStats").GetComponent<GlobalManager> ();
		if (name == "Lee") {
			globe.leeRelationship -= value;
			GameObject.Find ("Lee").GetComponent<CharacterSpriteController> ().doFriendEffect (false);
			fme.startProcess (false, "Lee", globe.leeRelationship);
		} else {
			globe.jPeRelationship -= value;
			GameObject.Find ("J-Pe").GetComponent<CharacterSpriteController> ().doFriendEffect (false);
			fme.startProcess (false, "J-Pe", globe.jPeRelationship);
		}
	}

	[YarnCommand("name")]
	public void changeName (string name)
	{
		for (int i = 0; i < characters.Length; i++) 
		{
			characters [i].speaking = false;
		}
		if (name != "...") {
			namey.text = name;
			if (name == "Aig") {
				namey.color = aigColor;
			} else if (name == "J-Pe") {
				namey.color = jpeColor;
			} else if (name == "Lee") {
				namey.color = leeColor;
			} else if (name == "Kent") {
				namey.color = kentColor;
			} else {
				namey.color = defaultColor;
			}
			CharacterSpriteController csc = GameObject.Find (name).GetComponent<CharacterSpriteController> ();
			if (csc != null) {
				csc.speaking = true;
			}
		} else {
			namey.text = "";
		}
	}
}
