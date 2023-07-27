using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField]
	private Camera m_Camera;
	[SerializeField]
	private Transform m_Target;
	[SerializeField]
	private float m_InterpolationLinear;
	[SerializeField]
	private float m_InterpolationAngular;
	[SerializeField]
	private float m_CameraOffsetZ;
	[SerializeField]
	private float m_CameraOffsetForward;

	private void FixedUpdate()
	{
		if (m_Target == null || m_Camera == null) return;

		Vector2 cameraPosition = m_Camera.transform.position;
		Vector2 targetPosition = m_Target.position + m_Target.transform.up * m_CameraOffsetForward;
		var newCameraPosition =
			Vector2.Lerp(cameraPosition, targetPosition, m_InterpolationLinear * Time.deltaTime);

		m_Camera.transform.position = new Vector3(newCameraPosition.x, newCameraPosition.y, m_CameraOffsetZ);

		if (m_InterpolationAngular > 0)
		{
			m_Camera.transform.rotation = Quaternion.Slerp(m_Camera.transform.rotation, m_Target.rotation,
				m_InterpolationAngular * Time.deltaTime);
		}
	}

	public void SetTarget(Transform newTarget)
	{
		m_Target = newTarget;
	}
}