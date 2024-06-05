
using Source.Scripts.EasyECS.Core;
using UnityEngine;

namespace Source.Scripts.ECS.Views.Substances
{
    public partial class Substance : EcsComponent, EcsComponent.IInitialize
    {
        [SerializeField] private Type substanceType;
        
        public enum Type
        {
            Aqua,
            Calendula,
            FishOil,
            Hypericum,
        }
    }
}