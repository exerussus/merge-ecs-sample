using Source.EasyECS;
using Source.EasyECS.Interfaces;
using Source.Scripts.EasyECS.Core;
using Source.Scripts.ECS.Systems;
using UnityEngine;

namespace Source.Scripts.ECS.Views.Substances
{
    public partial class Substance : EcsComponent, EcsComponent.IInitialize
    {
        [SerializeField] private int amount;
        
        private void InitData<T>(int entity, Componenter componenter) 
            where T : struct, IEcsData<int>, ISubstance
        {
            ref var data = ref componenter.AddOrGet<T>(entity);
            data.SubstanceAmount += amount;
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            if (amount < 1) amount = 1;
        }
    }
}