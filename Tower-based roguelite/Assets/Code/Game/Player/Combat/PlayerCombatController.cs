using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Player.Combat
{
    public class PlayerCombatController : MonoBehaviour
    {
        [Title("Combo Data")]
        [SerializeField]
        public ComboData comboData;

        private int currentComboIndex = 0;
        private float lastAttackTime;

        public Transform attackOrigin;

        void Update()
        {
            if (InputManager.Instance.IsAttackPressed())
                TryComboAttack();
        }

        void TryComboAttack()
        {
            if (comboData == null || comboData.comboSteps.Count == 0) return;

            float timeSinceLast = Time.time - lastAttackTime;
            var step = comboData.comboSteps[currentComboIndex];

            if (timeSinceLast <= step.comboWindow || currentComboIndex == 0)
            {
                step.Execute(attackOrigin, gameObject);
                lastAttackTime = Time.time;

                currentComboIndex = (currentComboIndex + 1) % comboData.comboSteps.Count;
            }
            else
            {
                // Combo cancelado, reinicia
                currentComboIndex = 0;
            }
        }

        public void SetCombo(ComboData combo)
        {
            comboData = combo;
            comboData?.Initialize();
        }
    }
}