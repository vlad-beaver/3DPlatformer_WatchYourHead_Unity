using Assets.Scripts.Infrastructure.Abstractables;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class PuzzleEmissionHandler : PuzzleComponent
    {
        private Material _material;

        private void Awake()
        {
            _material = GetComponent<Renderer>().material;
            _material.DisableKeyword("_EMISSION");
        }

        public override void Activate()
        {
            base.Activate();
            _material.EnableKeyword("_EMISSION");
        }

        public override void Deactivate()
        {
            base.Deactivate();
            _material.DisableKeyword("_EMISSION");
        }
    }
}
