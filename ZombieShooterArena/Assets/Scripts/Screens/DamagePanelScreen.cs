using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Screens
{
    public class DamagePanelScreen : MonoBehaviour
    {
        [SerializeField] private Image DamageImage;

        private void OnEnable()
        {
            DamageImage.color = Color.white;
            StartCoroutine(OnFadeScreen());
        }

        private IEnumerator OnFadeScreen()
        {
            float alpha = DamageImage.color.a;

            while (alpha > 0)
            {
                alpha -= Time.deltaTime;
                Color color = new Color(255, 255, 255, alpha);
                DamageImage.color = color;
                yield return new WaitForEndOfFrame();
            }

            gameObject.SetActive(false);
        }
    }
}