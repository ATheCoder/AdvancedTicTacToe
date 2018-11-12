using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour {
    public TurnTimer turnTimer;
    Text timerText;
	// Use this for initialization
	void Start () {
        timerText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        timerText.text = Mathf.Ceil(turnTimer.turnTime - turnTimer.timer).ToString();
	}
}
