using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Suit
{
    Spade,
    Club,
    Diamond,
    Heart
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
    public Sprite[] sprites;

    public List<Card> CardPack = new List<Card>();
    
	// Use this for initialization
	void Start ()
    {
        for (int Suit = 0; Suit < 4; Suit++)
        {
            for (int Num = 0; Num < 13; Num++)
            {
                Card card = new Card(Suit, Num, Suit*);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    struct Card
    {
        public Sprite CardSprite;
        public Suit CardSuit;
        public Num CardNum;
        public void CardConstructor(Suit ConSuit, Num ConNum, Sprite ConSprite)
        {
            CardSuit = ConSuit;
            CardNum = ConNum;
            CardSprite = ConSprite;
        }
    }
}

