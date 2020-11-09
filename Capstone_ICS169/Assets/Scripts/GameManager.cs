using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UIManager _UIManager;

    public void OnPlayerDeath()
	{
        _UIManager.GameOver();
        //set some UI up to say "GAME OVER"
        //freeze time
        //UI should have some way to get back to main menu
        //record how many times player died STATS
	}
    // Start is called before the first frame update
    void Start()
    {
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_UIManager == null) Debug.LogError("Canvas not found");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
