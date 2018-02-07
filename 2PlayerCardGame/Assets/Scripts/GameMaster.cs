using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;


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
enum Gamemodes
{
    Snap, //Two cards of the same number in a row
    SlapJacks, // Snap on Jack
    Runs, //Snap when there is a run of cards in either ascending or descending order
    Sandwich, //Snap when two cards of the same number are seperated by only one card
    KingSalutes, //Snap on King
    Suits //Snap when the two cards are the same suit
}

public class GameMaster : MonoBehaviour
{
    //Initialise a random number
    System.Random random = new System.Random();

    //Stores which player's turn it is
    bool player1Turn;

    //Current Game mode, set to 0 for now
    Gamemodes currentMode = 0;

    //Arrays to store card sprites
    public Sprite[] sprites;

    //Card views on the board
    public GameObject top, second, third;

    //Default Card back
    public Sprite cardBack;

    //
    public Text player1DeckSize, player2DeckSize;

    //Stacks for each player's deck and the deck on the table at the time
    public Stack<CardInfo> playerDeck1 = new Stack<CardInfo>();
    public Stack<CardInfo> playerDeck2 = new Stack<CardInfo>();
    public Stack<CardInfo> boardDeck = new Stack<CardInfo>();

    // Use this for initialization
    void Start ()
    {
        int cardSprite = 0;
        //Loop through and create all of the cards needed
        for (int suit = 0; suit < System.Enum.GetNames(typeof(Suit)).Length; suit++)
        {
            for (int num = 0; num < System.Enum.GetNames(typeof(Num)).Length; num++)
            {
                //Create a new card and set its values based off where we are in the loop
                CardInfo newCard = new CardInfo();
                newCard.suit = (Suit)suit;
                newCard.num = (Num)num;
                newCard.sprite = sprites[cardSprite];
                cardSprite++;

                //After assigning values, push the newly created card onto the stack
                boardDeck.Push(newCard);
            }
        }
        //Shuffle the newly created deck
        Shuffle(boardDeck);
        Deal(0);
	}

    //Function to shuffle cards on the board, using Fisher-Yates'
    //https://www.dotnetperls.com/fisher-yates-shuffle
    public void Shuffle(Stack<CardInfo> shuffleDeck)
    {
        CardInfo[] tempDeck = new CardInfo[shuffleDeck.Count()];
        //Convert Stack to array
        shuffleDeck.CopyTo(tempDeck, 0);
        


        int n = shuffleDeck.Count();
        for (int i = 0; i < n; i++)
        {
            int r = i + random.Next(n - i);
            CardInfo temp = tempDeck[r];
            tempDeck[r] = tempDeck[i];
            tempDeck[i] = temp;
        }
        boardDeck.Clear();
        foreach (var card in tempDeck)
        {
            boardDeck.Push(card);
        }
        

    }

    //Deals out the pack of cards to the specified player, 0 = both players, 1 = player 1 and 2 = player 2
    public void Deal(int receivingPlayers)
    {

        if (receivingPlayers == 0)
        {
            bool switcher = true;
            while (boardDeck.Count != 0)
            {
                if (switcher)
                {
                    playerDeck1.Push(boardDeck.Pop());
                }
                else
                {
                    playerDeck2.Push(boardDeck.Pop());
                }
                switcher = !switcher;
            }
        }
        if (receivingPlayers == 1)
        {
            //Create a temporary deck to house the players current cards
            Stack<CardInfo> tempDeck = playerDeck1;
            //clear the player's deck
            playerDeck1.Clear();
            //Put all of the cards on the board into the player deck
            boardDeck = playerDeck1;
            //Get rid of the board deck now we have dealt it out
            boardDeck.Clear();
            //Cycle through all of the cards left and put them back into the deck
            foreach (var card in tempDeck)
            {
                playerDeck1.Push(tempDeck.Pop());
            }
        }
        if (receivingPlayers == 2)
        {
            //Create a temporary deck to house the players current cards
            Stack<CardInfo> tempDeck = playerDeck2;
            //clear the player's deck
            playerDeck2.Clear();
            //Put all of the cards on the board into the player deck
            boardDeck = playerDeck2;
            //Get rid of the board deck now we have dealt it out
            boardDeck.Clear();
            //Cycle through all of the cards left and put them back into the deck
            foreach (var card in tempDeck)
            {
                playerDeck2.Push(tempDeck.Pop());
            }
        }
    }

    //Activated whenever a player pushes their deck button
    public void Button_PopCard(bool player1)
    {
        //If player 1 pops their card and it's their turn
        if (player1 && player1Turn)
        {
            //Remove the top object of player1's deck onto the board deck
            boardDeck.Push(playerDeck1.Pop());
            player1Turn = !player1Turn;
            UpdateCards();
        }
        //If player 2 pops their card and it's their turn
        else if (!player1 && !player1Turn)
        {
            boardDeck.Push(playerDeck2.Pop());
            player1Turn = !player1Turn;
            UpdateCards();
        }
        //If neither of these triggers then the player should not have pressed their button
        else
        {
            Debug.Log("Not that player's turn");
        }
    }
    
    //Activated whenever a player pushes their snap button
    public void Snap(bool player1)
    {
        CardInfo topCard, secondCard, thirdCard;

        //Top card is set to be the current top of the stack
        topCard = boardDeck.Peek();
        //Skip the first element of the stack and then look at the (now first) card to find out the second
        secondCard = boardDeck.Skip(1).First();
        thirdCard = boardDeck.Skip(2).First();


        switch (currentMode)
        {
            case Gamemodes.Snap:
                if (topCard.num == secondCard.num)
                {
                    ResolveSnap(player1);
                }
                break;
            case Gamemodes.SlapJacks:
                if (topCard.num == Num.Jack)
                {
                    ResolveSnap(player1);
                }
                break;
            case Gamemodes.Runs:
                if (topCard.num == secondCard.num -1 && topCard.num == thirdCard.num -2)
                {
                    ResolveSnap(player1);
                }
                if (topCard.num == secondCard.num + 1 && topCard.num == thirdCard.num + 2)
                {
                    ResolveSnap(player1);
                }
                break;
            case Gamemodes.Sandwich:
                break;
            case Gamemodes.KingSalutes:
                break;
            case Gamemodes.Suits:
                break;
            default:
                break;
        }
    }

    //Function that gets called when a player wins or loses a snap
    void ResolveSnap(bool player1)
    {
        if (player1)
        {
            Shuffle(playerDeck1);
        }
        else
        {
            Shuffle(playerDeck2);
        }
        
    }

    //Function to call when a player has run out of cards and has therefore lost
    void WinGame()
    {

    }

    //Update the card gameobjects shown on the board
    void UpdateCards()
    {
        if (boardDeck.Count() >= 1)
            top.GetComponent<Image>().sprite = boardDeck.Peek().sprite;
        else
            top.GetComponent<Image>().sprite = cardBack;

        if (boardDeck.Count() >= 2)
            second.GetComponent<Image>().sprite = boardDeck.Skip(1).First().sprite;
        else
            second.GetComponent<Image>().sprite = cardBack;

        if (boardDeck.Count() >= 3)
            third.GetComponent<Image>().sprite = boardDeck.Skip(2).First().sprite;
        else
            third.GetComponent<Image>().sprite = cardBack;

        player1DeckSize.text = playerDeck1.Count().ToString();
        player2DeckSize.text = playerDeck2.Count().ToString();
    }
}

//Class to hold all card info
public class CardInfo
{
    public Sprite sprite;
    public Suit suit;
    public Num num;
}