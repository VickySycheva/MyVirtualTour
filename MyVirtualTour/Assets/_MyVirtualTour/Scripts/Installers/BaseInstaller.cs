using System;
using UnityEngine;
using Zenject;

public class BaseInstaller : MonoInstaller
{
    protected void BindPrefabFactory<TPrefab, TFactory>(GameObject prefab) where TFactory : PlaceholderFactory<TPrefab>
    {
        if (prefab == null)
            throw new ArgumentException();

        Container.BindFactory<TPrefab, TFactory>()
            .FromComponentInNewPrefab(prefab)
            .WithGameObjectName(prefab.name);
    }
}
