using System;
using System.Diagnostics;
using System.Collections.Generic; // Adding so that I can write with more different code

namespace First_project__bj_
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("You have started a game of Black Jack");
            Console.WriteLine("If you want info press 1");
            Console.WriteLine("If you want to start a game right now press 2");

            int StartOfGame = Convert.ToInt32(Console.ReadLine());

            if (StartOfGame == 1)
            {
                Process pWeb = new Process();
                pWeb.StartInfo.UseShellExecute = true;
                pWeb.StartInfo.FileName = "microsoft-edge:https://bicyclecards.com/how-to-play/blackjack/";
                pWeb.Start();
                // https://stackoverflow.com/questions/57057459/process-startmicrosoft-edge-throws-win32exception-in-dot-net-core
            }
            else if (StartOfGame == 2)
            {
                int PlayAgain;
                do
                {
                    RunGame();
                    Console.WriteLine("Do you want to play another round?\nIf yes press 1\nIf no press 2");
                    PlayAgain = Convert.ToInt32(Console.ReadLine());
                } while (PlayAgain == 1);
            }


            Console.ForegroundColor = ConsoleColor.Black;
        }
        static void RunGame()
        {
            int PlayerCardCount = 0;
            int DealerCardCount = 0;
         
            Deck Deck = new Deck();
            Hand DealerHand = new Hand();
            Hand PlayerHand = new Hand();

            Random rnd = new Random();
            
            //PlayersDeck.cards[2]

        
            //Dealing of two first cards
            for (int i = 0; i < 2; i++)
            {
                //Deal one card to player
                int rndCard = rnd.Next(0, 51);
                while(Deck.cards[rndCard] == null) 
                {
                    rndCard = rnd.Next(0, 51);
                }
                PlayerHand.cards[PlayerCardCount++] = Deck.cards[rndCard];
                Deck.cards[rndCard] = null;

                //Deal one card to dealer
                rndCard = rnd.Next(0, 51);
                while (Deck.cards[rndCard] == null)
                {
                    rndCard = rnd.Next(0, 51);
                }
                DealerHand.cards[DealerCardCount++] = Deck.cards[rndCard];
                Deck.cards[rndCard] = null;
            }

                //Dealers card shown
            Console.WriteLine(string.Format("Dealers card: {0}", 
                DealerHand.cards[0].GetCardName()));

            int DealerPoints = DealerHand.GetTotalPoints();
            Console.WriteLine(string.Format("Dealers points: {0}\n", DealerHand.GetTotalPoints()));
                //What cards you have value + color
            Console.WriteLine(string.Format("Your cards are: {0}, {1}", 
                PlayerHand.cards[0].GetCardName(), 
                PlayerHand.cards[1].GetCardName()));

                //The players cards points together
            Console.WriteLine(string.Format("Your points are: {0}\n", PlayerHand.GetTotalPoints()));

            Console.WriteLine("\n Hit or Stand?\n",
                "If you want another card type Hit\n",
                "If you want are happy with your cards type Stand");

            string Response = Console.ReadLine().ToLower();
            while (Response == "hit")
            {
                int rndCard = rnd.Next(0, 51);
                while (Deck.cards[rndCard] == null)
                {
                    rndCard = rnd.Next(0, 51);
                }
                PlayerHand.cards[PlayerCardCount++] = Deck.cards[rndCard];
                Deck.cards[rndCard] = null;

                string OutPut = "Your cards are: ";
                foreach (Card card in PlayerHand.cards)
                {
                    if (card == null)
                    {
                        break;
                    }
                    else
                    {
                        OutPut += card.GetCardName() + ", ";
                    }

                }

                Console.WriteLine(OutPut.Substring(0, OutPut.Length - 2));

                //The players cards points together
                Console.WriteLine(string.Format("Your points are: {0}\n", PlayerHand.GetTotalPoints()));

                Console.WriteLine("\nHit or Stand?\n");
                Response = Console.ReadLine().ToLower();
            }
            if (PlayerHand.GetTotalPoints() > 21)
            {
                Console.WriteLine("You lose ;)");
            }
            else 
            {
                while (DealerHand.GetTotalPoints() < 17)
                {
                    int rndCard = rnd.Next(0, 51);
                    while (Deck.cards[rndCard] == null)
                    {
                        rndCard = rnd.Next(0, 51);
                    }
                    DealerHand.cards[DealerCardCount++] = Deck.cards[rndCard];
                    Deck.cards[rndCard] = null; 
                }
                if (DealerHand.GetTotalPoints() > 21)
                {
                    Console.WriteLine("House is fat");
                }
                else 
                {
                    if (PlayerHand.GetTotalPoints() > DealerHand.GetTotalPoints())
                    {
                        Console.WriteLine("You win");
                    } 
                    else if (PlayerHand.GetTotalPoints() < DealerHand.GetTotalPoints())
                    {
                        Console.WriteLine("House wins");
                    }
                    else
                    {
                        Console.WriteLine("It's a draw");
                    }
                }
            }
        }
    }
    public class Card
    {
        public string color;
        public string value;
        public int points;
        public Card(string Color, string Value, int Points)
        {
            color = Color;
            value = Value;
            points = Points;
        }
        public string GetCardName()
        {
            return string.Format("{0} of {1}", value, color);
        }
    }
    public class Deck
    {                           // Making all the cards in a deck
        public Card[] cards = { 
            new Card("Hearts", "A", 11),
            new Card("Hearts", "2", 2),
            new Card("Hearts", "3", 3),
            new Card("Hearts", "4", 4),
            new Card("Hearts", "5", 5),
            new Card("Hearts", "6", 6),
            new Card("Hearts", "7", 7),
            new Card("Hearts", "8", 8),
            new Card("Hearts", "9", 9),
            new Card("Hearts", "10", 10),
            new Card("Hearts", "Jack", 10),
            new Card("Hearts", "Queen", 10),
            new Card("Hearts", "King", 10),

            new Card("Dimonds", "A", 11),
            new Card("Dimonds", "2", 2),
            new Card("Dimonds", "3", 3),
            new Card("Dimonds", "4", 4),
            new Card("Dimonds", "5", 5),
            new Card("Dimonds", "6", 6),
            new Card("Dimonds", "7", 7),
            new Card("Dimonds", "8", 8),
            new Card("Dimonds", "9", 9),
            new Card("Dimonds", "10", 10),
            new Card("Dimonds", "Jack", 10),
            new Card("Dimonds", "Queen", 10),
            new Card("Dimonds", "King", 10),

            new Card("Spades", "A", 11),
            new Card("Spades", "2", 2),
            new Card("Spades", "3", 3),
            new Card("Spades", "4", 4),
            new Card("Spades", "5", 5),
            new Card("Spades", "6", 6),
            new Card("Spades", "7", 7),
            new Card("Spades", "8", 8),
            new Card("Spades", "9", 9),
            new Card("Spades", "10", 10),
            new Card("Spades", "Jack", 10),
            new Card("Spades", "Queen", 10),
            new Card("Spades", "King", 10),

            new Card("Cloves", "A", 11),
            new Card("Cloves", "2", 2),
            new Card("Cloves", "3", 3),
            new Card("Cloves", "4", 4),
            new Card("Cloves", "5", 5),
            new Card("Cloves", "6", 6),
            new Card("Cloves", "7", 7),
            new Card("Cloves", "8", 8),
            new Card("Cloves", "9", 9),
            new Card("Cloves", "10", 10),
            new Card("Cloves", "Jack", 10),
            new Card("Cloves", "Queen", 10),
            new Card("Cloves", "King", 10),
        };
    }
    public class Hand
    {
        public Card[] cards = new Card [10];

        public int GetTotalPoints()
        {
            int TotalPoints = 0;
            foreach (Card card in cards)
            {
                if (card == null)
                {
                    break;
                }
                else
                {
                    TotalPoints += card.points;
                }
            }
           
            return TotalPoints;
        }
    }
}
