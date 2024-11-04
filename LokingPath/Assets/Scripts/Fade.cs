using System;
using System.Threading.Tasks;
using Assets.Scripts.FinishTrigger;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Fade
    {
        private const int StepOffset = 15;

        private Image _panelImage;

        public enum FadeMode
        {
            fade = 1,
            unFade = 2,
        }

        public Fade(Image panelImage) => _panelImage = panelImage;

        public async Task PanelFade(FadeMode mode)
        {
            Color color = _panelImage.color;

            int minValue = 0;
            int maxValue = byte.MaxValue;
            int currentValue = (int)(_panelImage.color.a * maxValue);

            int offset = 0;
            if (mode == FadeMode.fade)
                offset = StepOffset;
            else if(mode == FadeMode.unFade)
                offset = -StepOffset;

            while (true)
            {
                currentValue += offset;
                float newA = CalculateOneNormalizeValue.CalculateOneNormalize(currentValue, minValue, maxValue);
                color.a = newA;

                _panelImage.color = color;

                if (currentValue == minValue || currentValue == maxValue)
                    return;

                await Task.Delay(40);
            }
        }
    }
}
