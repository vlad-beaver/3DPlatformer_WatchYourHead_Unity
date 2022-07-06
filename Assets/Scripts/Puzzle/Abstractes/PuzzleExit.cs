namespace Assets.Scripts.Puzzle.Abstractes
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
