using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum goals
{
    MATCHsymbols,
    MATCHshapes,
    MATCHcolour,
}

public class GameController : MonoBehaviour
{
    //player scores
    [Header("Player Scores")]
    public int player1Score = 0;
    public int player2Score = 0;
    //Text to show for players 1 and 2
    [Header("UI objects")]
    public Text text1;
    public Text text2;
    //Images for shape 1 and two, and symbol 1 and 2
    public GameObject shape1, shape2, symbol1, symbol2;

    [Header("Sprites")]
    //Sprites to be replaced on the UI
    public Sprite [] shapes;
    public Sprite [] symbols;
    public Sprite wildCard;

    [Header("Symbol colours")]
    public Color[] colours;

    goals currentGoals;

    System.Random randomNum = new System.Random();


    // Use this for initialization
    void Start ()
    {
        NewGoal();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Picks the next two symbols
    public void ButtonNext()
    {
        Shuffle();
    }

    //Button the player presses to announce a snap
    public void ButtonSnap(bool player1)
    {
        //If player 1 and was correct
        if (SnapCheck() && player1)
        {
            player1Score++;
        }
        //If player 2 and was correct
        if (SnapCheck() && !player1)
        {
            player2Score++;
        }
        //If player 1 and was incorrect
        if (!SnapCheck() && player1)
        {
            player1Score--;
        }
        //If player 2 and was incorrect
        if (!SnapCheck() && !player1)
        {
            player2Score--;
        }
    }

    //Shuffles the symbols on the board
    void Shuffle()
    {
        //Roll up a random number and then apply the sprite to it
        int tempRan = randomNum.Next(0, shapes.Length);
        shape1.GetComponent<Image>().sprite = shapes[tempRan];

        //Do that for the second shape
        tempRan = randomNum.Next(0, shapes.Length);
        shape2.GetComponent<Image>().sprite = shapes[tempRan];

        //Then repeat for the symbols
        tempRan = randomNum.Next(0, symbols.Length);
        symbol1.GetComponent<Image>().sprite = symbols[tempRan];
        tempRan = randomNum.Next(0, symbols.Length);
        symbol2.GetComponent<Image>().sprite = symbols[tempRan];

        //And the colours
        tempRan = randomNum.Next(0, symbols.Length);
        shape1.GetComponent<Image>().color = colours[tempRan];
        tempRan = randomNum.Next(0, symbols.Length);
        shape2.GetComponent<Image>().color = colours[tempRan];

    }

    bool SnapCheck()
    {
        //Check if there is a wildcard first, if their is then that player gets a point
        if (WildCardCheck())
        {
            return true;
        }
        switch (currentGoals)
        {
            case goals.MATCHsymbols:
                if (symbol1 == symbol2)
                    return true;
                else
                    return false;
            case goals.MATCHshapes:
                if (shape1 == shape2)
                    return true;
                else
                    return false;
            case goals.MATCHcolour:
                if (shape1.GetComponent<Image>().color == shape1.GetComponent<Image>().color)
                    return true;
                else
                    return false;
            default:
                return false;
        }
    }

    //Checks if either of the cards is a wildcard
    bool WildCardCheck()
    {
        if (symbol1 == wildCard || symbol2 == wildCard)
        {
            return true;
        }
        return false;
    }

    void NewGoal()
    {
        int tempRan = randomNum.Next(0, System.Enum.GetValues(typeof(goals)).Length);
        currentGoals = (goals)tempRan;

        switch (currentGoals)
        {
            case goals.MATCHsymbols:
                string tempText = "Match the symbols";
                text1.text = tempText;
                text2.text = tempText;
                break;
            case goals.MATCHshapes:
                tempText = "Match the shapes";
                text1.text = tempText;
                text2.text = tempText;
                break;
            case goals.MATCHcolour:
                tempText = "Match the colours";
                text1.text = tempText;
                text2.text = tempText;
                break;
            default:
                break;
        }
    }


}
