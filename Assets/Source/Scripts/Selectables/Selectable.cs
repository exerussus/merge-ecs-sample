
using Source.Scripts.SignalSystem;
using UnityEngine;

namespace Source.Scripts.Selectables
{
    [AddComponentMenu("Selectables/Selectable")]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Selectable : MonoSignalListener
    {
        [SerializeField, HideInInspector] private Collider2D selectableCollider2D;
        [SerializeField, HideInInspector] private Rigidbody2D selectableRigidbody2D;
        private OnSelectableKeepSignal _data;

        public Collider2D Collider2D => selectableCollider2D;
        public Rigidbody2D Rigidbody2D => selectableRigidbody2D;

        private void Start()
        {
            _data = new OnSelectableKeepSignal { Selectable = this };
        }

        private void OnMouseDown()
        {
            if (Input.GetMouseButtonDown(0)) Keep();
        }

        private void Keep()
        {
            Signal.RegistryRaise(_data);
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            if (selectableCollider2D == null) selectableCollider2D = GetComponent<Collider2D>();
            if (selectableRigidbody2D == null) selectableRigidbody2D = GetComponent<Rigidbody2D>();
        }
    }
}