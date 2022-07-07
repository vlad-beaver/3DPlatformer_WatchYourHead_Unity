using System;

namespace Assets.Scripts.Infrastructure.Abstractables
{
    public class PuzzleEventArgs<T> : EventArgs
    {
        public string Event { get; set; }

        public T Value { get; set; }
    }
}
