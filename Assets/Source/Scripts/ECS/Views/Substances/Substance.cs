
using System;
using Source.EasyECS;
using Source.Scripts.EasyECS.Core;

namespace Source.Scripts.ECS.Views.Substances
{
    public partial class Substance : EcsComponent
    {
        public override void Initialize(int entity, Componenter componenter)
        {
            ref var type = ref componenter.AddOrGet<SubstanceData>(entity);
            type.Type = substanceType;
            
            switch (substanceType)
            {
                case Type.Aqua:
                    InitData<AquaSubstanceData>(entity, componenter);
                    break;
                case Type.Calendula:
                    InitData<CalendulaSubstanceData>(entity, componenter);
                    break;
                case Type.FishOil:
                    InitData<FishOilSubstanceData>(entity, componenter);
                    break;
                case Type.Hypericum:
                    InitData<HypericumSubstanceData>(entity, componenter);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}