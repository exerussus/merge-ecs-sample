
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Source.Scripts.SignalSystem;
using UnityEngine;

namespace Source.Scripts.ECS.Views.Substances
{
    public class SubstanceHandler : MonoSignalListener
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private Vector3 spawnOffset;
        [SerializeField] private int count = 1;
        [SerializeField, ReadOnly] private Substance.Type substanceType;
        [SerializeField, ReadOnly] private List<Substance> substances;
        private float _timeDelay = 1f;
        private float _timer;
        
        private void Start()
        {
            for (int i = 0; i < count; i++)
            {
                var substance = Instantiate(prefab, position: transform.position + spawnOffset, rotation: Quaternion.identity).GetComponent<Substance>();
                substances.Add(substance);
            }
        }

        private void FixedUpdate()
        {
            _timer += Time.fixedDeltaTime;
            if (_timer > _timeDelay)
            {
                _timer -= _timeDelay;
                foreach (var substance in substances)
                {
                    if (!substance.gameObject.activeSelf)
                    {
                        substance.transform.position = transform.position;
                        substance.gameObject.SetActive(true);
                    }
                }
            }
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            if (count < 1) count = 1;
            if (!prefab.TryGetComponent(out Substance substance)) Debug.LogError($"Отсутствует Substance в префабе {prefab.name}.");
            else substanceType = substance.SubstanceType;
        }
    }
}