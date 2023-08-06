using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public abstract class Pickup : MonoBehaviour
{
	protected bool IsCollected = false;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		CarController car = collision.transform.root.GetComponent<CarController>();

		if (car != null && car.CompareTag("Player"))
		{
			if (!IsCollected)
			{
				OnPickedUp(car);
				Collect(car);
			}
		}
	}

	protected abstract void OnPickedUp(CarController car);

	protected abstract void Collect(CarController car);
}