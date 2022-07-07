namespace Assets.Scripts.Infrastructure.Abstractables
{
    public abstract class PuzzleTrap : PuzzleComponent
    {
        public override void Register(PuzzleSystem system)
        {
            base.Register(system);
            system.OnCompleted += Deactivate;
        }
    }
}
