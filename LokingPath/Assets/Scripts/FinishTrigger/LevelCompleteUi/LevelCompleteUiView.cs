using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;

namespace Assets.Scripts.FinishTrigger.LevelCompleteUi
{
    public class LevelCompleteUiView : MonoBehaviour
    {
        private Image _image;
        private Fade _fade;

        private void OnEnable()
        {
            _image = GetComponent<Image>();
            _fade = new Fade(_image);
        }

        public async Task FadePanel(Fade.FadeMode fadeMode)
        {
            await _fade.PanelFade(fadeMode);
        }
    }
}