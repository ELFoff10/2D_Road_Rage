public class PowerUpGem : Powerup
{
    protected override void OnPickedUp(CarController car)
    {
        Destroy(gameObject);
    }
}