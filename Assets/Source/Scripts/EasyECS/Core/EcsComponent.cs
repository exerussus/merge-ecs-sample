using Source.Scripts.SignalSystem;
using UnityEngine;

namespace Source.Scripts.EasyECS.Core
{
    [RequireComponent(typeof(EcsMonoBehavior))]
    public class EcsComponent : MonoSignalListener
    {
        public interface IInitialize : IEcsComponentInitialize
        {

        }
    
        public interface IDestroy : IEcsComponentDestroy
        {

        }
    }
}