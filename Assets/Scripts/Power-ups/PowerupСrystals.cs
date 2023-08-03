public class PowerupСrystals : Powerup
{
    protected override void OnPickedUp(CarController car)
    {
        Destroy(gameObject);
    }
}