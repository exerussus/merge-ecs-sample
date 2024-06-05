
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
        [SerializeField, ReadOnly] private Substance.Type substanceType;
        private Componenter _componenter;
        public int Entity => _entity;
        public Substance.Type SubstanceType => substanceType;

        public override void Initialize(int entity, Componenter componenter)
        {
            _entity = entity;
            _componenter = componenter;
            ref var data = ref componenter.Add<ContainerData>(_entity);
            data.InitializeValues(substanceType);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.TryGetComponent(out EcsMonoBehavior substance))
            {
                if (!_componenter.Has<SubstanceData>(substance.Entity)) return;
                if (_componenter.Has<OnDestroyData>(substance.Entity)) return;
                
                Signal.RegistryRaise(new CommandContainerAddingSignal {ContainerEntity = Entity, SubstanceEntity = substance.Entity});
            }
        }
    }

    public struct ContainerData : IEcsData<Substance.Type>
    {
        public Substance.Type SubstanceType;
        
        public void InitializeValues(Substance.Type value)
        {
            SubstanceType = value;
        }
    }
}