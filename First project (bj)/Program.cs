using System;
using System.Diagnostics;
using System.Collections.Generic; // Adding so that I can write with more different code

namespace First_project__bj_
{
    class Program
    {
        static void Main(string[] args) 
        {
            // what happens in the beginning
            Console.WriteLine("You have started a game of Black Jack\n");
            Console.WriteLine("If you want info and rules write 1 and press enter\n");
            Console.WriteLine("If you want to start a game right now write 2 and press enter\n\n");

            // Giving the player a choice and different output depending on the choice
            int StartOfGame = Convert.ToInt32(Console.ReadLine());

            if (StartOfGame == 1) // it takes you to a webbsite where you find the rules
            {
                Process pWeb = new Process();
                pWeb.StartInfo.UseShellExecute = true;
                pWeb.StartInfo.FileName = "microsoft-edge:https://bicyclecards.com/how-to-play/blackjack/";
                pWeb.Start();
                Main(null); //Jumps up to start so that you don't have to restart the game to play
                // https://stackoverflow.com/questions/57057459/process-startmicrosoft-edge-throws-win32exception-in-dot-net-core <- where I found the code to ba able to do this
            }
            else if (StartOfGame == 2) // starts the game by calling the method RunGame
            {
                int PlayAgain;
                do // a loop so that you can restart the game
                {
                    RunGame();
                    Console.WriteLine("Do you want to play another round?\nIf yes press 1\nIf no press 2\n\n"); // shows up when you win or lose
                    PlayAgain = Convert.ToInt32(Console.ReadLine()); 
                } while (PlayAgain == 1); // as long as the player puts in 1 at this question the game go again
            }

            Console.ForegroundColor = ConsoleColor.Black;
        }
        static void RunGame() // method that has the game 
        {
            int PlayerCardCount = 0; // the int that will hold the value of the cards in total for player
            int DealerCardCount = 0; // the int that will hold the value of the cards in total for dealer

            // creates a new deck, hand for player and dealer
            Deck Deck = new Deck();
            Hand DealerHand = new Hand();
            Hand PlayerHand = new Hand();

            Random rnd = new Random();

        
            //Dealing of first card and the second
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
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(string.Format("Dealers card: {0}", 
                DealerHand.cards[0].GetCardName()));
            Console.ForegroundColor = ConsoleColor.Gray;

            //What cards you have value + color
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(string.Format("\nYour cards are: {0}, {1}", 
                PlayerHand.cards[0].GetCardName(), 
                PlayerHand.cards[1].GetCardName()));
            Console.ForegroundColor = ConsoleColor.Gray;

            //The players cards points together
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(string.Format("\nYour points are: {0}\n", PlayerHand.GetTotalPoints()));
            Console.ForegroundColor = ConsoleColor.Gray;

            // Gives player a choice to hit or stand
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n \nHit or Stand?\n",
                "If you want another card type Hit\n",
                "If you want are happy with your cards type Stand");
            Console.ForegroundColor = ConsoleColor.Gray;

            // When you answer hit you get another random card that isn't already used
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

                // create a string that contains all the cards in the players hand
                string OutPut = "\nYour cards are: ";
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

                //Write that string to console
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(OutPut.Substring(0, OutPut.Length - 2));
                Console.ForegroundColor = ConsoleColor.Gray;

                //The players cards points together
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(string.Format("\nYour points are: {0}\n", PlayerHand.GetTotalPoints()));
                Console.ForegroundColor = ConsoleColor.Gray;

                //if you get more than 21 you don't get a choiceand 
                if (PlayerHand.GetTotalPoints() > 21)
                {
                    break;
                }
                //if you didn't lose it gives you a choice again
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nHit or Stand?\n");
                Console.ForegroundColor = ConsoleColor.Gray;
                Response = Console.ReadLine().ToLower();
            }
            //tells you that you lose when you are over 21
            if (PlayerHand.GetTotalPoints() > 21)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nYou lose ;)");
                Console.ForegroundColor = ConsoleColor.Gray; 
            }
            else 
            {
                //this tells the console that it needs to add cards to dealers hand until the points are over 17
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

                //says which cards the dealer got when the game ends
                Console.ForegroundColor = ConsoleColor.Blue;
                string OutPut = "Dealers cards are: ";
                Console.ForegroundColor = ConsoleColor.Gray;
                foreach (Card card in DealerHand.cards)
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
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(string.Format("Dealer's points are: {0}", DealerHand.GetTotalPoints()));
                Console.ForegroundColor = ConsoleColor.Gray;
                //if the dealer gets over 21 you win
                if (DealerHand.GetTotalPoints() > 21)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Dealer lose! You win!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else 
                {
                    //Who wins or loses
                    if (PlayerHand.GetTotalPoints() > DealerHand.GetTotalPoints())
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("You win");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    } 
                    else if (PlayerHand.GetTotalPoints() < DealerHand.GetTotalPoints())
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("House wins! You lose!");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("It's a draw");
                        Console.ForegroundColor = ConsoleColor.Gray;
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
            new Card("Hearts", "Ace", 11),
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

            new Card("Diamonds", "Ace", 11),
            new Card("Diamonds", "2", 2),
            new Card("Diamonds", "3", 3),
            new Card("Diamonds", "4", 4),
            new Card("Diamonds", "5", 5),
            new Card("Diamonds", "6", 6),
            new Card("Diamonds", "7", 7),
            new Card("Diamonds", "8", 8),
            new Card("Diamonds", "9", 9),
            new Card("Diamonds", "10", 10),
            new Card("Diamonds", "Jack", 10),
            new Card("Diamonds", "Queen", 10),
            new Card("Diamonds", "King", 10),

            new Card("Spades", "Ace", 11),
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

            new Card("Cloves", "Ace", 11),
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

        public int GetTotalPoints() // how it is supose to count the points and change the ace value
        {
            int TotalPoints = 0;
            int AceIndex = -1;
            for (int i = 0; i < cards.Length; i++)
            {
                Card card = cards[i];
                if (card == null)
                {
                    break;
                }
                else
                {
                    TotalPoints += card.points;
                    if (card.points == 11 && TotalPoints > 21)
                    {
                        card.points = 1;
                        TotalPoints -= 10;
                    }
                    else if (card.points == 11)
                    {
                        AceIndex = i;
                    }
                    else if (TotalPoints > 21 && AceIndex != -1)
                    {
                        cards[AceIndex].points = 1;
                        TotalPoints -= 10;
                    }
                }
            }
           
            return TotalPoints;
        }
    }
}
