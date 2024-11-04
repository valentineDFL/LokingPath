using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Move;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.FinishTrigger
{
    internal class FinishTrigger : MonoBehaviour
    {
        public event Action GameFinished;
        public event Action OnHeroStandOnFinish;

        [SerializeField] private LevelCompleteUi.LevelCompleteUiView _levelCompleteUi;

        private async void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent(typeof(IMovable)))
            {
                print("Вход в финишный триггер");
                _levelCompleteUi.gameObject.SetActive(true);
                
                OnHeroStandOnFinish?.Invoke();

                await _levelCompleteUi.FadePanel(Fade.FadeMode.fade);
                await Task.Delay(1000);
                GameFinished?.Invoke();
                await _levelCompleteUi.FadePanel(Fade.FadeMode.unFade);

                _levelCompleteUi.gameObject.SetActive(false);
            }
        }
    }
}
