using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.GameMechanic.Models
{
    public class GameResult
    {
        public bool IsToe { get; }
        public Player Winner { get; }
        public List<(int Y, int X)> VictorySequence { get; }
        public string VictorySequenceDebugInfo => "(x,y) " + VictorySequence.Aggregate(" ", (current, i) => current + $"({i.Y},{i.X}) ");

        public GameResult(bool isToe, Player winner = Player.None, List<(int, int)> victorySequence = null)
        {
            IsToe = isToe;
            Winner = winner;
            VictorySequence = victorySequence ?? new List<(int, int)>();
        }
    }
}
