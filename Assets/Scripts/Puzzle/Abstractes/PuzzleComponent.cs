using UnityEngine;

namespace Assets.Scripts.Puzzle.Abstractes
{
    public abstract class PuzzleComponent : MonoBehaviour
    {
        protected Animator Animator;

        protected PuzzleSystem PuzzleSystem;

        private void Awake()
        {
            Animator ??= GetComponent<Animator>();
        }

        public virtual void Register(PuzzleSystem system)
        {
            PuzzleSystem = system;
        }

        public virtual void Activate()
        {
            Animator.SetTrigger("Activate");
        }

        public virtual void Deactivate()
        {
            Animator.SetTrigger("Deactivate");
        }
    }
}
