
using Source.EasyECS.Interfaces;
using Source.Scripts.ECS.Views.Substances;
using Source.Scripts.SignalSystem;
using Source.SignalSystem;
using UnityEngine;
using UnityEngine.Scripting;

namespace Source.Scripts.ECS.Systems
{
    [Preserve]
    public class MergeSystem : EcsSignalListener<CommandMergeSignal>
    {
        protected override void OnSignal(CommandMergeSignal data)
        {
            TryMergeAll(data.ContainerEntity);
            RegistrySignal(new OnMergeSignal { ContainerEntity = data.ContainerEntity});
        }
        
        /// <summary>
        /// Добавлять мержи тут.
        /// </summary>
        private void TryMergeAll(int mergeEntity)
        {
            if (TryMerge<AquaSubstanceData, CalendulaSubstanceData, HypericumSubstanceData>(mergeEntity)) return;
        }
        
        /// <summary>
        /// Метод для мержа двух компонентов
        /// </summary>
        /// <typeparam name="T1">Первая субстанция.</typeparam>
        /// <typeparam name="T2">Вторая субстанция.</typeparam>
        /// <typeparam name="T3">Полученная в результате субстанция.</typeparam>
        private bool TryMerge<T1, T2, T3>(int containerEntity)
        where T1 : struct, IEcsComponent, ISubstance
        where T2 : struct, IEcsComponent, ISubstance
        where T3 : struct, IEcsComponent, ISubstance
        {
            if (Componenter.Has<T1>(containerEntity) && Componenter.Has<T2>(containerEntity))
            {
                Componenter.Del<T1>(containerEntity);
                Componenter.Del<T2>(containerEntity);
                ref var newSubstance = ref Componenter.AddOrGet<T3>(containerEntity);
                newSubstance.SubstanceAmount = 1;
                return true;
            }

            return false;
        }
    }

    public struct CommandMergeSignal : ISignal
    {
        public int ContainerEntity;
    }

    public struct OnMergeSignal : ISignal
    {
        public int ContainerEntity;
    }
}