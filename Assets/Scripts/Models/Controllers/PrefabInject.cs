using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PrefabInject
{
    private readonly IObjectResolver _objectResolver;

    public PrefabInject(IObjectResolver container)
    {
        _objectResolver = container;
    }

    public void InjectGameObject(GameObject go) => _objectResolver.InjectGameObject(go);
}