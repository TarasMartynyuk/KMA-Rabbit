namespace Actors
{
    /// <summary>
    /// encapsulates the lives mechanic of actors
    /// </summary>
    public class LivesComponent : ILivesComponent
    {
        public int Lives { get; protected set; }

        public LivesComponent(int lives)
        {
            _initialLives = lives;
            Lives = lives;
        }

        readonly int _initialLives;

        /// <summary>
        /// returns true if rabbit died due to inflicted damage
        /// </summary>
        public bool LoseLife()
        {
            Lives--;
            return Lives <= 0;
        }

        /// <summary>
        /// sets lives to inital amount
        /// </summary>
        public void ResetLives()
        {
            Lives = _initialLives;
        }
    }
}
