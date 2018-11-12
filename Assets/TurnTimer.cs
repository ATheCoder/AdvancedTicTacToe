using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTimer : MonoBehaviour {
    public float timer = 0f;
    public float turnTime = 15f;
    GameManager gameManager;
	// Use this for initialization
	void Start () {
        gameManager = GetComponent<GameManager>();
	}
    public void ResetTimer() {
        timer = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > turnTime) {
            timer = 0f;
            gameManager.allowedBoardNumber = -1;
            gameManager.choosePhase();
            gameManager.changePlayer();
        }
    }
}
