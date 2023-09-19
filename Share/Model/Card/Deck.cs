using System;
using System.Collections.Generic;
using System.Text;
using CardMatching.Pages;

namespace Share.Model.Card
{
    public class Deck
    {
        public List<Card> Cards { get; set; }
        private string[] CardFaces { get; set; }
        public Deck(string[] cardFace) 
        {
            Cards = new List<Card>();
            CardFaces = cardFace;
        }

        public void CreateDeck()
        {
            Cards.Clear();

            foreach (var card in CardFaces)
            {
                for (var i = 0; i < 2; i++)
                {
                    var id = Guid.NewGuid().ToString();
                    Cards.Add(new Card
                    {
                        Id = id,
                        Name = card
                    });
                }
            }

            //random items inside the list
            Cards.Shuffle();
        }
        public bool ChangeCardStatus(string? id, CardStatus status)
        {
            var card = Cards.FirstOrDefault(x => x.Id == id);

            if (card == null) return false;
            if (card.Status == status) return false;
            if (card.Status == CardStatus.Matched) return false;

            card.Status = status;

            return true;
        }

        public List<Card> GetOpenedCards() 
        {
            return Cards.Where(card => card.Status == CardStatus.Open).ToList();
        }

        public (bool isChecked, bool isMatched, List<Card> openedCards) CheckForMatchingCards()
        {
            var openedCards = GetOpenedCards();

            if (openedCards.Count < 2) return (false, false, openedCards);

            var isMatched = openedCards[0].Name == openedCards[1].Name;

            return (true, isMatched, openedCards);
        }
    }

    public static class Extensions
    {
        //Fisher-Yates Algorithm
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rng = new Random();

            int n = list.Count;

            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
