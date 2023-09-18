using Microsoft.AspNetCore.Components;
using Share.Model.Card;

namespace CardMatching.Pages
{
    public partial class Index
    {
        static string[] CardFace = { "🔥", "🗿", "👁️", "💀", "😭", "🤨"};

        Deck _deck = new Deck(cardFace: CardFace);

        bool _isChecking = false;

        protected override void OnInitialized()
        {
            _deck.CreateDeck();
        }

        public async Task OnOpenCard(string? id)
        {
            if (_isChecking) return;

            var isSuccess = _deck.ChangeCardStatus(id, CardStatus.Open);

            if (isSuccess) 
            { 
                _isChecking = true;

                await CheckForMatchingItems();

                _isChecking = false;
            }
        }

        public async Task CheckForMatchingItems()
        {
            var result = _deck.CheckForMatchingCards();

            if (!result.isChecked) return;

            if (!result.isMatched)
            {
                await Task.Delay(1000);
            }

            foreach (var card in result.openedCards)
            {
                var status = result.isMatched ? CardStatus.Matched : CardStatus.Close;

                _deck.ChangeCardStatus(card.Id, status);
            }
        }

        public void Refresh()
        {
            _deck.CreateDeck();
        }
    }

    public enum GameStatus
    {
        Started,
        Paused,
        Finished
    }
}