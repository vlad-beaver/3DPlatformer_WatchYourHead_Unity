using UnityEngine;

namespace Assets.Scripts.Infrastructure.Abstractables
{
    public abstract class PuzzleComponent : MonoBehaviour
    {
        protected PuzzleSystem PuzzleSystem;

        public virtual void Register(PuzzleSystem system)
        {
            PuzzleSystem = system;
        }

        public abstract void Activate();

        public abstract void Deactivate();
    }
}
