namespace Assets.Scripts.Infrastructure.Abstractables
{
    public abstract class PuzzlePlayer : PuzzleEntity
    {
        public static PuzzlePlayer Instance { get; protected set; }

        protected PuzzlePlayer()
        {
            Instance = this;
        }

        public abstract void Kill();
    }
}
