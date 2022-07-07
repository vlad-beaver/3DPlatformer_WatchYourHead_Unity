namespace Assets.Scripts.Infrastructure.Abstractables
{
    public abstract class PuzzleExit : PuzzleComponent
    {
        public override void Register(PuzzleSystem system)
        {
            base.Register(system);
            system.OnCompleted += Activate;
        }
    }
}
