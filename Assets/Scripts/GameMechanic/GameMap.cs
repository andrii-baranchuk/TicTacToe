using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.GameMechanic.Models;
using Assets.Scripts.GameMechanic.Extensions;

namespace Assets.Scripts.GameMechanic
{
    public class GameMap
    {
        public static bool IsMapFull(int[,] map)
        {
            var elements = map.Cast<Player>().ToList();
            return elements.Count(x => x == Player.None) == 0;
        }

        public static GameResult CalculateGameResult(int[,] map)
        {
            var mainDiagonal = map.GetMainDiagonal();
            Player winner = DetermineWinner(mainDiagonal.Elements);
            if (winner != Player.None)
                return new GameResult(false, winner, mainDiagonal.Indexes);

            var reverseDiagonal = map.GetReverseDiagonal();
            winner = DetermineWinner(reverseDiagonal.Elements);
            if (winner != Player.None)
                return new GameResult(false, winner, reverseDiagonal.Indexes);

            for (int index = 0; index < map.GetLength(0); index++)
            {
                var row =  map.GetRow(index);
                winner = DetermineWinner(row.Elements);
                if (winner != Player.None)
                    return new GameResult(false, winner, row.Indexes);

                var column = map.GetColumn(index);
                winner = DetermineWinner(column.Elements);
                if (winner != Player.None)
                    return new GameResult(false, winner, column.Indexes);
            }

            return new GameResult(true);
        }

        private static Player DetermineWinner(IEnumerable<int> elements)
        {
            var uniqueElements = elements.Distinct().ToList();
            if (uniqueElements.Any(x => x == (int) Player.None))
                return Player.None;

            if (uniqueElements.Count > 1)
                return Player.None;

            Player winner = (Player) uniqueElements.First();
            return winner;
        }
    }
}
