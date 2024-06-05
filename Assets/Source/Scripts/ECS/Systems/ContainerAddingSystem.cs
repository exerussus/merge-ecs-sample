
using Source.EasyECS.Interfaces;
using Source.Scripts.ECS.Views.Substances;
using Source.Scripts.SignalSystem;
using Source.SignalSystem;
using UnityEngine.Scripting;

namespace Source.Scripts.ECS.Systems
{
    [Preserve]
    public class ContainerAddingSystem : EcsSignalListener<CommandContainerAddingSignal>
    {
        protected override void OnSignal(CommandContainerAddingSignal data)
        {
            TryAddAllSubstances(data.ContainerEntity, data.SubstanceEntity);
            RegistrySignal(new CommandMergeSignal {ContainerEntity = data.ContainerEntity});
            RegistrySignal(new CommandKillEntitySignal {Entity = data.SubstanceEntity});
        }
        
        /// <summary>
        /// Добавлять новые субстанции тут.
        /// </summary>
        private void TryAddAllSubstances(int containerEntity, int substanceEntity)
        {
            TryAdd<AquaSubstanceData>(containerEntity, substanceEntity);
            TryAdd<HypericumSubstanceData>(containerEntity, substanceEntity);
            TryAdd<FishOilSubstanceData>(containerEntity, substanceEntity);
            TryAdd<CalendulaSubstanceData>(containerEntity, substanceEntity);
            RegistrySignal(new OnContainerAddingSignal {ContainerEntity = containerEntity, SubstanceEntity = substanceEntity});
        }

        private void TryAdd<T>(int containerEntity, int substanceEntity) where T : struct, IEcsComponent, ISubstance
        {
            if (Componenter.TryGetReadOnly(substanceEntity, out T substanceData))
            {
                ref var data = ref Componenter.AddOrGet<T>(containerEntity);
                data.SubstanceAmount += substanceData.SubstanceAmount;
            }
        }
    }
    
    public struct CommandContainerAddingSignal : ISignal
    {
        public int SubstanceEntity;
        public int ContainerEntity;
    }

    public struct OnContainerAddingSignal : ISignal
    {
        public int ContainerEntity;
        public int SubstanceEntity;
    }

    public interface ISubstance
    {
        public int SubstanceAmount { get; set; }
    }
}