using System;
using UnityEngine;

namespace Assets.Scripts.Infrastructure.Abstractables
{
    public abstract class PuzzleComponent : MonoBehaviour
    {
        [SerializeField]
        private PuzzleActions _onComplete;

        protected PuzzleSystem PuzzleSystem;

        public bool IsActive { get; protected set; }

        public virtual void Register(PuzzleSystem system)
        {
            PuzzleSystem = system;

            switch (_onComplete)
            {
                case PuzzleActions.Activate:
                    PuzzleSystem!.OnCompleted += Activate;
                    break;
                case PuzzleActions.Deactivate:
                    PuzzleSystem!.OnCompleted += Deactivate;
                    break;
            }
        }

        public virtual void Unregister()
        {
            PuzzleSystem = null;

            switch (_onComplete)
            {
                case PuzzleActions.Activate:
                    PuzzleSystem!.OnCompleted -= Activate;
                    break;
                case PuzzleActions.Deactivate:
                    PuzzleSystem!.OnCompleted -= Deactivate;
                    break;
            }
        }

        /// <summary>
        /// Sets the value of <see cref="IsActive"/> to true.
        /// </summary>
        public virtual void Activate()
            => IsActive = true;

        /// <summary>
        /// Sets the value of <see cref="IsActive"/> to false.
        /// </summary>
        public virtual void Deactivate()
            => IsActive = false;
    }

    public enum PuzzleActions
    {
        Nothing,
        Activate,
        Deactivate
    }
}
