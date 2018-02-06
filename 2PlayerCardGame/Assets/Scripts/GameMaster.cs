using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Suit
{
    Club,
    Spade,
    Heart,
    Diamond
}
public enum Num
{
    Ace,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King
}

public class GameMaster : MonoBehaviour
{
    //Stores which player's turn it is
    bool player1Turn;

    //Arrays to store card sprites
    public Sprite[] sprites;


    //Stacks for each player's deck and the deck on the table at the time
    public Stack<CardInfo> playerDeck1 = new Stack<CardInfo>();
    public Stack<CardInfo> playerDeck2 = new Stack<CardInfo>();
    public Stack<CardInfo> boardDeck = new Stack<CardInfo>();

    // Use this for initialization
    void Start ()
    {
        //Loop through and create all of the cards needed
        for (int suit = 0; suit < System.Enum.GetNames(typeof(Suit)).Length; suit++)
        {
            for (int num = 0; num < System.Enum.GetNames(typeof(Num)).Length; num++)
            {
                //Create a new card and set its values based off where we are in the loop
                CardInfo newCard = new CardInfo();
                newCard.suit = (Suit)suit;
                newCard.num = (Num)num;
                newCard.sprite = sprites[suit * num];

                //After assigning values, push the newly created card onto the stack
                boardDeck.Push(newCard);
            }
        }
	}
	
    //Function to shuffle cards on the board, using Fisher-Yates'
    public void Shuffle()
    {

    }

    //Deals out the pack of cards to the specified player, 0 = both players, 1 = player 1 and 2 = player 2
    public void Deal(int receivingPlayers)
    {

    }


	// Update is called once per frame
	void Update ()
    {
		
	}

    //Activated whenever a player pushes their deck button
    public void Button_PopCard(bool player1)
    {
        //If player 1 pops their card and it's their turn
        if (player1 && player1Turn)
        {
            //Remove the top object of player1's deck onto the board deck
            boardDeck.Push(playerDeck1.Pop());
        }
        //If player 2 pops their card and it's their turn
        else if (!player1 && !player1Turn)
        {
            boardDeck.Push(playerDeck2.Pop());
        }
        //If neither of these triggers then the player should not have pressed their button
        else
        {
            Debug.Log("Not that player's turn");
        }
    }


    //Activated whenever a player pushes their snap button
    void Snap(bool player1)
    {

    }
}

//Class to hold all card info
public class CardInfo : MonoBehaviour
{
    public Sprite sprite;
    public Suit suit;
    public Num num;
}