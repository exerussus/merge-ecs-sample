
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Sirenix.OdinInspector;
using Source.Scripts.Managers.ProjectSettings;
using Source.Scripts.SignalSystem;
using Source.SignalSystem;
using UnityEngine;

namespace Source.Scripts.Selectables
{
    [AddComponentMenu("Selectables/SelectableHandler")]
    public class SelectableHandler : MonoSignalListener<OnSelectableKeepSignal>
    {
        [SerializeField, ReadOnly] private bool isExist;
        [SerializeField, ReadOnly] private Selectable currentSelectable;
        [SerializeField, ReadOnly] private Camera mainCamera;
        private TweenerCore<Vector2, Vector2, VectorOptions> _tweenTask;

        private void Start()
        {
            mainCamera = Camera.main;
        }

        private void FixedUpdate()
        {
            if (!isExist) return;

            if (Input.GetMouseButton(0))
            {
                var position = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                position.z = currentSelectable.transform.position.z;
                Vector2 direction = (Vector2)position - currentSelectable.Rigidbody2D.position;
                float distance = direction.magnitude;
                direction.Normalize();
                float speed = Mathf.Lerp(0, Constants.Physics.MaxSpeed, distance / Constants.Physics.SlowdownDistance);
                currentSelectable.Rigidbody2D.velocity = direction * speed;
            }
            else ReleaseCurrentSelectable();
        }
        //_tweenTask = currentSelectable.Rigidbody2D.DOMove(position, 0.5f);
        
        protected override void OnSignal(OnSelectableKeepSignal data)
        {
            isExist = true;
            currentSelectable = data.Selectable;
            currentSelectable.Rigidbody2D.gravityScale = 0;
            currentSelectable.Rigidbody2D.velocity = Vector2.zero;
            currentSelectable.Rigidbody2D.angularVelocity = 0;
            _tweenTask?.Kill();
        }

        private void Release()
        {
            isExist = false;
            currentSelectable.Rigidbody2D.gravityScale = Constants.Physics.DefaultGravity;
            currentSelectable = null;
        }

        private void ReleaseCurrentSelectable()
        {
            if (isExist) Release();
        }
    }

    public struct OnSelectableKeepSignal : ISignal
    {
        public Selectable Selectable;
    }
}