namespace GameHub.Games.TicTacToe2D
{ 
    /// <summary>
    /// Struct <c>Opponent</c> provides a list of opponent types.
    /// </summary>
    public struct Opponent
    {
        /// <summary>
        /// Expert AI game player.
        /// </summary>
        public static string AIExpert => "AI Expert";

        /// <summary>
        /// Beginner AI game player.
        /// </summary>
        public static string AIBeginner => "AI Beginner";

        /// <summary>
        /// Play against yourself and another human player.
        /// </summary>
        public static string MultiPlayer => "2 Player";
    }
}
