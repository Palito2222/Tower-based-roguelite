using UnityEngine;

namespace Game.Player.Combat
{
    public class PlayerCombatController : MonoBehaviour
    {
        public ComboData comboData;
        private int currentComboIndex = 0;
        private float lastAttackTime;

        public Transform attackOrigin; // Empty object delante del jugador

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
                step.attack.Execute(attackOrigin, gameObject);
                lastAttackTime = Time.time;

                currentComboIndex++;
                if (currentComboIndex >= comboData.comboSteps.Count)
                    currentComboIndex = 0;
            }
            else
            {
                // Combo cancelado, reinicia
                currentComboIndex = 0;
            }
        }
    }
}