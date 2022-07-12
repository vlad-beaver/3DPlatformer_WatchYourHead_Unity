using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Components;
using Assets.Scripts.Infrastructure.Abstractables;
using UnityEngine;

namespace Assets.Scripts.Puzzles
{
    public class AllCompletePuzzle : PuzzleSystem
    {
        [SerializeField]
        private int _goal;
        [SerializeField]
        private int _currentGoal;

        [SerializeField]
        private List<PuzzleComponent> _interactables;

        [SerializeField]
        private List<PuzzleComponent> _traps;

        [SerializeField]
        private List<PuzzleComponent> _exits;

        [SerializeField]
        private List<PuzzleComponent> _informationables;

        private void Start()
        {
            _interactables
                .Concat(_traps).Concat(_exits).Concat(_informationables)
                .AsParallel()
                .ForAll(_ => _.Register(this));
        }

        public override void Notify(PuzzleComponent sender, EventArgs args)
        {
            PuzzleEventArgs<bool> puzzleArgs = args as PuzzleEventArgs<bool>;

            if (puzzleArgs is { Value: true })
            {
                _currentGoal++;
            }

            if (puzzleArgs is { Value: false })
            {
                _currentGoal--;
            }

            for (int i = 0; i < _informationables.Count; i++)
            {
                if (i < _currentGoal)
                {
                    _informationables[i].Activate();
                }
                else
                {
                    _informationables[i].Deactivate();
                }
            }

            if (_currentGoal >= _goal)
            {
                TriggerCompleteEvent();
            }
        }

        protected override void TriggerCompleteEvent()
        {
            base.TriggerCompleteEvent();
            _exits.ForEach(_ => _.Activate());
            _traps.ForEach(_ => _.Deactivate());
        }
    }
}
