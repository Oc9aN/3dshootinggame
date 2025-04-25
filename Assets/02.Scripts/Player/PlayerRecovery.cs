using UnityEngine;

public class PlayerRecovery : PlayerComponent
{
    private void Update()
    {
        RecoverStamina();
        RecoverHealth();
    }

    private void RecoverStamina()
    {
        if (!Player.IsRecoverStamina)
        {
            return;
        }

        Player.CurrentStamina += Player.Data.StaminaRecoverPerSecond * Time.deltaTime;
        Player.CurrentStamina = Mathf.Min(Player.CurrentStamina, Player.Data.MaxStamina);
    }

    private void RecoverHealth()
    {
        Player.CurrentHealth += Player.Data.HealthRecoverPerSecond * Time.deltaTime;
        Player.CurrentHealth = Mathf.Min(Player.CurrentHealth, Player.Data.MaxHealth);
    }
}