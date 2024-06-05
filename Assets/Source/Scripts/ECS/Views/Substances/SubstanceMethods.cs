
using Source.EasyECS;
using Source.EasyECS.Interfaces;
using Source.Scripts.EasyECS.Core;
using Source.Scripts.ECS.Systems;
using UnityEngine;

namespace Source.Scripts.ECS.Views.Substances
{
    public partial class Substance : EcsComponent
    {
        [SerializeField] private int amount;
        [SerializeField, HideInInspector] private Rigidbody2D substanceRigidbody;

        public Rigidbody2D Rigidbody2D => substanceRigidbody;

        private void InitData<T>(int entity, Componenter componenter) 
            where T : struct, IEcsData<int>, ISubstance
        {
            ref var data = ref componenter.AddOrGet<T>(entity);
            data.SubstanceAmount += amount;
        }

        private void OnDisable()
        {
            Signal.RegistryRaise(new OnSubstanceDestroyedSignal {Substance = this});
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            if (substanceRigidbody == null) substanceRigidbody = GetComponent<Rigidbody2D>();
            if (amount < 1) amount = 1;
        }
    }
}