using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.ShieldAbility;
using UnityEngine;

namespace Assets.Scripts.HeroShield
{
    internal class HeroShield : MonoBehaviour
    {
        [SerializeField] private ShieldIconUi _shieldUi;

        [SerializeField] private bool _shieldEnabled;

        public bool ShieldEnabled => _shieldEnabled;

        private void OnEnable()
        {
            _shieldUi.ShieldActivate += ShieldActivate;
            _shieldUi.ShieldDeactivate += ShieldDeactivate;
        }

        private void OnDisable()
        {
            _shieldUi.ShieldActivate -= ShieldActivate;
            _shieldUi.ShieldDeactivate -= ShieldDeactivate;
        }

        private void ShieldActivate() => _shieldEnabled = true;
        private void ShieldDeactivate() => _shieldEnabled = false;
    }
}
