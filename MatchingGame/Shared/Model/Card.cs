using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MatchingGame.Shared.Model
{
    public class Card
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public CardStatus Status { get; set; } = CardStatus.Close;

        public string? DisplayName()
        {
            var isShow = Status != CardStatus.Close;
            return isShow ? Name : "❔";
        }
    }

    public enum CardStatus
    {
        Open,
        Close,
        Matched
    }
}
