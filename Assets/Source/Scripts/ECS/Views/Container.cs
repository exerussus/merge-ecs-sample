
using Sirenix.OdinInspector;
using Source.EasyECS;
using Source.EasyECS.Interfaces;
using Source.Scripts.EasyECS.Core;
using Source.Scripts.ECS.Systems;
using Source.Scripts.ECS.Views.Substances;
using UnityEngine;

namespace Source.Scripts.ECS.Views
{
    public class Container : EcsComponent
    {
        [SerializeField, ReadOnly] private int _entity;
        private Componenter _componenter;
        public int Entity => _entity;
        
        public override void Initialize(int entity, Componenter componenter)
        {
            _entity = entity;
            _componenter = componenter;
            componenter.Add<ContainerMark>(_entity);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.TryGetComponent(out EcsMonoBehavior substance))
            {
                if (!_componenter.Has<SubstanceMark>(substance.Entity)) return;
                if (_componenter.Has<OnDestroyData>(substance.Entity)) return;
                
                Signal.RegistryRaise(new CommandContainerAddingSignal {ContainerEntity = Entity, SubstanceEntity = substance.Entity});
            }
        }
    }

    public struct ContainerMark : IEcsMark
    {
        
    }
}