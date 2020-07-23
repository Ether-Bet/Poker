﻿using System.Collections.Generic;
using System.Linq;
using EtherBetClientLib.Models.Games.CardGameModels;
using EtherBetClientLib.Models.Games.Poker.PokerCombinations;

namespace EtherBetClientLib.Models.Games.Poker
{
    /// <summary>
    /// Class for storing essential variables and constraints
    /// </summary>
    public static class PokerGameData
    {
        #region CONSTANTS

        #region GuiConstants

        public static string[] CombinationNames = new string[]
        {
            "High Card",
            "Pair",
            "Two Pairs",
            "Three of a Kind",
            "Straight",
            "Flush",
            "Full House",
            "Four of a Kind",
            "Straight Flush",
            "Royal Flush"
        };

        #endregion

        /// <summary>
        /// Count of card ranks
        /// </summary>
        public const int RankCount = 13;

        /// <summary>
        /// Count of card suits
        /// </summary>
        public const int SuitCount = 4;

        /// <summary>
        /// 5 cards which are on table.
        /// </summary>
        public const int CardsOnTableCount = 5;

        /// <summary>
        /// 2 unique cards which each player has and only he can see them.
        /// </summary>
        public const int PlayerUniqueCardCount = 2;

        /// <summary>
        /// Full number of each player cards which can be included in combination
        /// </summary>
        public const int CardCount = CardsOnTableCount + PlayerUniqueCardCount;

        /// <summary>
        /// Number of poker cards in deck
        /// </summary>
        public const int AllCardCount = 52;

        /// <summary>
        /// List of all poker combinations types
        /// </summary>
        public static Combination[] CombTypes =
        {
            new RoyalFlush(null),
            new StraightFlush(null),
            new FourOfAKind(null),
            new FullHouse(null),
            new FLush(null),
            new Straight(null),
            new ThreeOfAKind(null),
            new TwoPairs(null),
            new Pair(null),
            new HighCard(null)
        };

        /// <summary>
        /// List of all cards
        /// will be initialized in main method
        /// </summary>
        public static Card[] CardDeck = Enumerable.Range(0, 52).Select(Card.FromNumber).ToArray();

        #endregion


        /// <summary>
        /// Determines if current 7 card array contains specified combination
        /// Used to preserve time. Using this dictionary avoids double checking for some Combinations,
        /// For example for "Flush".
        /// </summary>
        public static Dictionary<CombinationType, Card[]> Combinations = new Dictionary<CombinationType, Card[]>(10);

        /// <summary>
        /// Main database where frequency of getting each combination
        /// as highest combination is stored. frequency of combination
        /// is number how many times we got that combination as highest 
        /// combination after checking all possible card sets in concrete situation. 
        /// </summary>
        public static Dictionary<CombinationType, int> FrequencyTable = new Dictionary<CombinationType, int>(10);

        /// <summary>
        /// Count of all checked cases
        /// </summary>
        public static int CaseCount = 0;
    }
}