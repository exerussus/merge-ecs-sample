﻿using Source.EasyECS.Interfaces;
using Source.Scripts.ECS.Systems;
using Source.SignalSystem;

namespace Source.Scripts.ECS.Views.Substances
{
    public struct SubstanceData : IEcsMark
    {
        public Substance.Type Type;
    }

    public struct OnSubstanceDestroyedSignal : ISignal
    {
        public Substance Substance;
    }
    
    public struct HypericumSubstanceData : IEcsData<int>, ISubstance
    {
        public int Amount;
        
        public void InitializeValues(int value)
        {
            Amount = value;
        }

        public Substance.Type SubstanceType => Substance.Type.Hypericum;
        public int SubstanceAmount { get => Amount; set => Amount = value; }
    }
    
    public struct FishOilSubstanceData : IEcsData<int>, ISubstance
    {
        public int Amount;
        
        public void InitializeValues(int value)
        {
            Amount = value;
        }

        public Substance.Type SubstanceType => Substance.Type.FishOil;
        public int SubstanceAmount { get => Amount; set => Amount = value; }
    }
    
    public struct CalendulaSubstanceData : IEcsData<int>, ISubstance
    {
        public int Amount;
        
        public void InitializeValues(int value)
        {
            Amount = value;
        }

        public Substance.Type SubstanceType => Substance.Type.Calendula;
        public int SubstanceAmount { get => Amount; set => Amount = value; }
    }
    
    public struct AquaSubstanceData : IEcsData<int>, ISubstance
    {
        public int Amount;
        
        public void InitializeValues(int value)
        {
            Amount = value;
        }

        public Substance.Type SubstanceType => Substance.Type.Aqua;
        public int SubstanceAmount { get => Amount; set => Amount = value; }
    }
}