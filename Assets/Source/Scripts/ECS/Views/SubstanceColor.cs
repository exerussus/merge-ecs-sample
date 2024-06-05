using System;
using Sirenix.OdinInspector;
using Source.EasyECS;
using Source.Scripts.EasyECS.Core;
using Source.Scripts.ECS.Systems;
using Source.Scripts.ECS.Views.Substances;
using UnityEngine;

namespace Source.Scripts.ECS.Views
{
    public class SubstanceColor : EcsComponent
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField, ReadOnly] private Substance.Type type;
        [SerializeField, ReadOnly] private bool isContainer;
        [SerializeField, ReadOnly] private bool isSubstance;
        [SerializeField, ReadOnly] private bool isSubstanceHandler;
        private int Entity { get; set; }
        private Componenter Componenter { get; set; }
        
        public override void Initialize(int entity, Componenter componenter)
        {
            Entity = entity;
            Componenter = componenter;
            ChangeColorBySubstance(type);
            if (isContainer) Signal.Subscribe<OnMergeSignal>(OnSignal);
        }

        public override void Destroy(int entity, Componenter componenter)
        {
            if (isContainer) Signal.Unsubscribe<OnMergeSignal>(OnSignal);
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            
            if (TryGetComponent(out Container container))
            {
                type = container.SubstanceType;
                isContainer = true;
            }
            else isContainer = false;
            
            if (TryGetComponent(out Substance substance))
            {
                type = substance.SubstanceType;
                isSubstance = true;
            }
            else isSubstance = false;
            
            if (TryGetComponent(out SubstanceHandler substanceHandler))
            {
                type = substanceHandler.SubstanceType;
                isSubstanceHandler = true;
            }
            else isSubstanceHandler = false;
            
        }

        private void OnSignal(OnMergeSignal data)
        {
            if (data.ContainerEntity != Entity) return;

            SetCurrentColorByEntity();
        }

        private void SetCurrentColorByEntity()
        {
            if (Componenter.TryGetReadOnly(Entity, out ContainerData containerData))
            {
                ChangeColorBySubstance(containerData.SubstanceType);
            }
            else if (Componenter.TryGetReadOnly(Entity, out SubstanceData substanceData))
            {
                ChangeColorBySubstance(substanceData.Type);
            }
        }

        private void ChangeColorBySubstance(Substance.Type substanceType)
        {
            switch (substanceType)
            {
                case Substance.Type.Aqua:
                    spriteRenderer.color = Color.cyan;
                    break;
                case Substance.Type.Calendula:
                    spriteRenderer.color = Color.red;
                    break;
                case Substance.Type.FishOil:
                    spriteRenderer.color = Color.yellow;
                    break;
                case Substance.Type.Hypericum:
                    spriteRenderer.color = Color.green;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}