using System;

namespace Assets.Scripts.Infrastructure.Abstractables
{
    public abstract class PuzzleInteractable : PuzzleComponent
    {
        public override void Activate()
        {
            PuzzleSystem.Notify(this, new PuzzleEventArgs<int>());
        }

        public override void Deactivate()
        {
            PuzzleSystem.Notify(this, new PuzzleEventArgs<int>());
        }
    }
}
