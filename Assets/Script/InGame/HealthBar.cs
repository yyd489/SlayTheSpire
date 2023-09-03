using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FrameWork
{
    public class HealthBar : MonoBehaviour
    {
        Slider slider;
        Image image;

        [SerializeField] Image shieldImg;
        [SerializeField] TextMeshProUGUI tmpShild;
        [SerializeField] Image hpGaugeImg;
        [SerializeField] TextMeshProUGUI tmpHealth;

        private GameObject character;

        public ObjectPool iconPool;

        Color shieldColor;

        public void Init(int hp, int maxHp, int shield, GameObject initCharacter)
        {
            shieldColor = new Color32(46, 75, 126, 255);
            slider = GetComponent<Slider>();
            image = GetComponent<Image>();
            character = initCharacter;
            SetHealthGauge(hp, maxHp, shield);
        }

        private void FixedUpdate()
        {
            if (character != null)
                transform.position = Camera.main.WorldToScreenPoint(character.transform.position - new Vector3(0f, 3f, 0));
        }

        public void SetHealthGauge(int hp, int maxHp, int shield)
        {
            bool isShield = shield > 0;
            float value = (float)hp / (float)maxHp;

            slider.value = value;

            image.enabled = isShield;
            shieldImg.gameObject.SetActive(isShield);
            tmpShild.text = shield.ToString();
            hpGaugeImg.color = isShield ? shieldColor : Color.red;

            tmpShild.text = shield.ToString();
            tmpHealth.text = string.Format("{0}/{1}", hp, maxHp);
        }
    }
}