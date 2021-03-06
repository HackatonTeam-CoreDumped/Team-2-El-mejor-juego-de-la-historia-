﻿using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

	// The map we're building is going to look like:
	//
	//	LIST OF USERS -> A User -> LIST OF SCORES for that user
	//

	Dictionary< string, Dictionary<string, int> > playerScores;

	int changeCounter = 0;

	void Start() {
        int winner=PlayerPrefs.GetInt("Highscore_count", 0);
        for (int i = 0; i < winner; i++)
        {
            int play_num=Random.Range(0, 5437);
            SetScore("Player"+play_num, "assists", PlayerPrefs.GetInt("Highscore_" + i, 0));
            //SetScore(PlayerPrefs.GetString("User_" + i, "Player1"), "assists", PlayerPrefs.GetInt("Highscore_" + i, 0));
        }
	}

	void Init() {
		if(playerScores != null)
			return;

		playerScores = new Dictionary<string, Dictionary<string, int>>();
	}

	public void Reset() {
		changeCounter++;
		playerScores = null;
	}

	public int GetScore(string username, string scoreType) {
		Init ();

		if(playerScores.ContainsKey(username) == false) {
			// We have no score record at all for this username
			return 0;
		}

		if(playerScores[username].ContainsKey(scoreType) == false) {
			return 0;
		}

		return playerScores[username][scoreType];
	}

	public void SetScore(string username, string scoreType, int value) {
		Init ();

		changeCounter++;

		if(playerScores.ContainsKey(username) == false) {
			playerScores[username] = new Dictionary<string, int>();
		}

		playerScores[username][scoreType] = value;
	}

	public void ChangeScore(string username, string scoreType, int amount) {
		Init ();
		int currScore = GetScore(username, scoreType);
		SetScore(username, scoreType, currScore + amount);
	}

	public string[] GetPlayerNames() {
		Init ();
		return playerScores.Keys.ToArray();
	}
	
	public string[] GetPlayerNames(string sortingScoreType) {
		Init ();

		return playerScores.Keys.OrderByDescending( n => GetScore(n, sortingScoreType) ).ToArray();
	}

	public int GetChangeCounter() {
		return changeCounter;
	}

	public void DEBUG_ADD_KILL_TO_QUILL() {
		ChangeScore("quill18", "assists", 1);
	}
	
	public void DEBUG_INITIAL_SETUP() {
		SetScore("quill18", "assists", 345);
		
		
		SetScore("AAAAAA", "assists", 3);
		SetScore("BBBBBB", "assists", 2);
		SetScore("CCCCCC", "assists", 1);
        SetScore("DDDDDD", "assists", 1);


        Debug.Log (  GetScore("quill18", "assists") );
	}

    public void playAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
