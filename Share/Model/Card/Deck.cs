using System;
using System.Collections.Generic;
using System.Text;

namespace Share.Model.Card
{
    public class Deck
    {
        public List<CardModel> Cards { get; set; }
        private string[] CardFaces { get; set; }
        public Deck(string[] cardFace) 
        {
            Cards = new List<CardModel>();
            CardFaces = cardFace;
        }

        public void CreateDeck()
        {
            Cards.Clear();

            Random _randomGenerator = new Random();

            foreach (var card in CardFaces)
            {
                for (var i = 0; i < 2; i++)
                {
                    var id = Guid.NewGuid().ToString();
                    Cards.Add(new CardModel
                    {
                        Id = id,
                        Name = card
                    });
                }
            }

            //random items inside the list
            Cards.Sort((x, y) => _randomGenerator.Next(-1, 2));
        }
        public bool ChangeCardStatus(string? id, CardStatus status)
        {
            var card = Cards.FirstOrDefault(x => x.Id == id);

            if (card == null) return false;

            if (card.Status == status) return false;

            card.Status = status;

            return true;
        }

        public List<CardModel> GetOpenedCards() 
        {
            return Cards.Where(card => card.Status == CardStatus.Open).ToList();
        }

        public (bool isChecked, bool isMatched, List<CardModel> openedCards) CheckForMatchingCards()
        {
            var openedCards = GetOpenedCards();

            if (openedCards.Count < 2) return (false, false, openedCards);

            var isMatched = openedCards[0].Name == openedCards[1].Name;

            return (true, isMatched, openedCards);
        }
    }
}
