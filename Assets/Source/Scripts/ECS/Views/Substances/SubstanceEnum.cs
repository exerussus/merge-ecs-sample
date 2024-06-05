
using Source.Scripts.EasyECS.Core;
using UnityEngine;

namespace Source.Scripts.ECS.Views.Substances
{
    public partial class Substance : EcsComponent
    {
        [SerializeField] private Type substanceType;

        public Type SubstanceType => substanceType;

        public enum Type
        {
            Aqua,
            Calendula,
            FishOil,
            Hypericum,
        }
    }
}