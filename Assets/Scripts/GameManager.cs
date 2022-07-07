using Assets.Scripts.Infrastructure.Abstractables;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager
    {
        public static GameManager Instance { get; }

        static GameManager()
        {
            Instance ??= new GameManager();
        }

        public void KillPlayer()
        {
            PuzzlePlayer.Instance.Kill();
        }
    }
}
