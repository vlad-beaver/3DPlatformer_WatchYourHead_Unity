using Assets.Scripts.Infrastructure.Abstractables;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Components
{
    public class PuzzlePlayerDieHandler : PuzzleComponent
    {
        public override void Activate()
        {
            base.Activate();
            PuzzlePlayer.Instance.Kill();
        }
    }
}
