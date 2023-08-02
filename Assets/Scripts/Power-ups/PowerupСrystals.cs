public class PowerupСrystals : Powerup
{
    private float _coins = 0;

    protected override void OnPickedUp(CarController car)
    {
        _coins++;
    }
}