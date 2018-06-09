namespace Actors
{
    /// <summary>
    /// for mocking
    /// </summary>
    public interface ILivesComponent
    {
        int Lives { get; }

        /// <summary>
        /// returns true if rabbit died due to inflicted damage
        /// </summary>
        bool LoseLife();

        /// <summary>
        /// sets lives to inital amount
        /// </summary>
        void ResetLives();
    }
}