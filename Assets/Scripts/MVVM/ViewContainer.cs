using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.MVVM
{
    public abstract class ViewContainer : ScriptableObject, IViewContainer
    {
        [field: SerializeField]
        protected GameObject[] Prefabs { get; set; }

        private IReadOnlyDictionary<Type, GameObject> _mapping;

        private void OnEnable()
        {
            CreateViewPrefabMapping();
        }

        private void CreateViewPrefabMapping()
        {
            var allPrefabs = Prefabs;

            if (allPrefabs == null)
                return;

            _mapping = allPrefabs
                .ToDictionary(prefab => prefab.GetComponent<IView>().GetType(), prefab => prefab);
        }

        public GameObject GetView<TView>()
        {
            var view = typeof(TView);

            if (!typeof(IView).IsAssignableFrom(view))
                throw new ArgumentException($"{view.Name} is not an {nameof(IView)}");

            if (_mapping is null)
                throw new InvalidOperationException();

            return _mapping.GetValueOrDefault(view);
        }
    }
}