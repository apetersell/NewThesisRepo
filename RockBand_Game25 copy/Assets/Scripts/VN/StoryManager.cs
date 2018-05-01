using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour {

	//Local reference to the stats. Updates every time we determine a new scene.
	public static float danceScore;
	public static float vocalScore ;
	public static float prScore;
	public static float stressLevel;
	public static float jPeRelationship;
	public static float leeRelationship;
	public static float songwriteScore;
	public static float modelScore;
	public static float tvScore;

	public static float statMeterFull = 1000f;
	public static float statHighThresh1 = 500f;
	public static float statLowThresh1 = 500;
	public static float fanMileStone1 = 5000;

	//Bools indicating whether a sideroute has been unlocked.
	public static bool friendshipUnlocked = false;
	public static bool songWritingUnlocked = false;
	public static bool TVUnlocked = false;
	public static bool modelingUnlocked = false;

	//String that indicates which friendship route we're in.
	public static string friendshipRoute = null;

	//Indicates when you can unlock side routes.
	public static float friendshipRouteDecideDay = 5;
	public static float careerRoutesCanBeUnlocked = 5;
	public static bool allCareersUnlocked; 

	//Separate Indexes keeping track of where the player is in each of the side routes.
	public static int friendshipTrackIndex = 0;
	public static int songWritingTrackIndex = 0;
	public static int TVTrackIndex = 0;
	public static int modelingTrackIndex = 0;



	//Keeps track of all the scenes we've already visited;
	public static List<string> scenesVisited = new List<string>();
	public static List<string> nodesVisited = new List<string> ();
	public static List <string> savedStates = new List <string> ();

	//Lists keeping track of all the point thresholds for side stories.
	public static List <float> friendshipThresholds = new List <float> ();
	public static List <float> kentFriendshipThresholds = new List<float> ();
	public static List <float> songWriteThresholds = new List <float>();
	public static List <float> modelThresholds = new List <float> (); 
	public static List <float> tvThresholds = new List <float>();  

	//Used to reset all static variables when the game is reset.
	public static void cleanHouse ()
	{
		danceScore = 0;
		vocalScore = 0;
		prScore = 0;
		stressLevel = 0;
		jPeRelationship = 0;
		leeRelationship = 0;
		songwriteScore = 0;
		modelScore = 0;
		tvScore = 0;
		scenesVisited.Clear ();
		nodesVisited.Clear ();
		savedStates.Clear ();
		friendshipTrackIndex = 0;
		songWritingTrackIndex = 0; 
		TVTrackIndex = 0;
		modelingTrackIndex = 0; 
		friendshipUnlocked = false;
		songWritingUnlocked = false;
		TVUnlocked = false;
		modelingUnlocked = false; 
		allCareersUnlocked = false;
	}


	//Returns true if the stat passed is higher than the other main stats. 
	public static bool isHighestStats(float score)
	{
		if(score >= danceScore
			&& score >= vocalScore
			&& score >= prScore)
		{
			return true;
		}

		return false;
	}

	public static bool isHighestCareerStat (float score)
	{
		if (score >= songwriteScore
		    && score >= modelScore
		    && score >= tvScore) 
		{
			return true;
		}

		return false;
	}

	//Returns true if all the stats are within a certain range of each other.
	public static bool isWellRounded (float averageRange)
	{
		float difDanceVocal = Mathf.Abs (danceScore - vocalScore); 
		float difDancePR = Mathf.Abs (danceScore - prScore);
		float difVocalPr = Mathf.Abs (vocalScore - prScore);
		if (difDanceVocal <= averageRange && difDancePR <= averageRange && difVocalPr <= averageRange) 
		{
			return true;
		}

		return false;
	}

	//Updates the local reference to stats;
	public static void updateStats (float dance, float vocal, float pr, float stress, float jPe, float lee, float songwrite, float model, float tv)
	{
		danceScore = dance;
		vocalScore = vocal;
		prScore = pr;
		stressLevel = stress;
		jPeRelationship = jPe;
		leeRelationship = lee;
		songwriteScore = songwrite;
		modelScore = model;
		tvScore = tv;
	}

	//Returns the VN scene to load.
	public static string determineScene(float dayIndex, float dance, float vocal, float pr, float stress, float jPe, float lee, float songwrite, float model, float tv)
	{
		updateStats (dance, vocal, pr, stress, jPe, lee, songwrite, model, tv);
		string result = null;

		//Priority 1: Unlock Friendship Route on Frienship Route Unlock Day.
		if (dayIndex == friendshipRouteDecideDay) 
		{
			unlockFriendshipRoute (); 
			result = determineSideStoryScene (friendshipRoute, friendshipTrackIndex);
			friendshipTrackIndex++;
		} 
		//Priority 2: Unlock Career Route if the Route isn't unlocked yet.
		else if (dayIndex >= careerRoutesCanBeUnlocked && !allCareersUnlocked) 
		{
			if (danceScore >= modelThresholds [0] && !modelingUnlocked) {
				modelingUnlocked = true;
				result = determineSideStoryScene ("Model", modelingTrackIndex); 
				modelingTrackIndex++; 
			} else if (vocalScore >= songWriteThresholds [0] && !songWritingUnlocked) {
				songWritingUnlocked = true;
				result = determineSideStoryScene ("SongWriting", songWritingTrackIndex);
				songWritingTrackIndex++;
			} else if (prScore >= tvThresholds [0] && !TVUnlocked) {
				TVUnlocked = true;
				result = determineSideStoryScene ("TV", TVTrackIndex); 
				TVTrackIndex++;  
			} else { 
				allCareersUnlocked = true; 
				result = determineDefaultScene (dayIndex);
			}
		} 
		//Priortiy 3: Play a Career Scene if the Career Scenes aren't exhausted.
		else if (qualifiedForCareerScene()) 
		{
			if (qualifiedForModel()) {
				result = determineSideStoryScene ("Model", modelingTrackIndex);
				modelingTrackIndex++;
			} else if (qualifiedForSongWriting ()) { 
				result = determineSideStoryScene ("SongWrite", songWritingTrackIndex);
				songWritingTrackIndex++;
			} else if (qualifiedForTV()) {
				result = determineSideStoryScene ("TV", TVTrackIndex);
				TVTrackIndex++;
			}
		}
		//Priority 4; Play a Friendship Scene
		else if (qualifiedForFriendshipScene())
		{
			result = determineSideStoryScene (friendshipRoute, friendshipTrackIndex);
			friendshipTrackIndex++;
		}
		//Priority 5: Play a default Scene
		else
		{
			result = determineDefaultScene (dayIndex); 
		}


		return result;
	}

	static string determineDefaultScene (float dayIndex)
	{
		string result = null;
		if (dayIndex == 1) {
			result = DayOne (); 
		} else if (dayIndex == 2) {
			result = DayTwo ();
		} else if (dayIndex == 3) {
			result = DayThree ();
		} else if (dayIndex == 4) {
			result = "Day4";
		} else if (dayIndex == 5) {
			result = "Day5";
		}
		return result;
	}


	//On days with multiple layers, finds what new node to navigate to.
	public static string findNewNode (string sourceText, int layer)
	{
		string result = "";

		if (sourceText == "Day3Rounded") 
		{
			if (danceScore + vocalScore + prScore < 600) {
				result = "Bad";
			} else {
				result = "Good";
			}
		}
		if (sourceText == "Day3PR") 
		{
			if (prScore < 500) {
				result = "Bad";
			} else {
				result = "Good";
			}
		}
		if (sourceText == "Day3Dance") 
		{
			if (danceScore < 500) 
			{
				result = "Bad";
			} 
			else 
			{
				result = "Good";
			}
		}
		if (sourceText == "Day3Vocal")
		{
			result = "Good";
		}

		if (sourceText == "Day4") 
		{
			float average = 100;
			if (layer == 3) 
			{
				if (danceScore + vocalScore + prScore <= statLowThresh1) {
					result = "LowStats";
				} else if (isWellRounded (average)) 
				{
					result = "AllRounder";
				}
				else 
				{
					if (isHighestStats (danceScore)) {
						result = "Dance";
					} else if (isHighestStats (vocalScore)) {
						result = "Vocals";
					} else {
						result = "Publicity";
					}
				}
			}
			if (layer == 2) 
			{
				result = "PreConcert";
			}
//			if (layer == 4) 
//			{
//				result = "PostConcert";
//			}
//			if (layer == 3) 
//			{
//				if (danceScore + vocalScore + prScore >= statHighThresh1) {
//					result = "GoodConcert";
//				} else if (danceScore + vocalScore + prScore < statLowThresh1) {
//					result = "BadConcert";
//				} else {
//					result = "OKConcert";
//				}
//			}
//			if (layer == 2) 
//			{
//				if (danceScore + vocalScore + prScore < statLowThresh1) {
//					result = "LowStats2";
//				} else if (isWellRounded (average)) 
//				{
//					result = "AllRounder2";
//				}
//				else 
//				{
//					if (isHighestStats (danceScore)) {
//						result = "Dance2";
//					} else if (isHighestStats (vocalScore)) {
//						result = "Vocals2";
//					} else {
//						result = "Publicity2";
//					}
//				}
//			}
		}

		nodesVisited.Add (result);
		return result;
		
	}

	//Logic for unlocking friendship route.
	static void unlockFriendshipRoute ()
	{
		float lowScore = 40;
		float highScore = 60;
		if (jPeRelationship <= lowScore && leeRelationship <= lowScore) {
			friendshipRoute = "Kent";
		} else if (jPeRelationship >= highScore && leeRelationship >= highScore) {
			friendshipRoute = "XIX"; 
		} else if (jPeRelationship > leeRelationship) {
			friendshipRoute = "JPe";
		} else if (leeRelationship > jPeRelationship) {
			friendshipRoute = "Lee"; 
		} else {
			friendshipRoute = "Kent"; 
		}
		Debug.Log ("FRIENDSHIP ROUTE: " + friendshipRoute);
	}

	//Logic for finding a sidestory scenes.
	static string determineSideStoryScene (string route, int scene)
	{
		string result = "SideStories/" + route + "Route" + scene; 
		return result;
	}

	static bool qualifiedForFriendshipScene ()
	{
		if (friendshipTrackIndex < friendshipThresholds.Count)
		{
			if (friendshipRoute == "JPe" && 
				jPeRelationship >= friendshipThresholds [friendshipTrackIndex]) 
			{ 
					return true;
			} 
			else if (friendshipRoute == "Lee" && 
				leeRelationship >= friendshipThresholds [friendshipTrackIndex]) 
			{ 
					return true;
			}
			else if (friendshipRoute == "Kent" 
				&& leeRelationship <= kentFriendshipThresholds [friendshipTrackIndex] &&
				    jPeRelationship <= kentFriendshipThresholds [friendshipTrackIndex]) 
			{

					return true;
			}
			else if (friendshipRoute == "XIX" && 
				leeRelationship >= friendshipThresholds [friendshipTrackIndex] &&
				    jPeRelationship >= friendshipThresholds [friendshipTrackIndex]) 
			{ 
					return true;
			}
			else 
			{
				return false;
			}
		}
		else 
		{
			return false;
		}
	}
				
	static bool qualifiedForCareerScene ()
	{
		if (qualifiedForModel () || qualifiedForSongWriting () || qualifiedForTV ()) {
			return true;
		} 

		return false;
	}

	static bool qualifiedForModel ()
	{
		if (modelingUnlocked &&
		    modelScore >= modelThresholds [modelingTrackIndex] &&
		    modelingTrackIndex < modelThresholds.Count) {
			return true;
		} 

		return false;
	}

	static bool qualifiedForSongWriting ()
	{
		if (songWritingUnlocked &&
			songwriteScore >= songWriteThresholds [songWritingTrackIndex] &&
			songWritingTrackIndex < songWriteThresholds.Count) {
			return true;
		} 

		return false;
	}

	static bool qualifiedForTV ()
	{
		if (TVUnlocked &&
			tvScore >= tvThresholds [TVTrackIndex] &&
			TVTrackIndex < tvThresholds.Count) {
			return true;
		}

		return false;
	}




	//String functions for each day.
		
	// PRE - DEBUT **************************************************************************************************************
	static string DayOne()
	{
		string result = "Day1ANew";
		return result;
	}

	static string DayTwo()
	{
		string result = "Day2ANew";

		return result;
	}

	static string DayThree() 
	{
		float average = 50;
		string result = "";

		if (isWellRounded (average)) {
			result = "Day3Rounded";
		} else {
			if (isHighestStats (prScore)) {
				result = "Day3PR";
			} else if (isHighestStats (danceScore)) {
				result = "Day3Dance";
			}
			else {
				result = "Day3Vocal";
			}
		}

		return result;
	}
}
