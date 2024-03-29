﻿using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;

namespace BlackJack
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //setup
            string playerChoice = "y", stickTwist = "";

            //create player and dealer objects
            Player p1 = new Player("Player 1", 0);
            Player Dealer = new Player("Dealer", 0);

            //loop to control gameplay
            while (playerChoice == "y")
            {        
                //game title
                Console.WriteLine("\n--- LETS PLAY BLACKJACK !!! ---");
                Console.WriteLine();

                //reset scores if game is on repeat
                p1.Score = 0;
                Dealer.Score = 0;
                
                //create new Deck object to use DealCard() method
                Deck Deck = new Deck();

                //player 1 plays
                Console.WriteLine("____ {0}'s Turn ____",p1.Name);

                //deal first 2 cards using Deck.DealCard() method
                for (int i = 0; i < 2; i++)
                    {
                        p1.Score += Deck.DealCard(p1.Score);
                    }

                    //display player 1 score
                    Console.WriteLine("Player 1 score : {0}", p1.Score);

                //ends game short if player 1 is bust
                if (p1.Score <= 21) { 

                    //ask to stick or twist
                    Console.Write("Do you want to stick or twist : s/t >> ");
                    stickTwist = Console.ReadLine();

                    if (stickTwist == "t") { 
                        while (stickTwist != "s")
                        {
                        //deal card
                            p1.Score += Deck.DealCard(p1.Score);
                         Console.WriteLine("Player 1 score : {0}", p1.Score);

                        //check if score is higher than 21 
                        if (p1.Score > 21)
                            break;
                        //ask again
                        Console.Write("\nDo you want to stick or twist : s/t >> ");
                            stickTwist = Console.ReadLine();
                        }
                    }
                }

                //dealer plays only if player 1 is still in the game
                if (p1.Score <= 21)
                {

                    Console.WriteLine("\n____ {0}'s Turn ____", Dealer.Name);

                    //deal first two cards
                    for (int i = 0; i < 2; i++)
                    {
                        Dealer.Score += Deck.DealCard(Dealer.Score);
                    }

                    Console.WriteLine("Dealers Score : {0}\n", Dealer.Score);

                    if (Dealer.Score < 17)
                        while (Dealer.Score < 17)
                        {
                            Dealer.Score += Deck.DealCard(Dealer.Score);
                            Console.WriteLine("Dealers Score : {0}", Dealer.Score);
                        }

                }

                //display player bust if 
                if (p1.Score > 21)
                    Console.WriteLine(p1.Name + " BUST!!");
                else if (Dealer.Score > 21)
                    Console.WriteLine(Dealer.Name + " BUST!!");

                //calculate and display winner
                GetWinner(p1.Score, Dealer.Score);

                Console.WriteLine("\n--- GAME OVER ---");
                Console.Write("Do you want to play again: y/n ? >> ");
                playerChoice = Console.ReadLine();

            } //end of gameplay loop

            Console.WriteLine("\n--- Have a Nice Day! ---");
            Console.ReadLine(); //pause

        }  //end of main method

        public static void GetWinner(int p1Score, int dealerScore)
        { 
            if (p1Score > dealerScore && p1Score <=21)
                Console.WriteLine("\nPlayer 1 Wins!!!"); 

            else if (dealerScore > p1Score && dealerScore <=21)
                Console.WriteLine("\nDealer Wins!!!");
            
            else if (p1Score == dealerScore && p1Score <= 21 && dealerScore <= 21 )
                Console.WriteLine("\nIts a draw!!!");
        }

    } //end of program class

} //end of namespace
