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

            Gizmos.color = Color.white;
            _components
                .ToList()
                .ForEach(_ => Gizmos.DrawLine(transform.position, _.transform.position));
        }
#endif

        protected virtual void TriggerCompleteEvent() => OnCompleted?.Invoke();

        public abstract void Notify(PuzzleComponent sender, EventArgs args);
    }
}
