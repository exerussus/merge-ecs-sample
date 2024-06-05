using Source.EasyECS.Interfaces;
using Source.Scripts.ECS.Systems;

namespace Source.Scripts.ECS.Views.Substances
{
    public struct SubstanceMark : IEcsMark
    {
        
    }
    
    public struct HypericumSubstanceData : IEcsData<int>, ISubstance
    {
        public int Amount;
        
        public void InitializeValues(int value)
        {
            Amount = value;
        }

        public int SubstanceAmount { get => Amount; set => Amount = value; }
    }
    
    public struct FishOilSubstanceData : IEcsData<int>, ISubstance
    {
        public int Amount;
        
        public void InitializeValues(int value)
        {
            Amount = value;
        }

        public int SubstanceAmount { get => Amount; set => Amount = value; }
    }
    
    public struct CalendulaSubstanceData : IEcsData<int>, ISubstance
    {
        public int Amount;
        
        public void InitializeValues(int value)
        {
            Amount = value;
        }

        public int SubstanceAmount { get => Amount; set => Amount = value; }
    }
    
    public struct AquaSubstanceData : IEcsData<int>, ISubstance
    {
        public int Amount;
        
        public void InitializeValues(int value)
        {
            Amount = value;
        }

        public int SubstanceAmount { get => Amount; set => Amount = value; }
    }
}