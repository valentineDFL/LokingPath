using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.EntryPoint;
using Assets.Scripts.FinishTrigger;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.ShieldAbility
{
    internal class ShieldIconUi : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public event Action ShieldActivate; 
        public event Action ShieldDeactivate;

        [SerializeField] [Range(1, 5)] [Tooltip("Seconds")] private float _waitingTime;
        [SerializeField] [Header("EntryPoint")] private GameEntryPoint _entryGamePoint;

        private float _counter;

        private Image _image;

        private bool _pressed;

        private bool _waitingTimeDoned = true;

        private void OnEnable() => _entryGamePoint.OnLevelStarted += ResetUi;
        private void OnDisable() => _entryGamePoint.OnLevelStarted -= ResetUi;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _counter = _waitingTime;
        }

        public void OnPointerDown(PointerEventData poi)
        {
            if (_waitingTimeDoned)
            {
                ShieldActivate?.Invoke();
                _pressed = true;
            }
        }

        public void OnPointerUp(PointerEventData poi)
        {
            ShieldDeactivate?.Invoke();
            _pressed = false;
        }

        private void FixedUpdate()
        {
            FillShieldIcon();
        }

        private void FillShieldIcon()
        {
            float fillAmountValue = CalculateOneNormalizeValue.CalculateOneNormalize(_counter, 0, _waitingTime);

            if (_pressed)
            {
                _image.fillAmount = fillAmountValue;

                if (_counter <= 0)
                {
                    _pressed = false;
                    _waitingTimeDoned = false;
                    ShieldDeactivate?.Invoke();
                }

                _counter -= Time.deltaTime;
            }
            else
            {
                if (_counter <= _waitingTime)
                {
                    _image.fillAmount = fillAmountValue;

                    if (_counter >= _waitingTime)
                    {
                        _waitingTimeDoned = true;
                    }

                    _counter += Time.deltaTime;
                }
            }
        }

        private void ResetUi()
        {
            _counter = _waitingTime;
            _pressed = false;
            _image.fillAmount = 1;
        }
    }
}
