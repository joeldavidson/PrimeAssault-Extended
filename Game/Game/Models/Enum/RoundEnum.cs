
namespace Game.Models
{
    /// <summary>
    /// The Conditions a round can have
    /// </summary>
    public enum RoundEnum 
    { 
        Unknown = 0, 
        NextTurn = 1, 
        NewRound = 2, 
        GameOver = 3, 
    }
}