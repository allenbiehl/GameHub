using System.Collections.Generic;
using System.Linq;

namespace GameHub.Games.TicTacToe2D
{
    public class GameSeries
    {
        public List<GameState> Games { get; }

        public IPlayer Player1 { get; private set; }

        public IPlayer Player2 { get; private set; }

        public GameState CurrentGame
        {
            get
            {
                if (Games.Count > 0)
                {
                    return Games.Last();
                }
                else
                {
                    return null;
                }
            }
        }

        public GameSeries( IPlayer player1, IPlayer player2 )
        {
            Games = new List<GameState>();
            Player1 = player1;
            Player2 = player2;    
        }
    }
}
