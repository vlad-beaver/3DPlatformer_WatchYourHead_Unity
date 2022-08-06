namespace Assets.Scripts.Infrastructure.Abstractables
{
    public abstract class PuzzlePlayer : PuzzleEntity
    {
        public static PuzzlePlayer Instance { get; protected set; }

        public ObservableValue<bool> HasHead;
        public bool HeadDead;

        protected PuzzlePlayer()
        {
            Instance = this;
            HasHead = new ObservableValue<bool>(false);
        }

        public abstract void Kill();
    }
}
