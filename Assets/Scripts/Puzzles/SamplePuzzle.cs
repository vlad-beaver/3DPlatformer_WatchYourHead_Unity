using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Components;
using Assets.Scripts.Infrastructure.Abstractables;
using UnityEngine;

namespace Assets.Scripts.Puzzles
{
    public class SamplePuzzle : PuzzleSystem
    {
        [SerializeField]
        private int _goal = 1;
        private int _currentGoal = 0;

        [SerializeField]
        protected List<PuzzleInteractable> Interactables;

        [SerializeField]
        protected List<PuzzleExit> PuzzleExits;

        [SerializeField]
        protected List<PuzzleTrap> Traps;

        private void Awake()
        {
            Interactables.AsParallel().ForAll(_ => _.Register(this));
            PuzzleExits.AsParallel().ForAll(_ => _.Register(this));
            Traps.AsParallel().ForAll(_ => _.Register(this));
        }

        public override void Notify(PuzzleInteractable sender, EventArgs args)
        {
            if (sender is not SamplePlate)
            {
                return;
            }

            _currentGoal += ((PuzzleEventArgs<int>)args).Value;
            if (_currentGoal >= _goal)
            {
                Complete();
            }
        }
    }
}
