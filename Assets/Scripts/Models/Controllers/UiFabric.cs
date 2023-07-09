using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Models.Controllers
{
    public class UiFabric
    {
        private readonly IObjectResolver _objectResolver;
        public UiFabric(IObjectResolver container)
        {
            _objectResolver = container;
        }

        public void InjectGameObject(GameObject go) => _objectResolver.InjectGameObject(go);
    }
}