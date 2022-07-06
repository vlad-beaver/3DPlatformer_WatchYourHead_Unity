using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Puzzle.Abstractes
{
    public abstract class PuzzleSystem : MonoBehaviour
    {
        [SerializeField]
        protected List<PuzzleInteractable> Interactables;

        [SerializeField]
        protected List<PuzzleTrap> Traps;

        [SerializeField]
        protected List<PuzzleExit> PuzzleExits;

        public event Action OnCompleted;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Traps.ForEach(_ => Gizmos.DrawLine(transform.position, _.transform.position));

            Gizmos.color = Color.green;
            PuzzleExits.ForEach(_ => Gizmos.DrawLine(transform.position, _.transform.position));

            Gizmos.color = Color.cyan;
            Interactables.ForEach(_ => Gizmos.DrawLine(transform.position, _.transform.position));
        }

        protected PuzzleSystem()
        {
            Interactables ??= new List<PuzzleInteractable>();
            Traps ??= new List<PuzzleTrap>();
            PuzzleExits ??= new List<PuzzleExit>();
        }

        protected virtual void Complete()
        {
            OnCompleted?.Invoke();
        }

        public abstract void Notify(PuzzleInteractable sender);
    }
}
