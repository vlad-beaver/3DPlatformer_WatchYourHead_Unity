using System;
using Assets.Scripts.Infrastructure.Abstractables;
using UnityEngine;

namespace Assets.Scripts.Puzzles
{
    public class HoldingPuzzle : PuzzleSystem
    {
        [SerializeField]
        private PuzzleComponent _platform;

        [SerializeField]
        private PuzzleComponent _button;

        private void Start()
        {
            _platform.Register(this);
            _button.Register(this);
        }

        public override void Notify(PuzzleComponent sender, EventArgs args)
        {
            if (!sender.Equals(_button))
            {
                return;
            }

            PuzzleEventArgs<bool> e = args as PuzzleEventArgs<bool>;
            if (e.Value)
            {
                _platform.Activate();
            }
            else
            {
                _platform.Deactivate();
            }
        }
    }
}
