using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Infrastructure.Abstractables
{
    public abstract class PuzzleSystem : MonoBehaviour
    {
        public event Action OnCompleted;

        public event Action OnInitialized;

        public event Action OnFailed;

#if UNITY_EDITOR
        private IEnumerable<PuzzleComponent> _components;

        private void OnDrawGizmosSelected()
        {
            //TODO: optimize
            IEnumerable<object> allFields = GetType()
                .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .AsParallel()
                .Select(_ => _.GetValue(this))
                .Where(_ => _ is PuzzleComponent or IEnumerable<PuzzleComponent>)
                .ToList();

            _components = allFields.OfType<PuzzleComponent>().ToList();

            allFields
                .AsParallel()
                .ForAll(_ =>
                {
                    if (_ is IEnumerable<PuzzleComponent> enumerable)
                    {
                        _components = _components.Concat(enumerable);
                    }
                });

            Gizmos.color = Color.red;
            List<PuzzleTrap> traps = _components.OfType<PuzzleTrap>().ToList();
            traps.ForEach(_ => Gizmos.DrawLine(transform.position, _.transform.position));

            Gizmos.color = Color.green;
            List<PuzzleExit> exits = _components.OfType<PuzzleExit>().ToList();
            exits.ForEach(_ => Gizmos.DrawLine(transform.position, _.transform.position));

            Gizmos.color = Color.cyan;
            List<PuzzleInteractable> interactables = _components.OfType<PuzzleInteractable>().ToList();
            interactables.ForEach(_ => Gizmos.DrawLine(transform.position, _.transform.position));

            Gizmos.color = Color.white;
            _components
                .Except(traps).Except(exits).Except(interactables)
                .ToList()
                .ForEach(_ => Gizmos.DrawLine(transform.position, _.transform.position));
        }
#endif

        protected virtual void Complete() => OnCompleted?.Invoke();

        protected virtual void Initialize() => OnInitialized?.Invoke();

        protected virtual void Fail() => OnFailed?.Invoke();

        public abstract void Notify(PuzzleInteractable sender, EventArgs args);
    }
}
