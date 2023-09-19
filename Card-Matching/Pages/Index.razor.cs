using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Share.Model.Card;

namespace CardMatching.Pages
{
    public partial class Index
    {
        static string[] CardFace = { "🔥", "🗿", "👁️", "💀", "😭", "🤨"};

        Deck _deck = new Deck(cardFace: CardFace);

        bool _isChecking = false;

        GameStatus _gameState = GameStatus.Started;

		bool isPause;
		
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

            if (IsGameOver())
            {
                _gameState = GameStatus.Over;

                ToggleOverlay();
            }
        }

        public bool IsGameOver()
        {
            return !_deck.Cards.Any(card => card.Status != CardStatus.Matched);
        }

        public void Refresh()
        {
            _deck.CreateDeck();

            if (isPause)
            {
                ToggleOverlay();
            }
        }

        public void Pause()
        {
            var isGamePaused = _gameState == GameStatus.Paused;

			_gameState = isGamePaused ? GameStatus.Started : GameStatus.Paused;

            ToggleOverlay();

            StateHasChanged();
        }

        public void ToggleOverlay()
        {
            isPause = !isPause;
        }
    }

    public enum GameStatus
    {
        Started,
        Paused,
        Over
    }
}