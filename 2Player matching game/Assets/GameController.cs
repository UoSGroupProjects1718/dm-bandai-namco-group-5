using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum Rules
{

}


public class GameController : MonoBehaviour
{
    //Text to show for players 1 and 2
    [Header("UI objects")]
    public Text text1;
    public Text text2;
    //Images for shape 1 and two, and symbol 1 and 2
    public Image shape1, shape2, symbol1, symbol2;

    [Header("Sprites")]
    //Sprites to be replaced on the UI
    public Sprite [] shapes;
    public Sprite []symbols;

    [Header("Symbol colours")]
    public Color[] colours;




    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Picks the next two symbols
    public void buttonNext()
    {

    }

    //Butto
    public void buttonSnap()
    {

    }




}
