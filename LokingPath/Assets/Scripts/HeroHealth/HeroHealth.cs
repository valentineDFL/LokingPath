using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using Assets.Scripts.Move;
using Assets.Scripts.FinishTrigger.LevelCompleteUi;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization;

namespace Assets.Scripts.HeroHealth
{
    internal class HeroHealth : MonoBehaviour, IDamageable
    {
        public event Action OnHeroDeaded;
        public event Action OnHeroStandOnDangerZone;

        [SerializeField] private LevelCompleteUiView _levelCompleteUi;

        public bool IsAlive { get; private set; } = true;

        public async void TakeDamage()
        {
            _levelCompleteUi.gameObject.SetActive(true);

            IsAlive = false;
            OnHeroStandOnDangerZone?.Invoke();

            await _levelCompleteUi.FadePanel(Fade.FadeMode.fade);

            await Task.Delay(1000);

            OnHeroDeaded?.Invoke();
            await _levelCompleteUi.FadePanel(Fade.FadeMode.unFade);

            IsAlive = true;

            _levelCompleteUi.gameObject.SetActive(false);
        }
    }
}
