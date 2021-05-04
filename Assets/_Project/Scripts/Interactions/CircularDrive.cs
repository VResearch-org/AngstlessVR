// ----------------------------------------------------------------------------
// <summary>
// Can be used to move object in a circular motion
// </summary>
// ----------------------------------------------------------------------------
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

public class CircularDrive : MonoBehaviour, IMixedRealityPointerHandler
{
	#region Common Variables
	public enum Axis_t
	{
		XAxis,
		YAxis,
		ZAxis
	};

	[Tooltip("The axis around which the circular drive will rotate in local space")]
	public Axis_t axisOfRotation = Axis_t.XAxis;

	[Tooltip("Child GameObject which is driving the rotation")]
	public Collider childCollider = null;

	private Vector3 lastPosition, currentPosition;
	private Vector3 worldPlaneNormal = new Vector3(1.0f, 0.0f, 0.0f);
	private Vector3 localPlaneNormal = new Vector3(1.0f, 0.0f, 0.0f);

	[Header("Limited Rotation")]
	[Tooltip("If true, the rotation will be limited to [minAngle, maxAngle], if false, the rotation is unlimited")]
	public bool limited = false;

	[Header("Limited Rotation Min")]
	[Tooltip("If limited is true, the specifies the lower limit, otherwise value is unused")]
	public float minAngle = -45.0f;

	[Header("Limited Rotation Max")]
	[Tooltip("If limited is true, the specifies the upper limit, otherwise value is unused")]
	public float maxAngle = 45.0f;
	private float angleTmp;

	[Tooltip("The output angle value of the drive in degrees, unlimited will increase or decrease without bound, take the 360 modulus to find number of rotations")]
	public float outAngle;

	public bool isRotating;

	private Quaternion start;
	#endregion

	#region Unity Callbacks
	private void Start()
	{
		//lastPosition = currentPosition = childCollider.transform.position;
		worldPlaneNormal = new Vector3(0.0f, 0.0f, 0.0f);
		worldPlaneNormal[(int)axisOfRotation] = 1.0f;

		localPlaneNormal = worldPlaneNormal;

		if (transform.parent)
		{
			worldPlaneNormal = transform.parent.localToWorldMatrix.MultiplyVector(worldPlaneNormal).normalized;
		}

		if (limited)
		{
			start = Quaternion.identity;
			outAngle = transform.localEulerAngles[(int)axisOfRotation];
		}
		else
		{
			start = Quaternion.AngleAxis(transform.localEulerAngles[(int)axisOfRotation], localPlaneNormal);
			outAngle = 0.0f;
		}
	}
	/// <summary>
	/// Updated based on MRTK hand position got from the handle
	/// </summary>
	private void UpdateAngle(Vector3 position)
	{
		if (isRotating)
		{
			ComputeAngle(position);
			transform.localRotation = start * Quaternion.AngleAxis(outAngle, localPlaneNormal);
		}
	}
	#endregion

	#region Common Methods
	/// <summary>
	/// Computes the angle to rotate the game object based on the change in the transform
	/// </summary>
	/// <param name="followObject"></param>
	private void ComputeAngle(Vector3 position)
	{
		currentPosition = position;

		if (!currentPosition.Equals(lastPosition))
		{
			float absAngleDelta = Vector3.Angle(lastPosition - transform.position, currentPosition - transform.position);

			if (absAngleDelta > 0.0f)
			{
				Vector3 cross = Vector3.Cross(lastPosition - transform.position, currentPosition - transform.position).normalized;
				float dot = Vector3.Dot(worldPlaneNormal, cross);

				float signedAngleDelta = absAngleDelta;

				if (dot < 0.0f)
				{
					signedAngleDelta = -signedAngleDelta;
				}

				if (limited)
				{
					angleTmp = Mathf.Clamp(outAngle + signedAngleDelta, minAngle, maxAngle);

					if (outAngle == minAngle)
					{
						if (angleTmp > minAngle)
						{
							outAngle = angleTmp;
							lastPosition = currentPosition;
						}
					}
					else if (outAngle == maxAngle)
					{
						if (angleTmp < maxAngle)
						{
							outAngle = angleTmp;
							lastPosition = currentPosition;
						}
					}
					else if (angleTmp == minAngle)
					{
						outAngle = angleTmp;
						lastPosition = currentPosition;
					}
					else if (angleTmp == maxAngle)
					{
						outAngle = angleTmp;
						lastPosition = currentPosition;
					}
					else
					{
						outAngle = angleTmp;
						lastPosition = currentPosition;
					}
				}
				else
				{
					outAngle += signedAngleDelta;
					lastPosition = currentPosition;
				}
			}
		}
	}
	#endregion

	#region IMixedRealityPointerHandler
	public void OnPointerDown(MixedRealityPointerEventData eventData)
	{
		lastPosition = eventData.Pointer.Position;
		isRotating = true;
	}

	public void OnPointerDragged(MixedRealityPointerEventData eventData)
	{
		UpdateAngle(eventData.Pointer.Position);
	}

	public void OnPointerUp(MixedRealityPointerEventData eventData)
	{
		isRotating = false;
	}

	public void OnPointerClicked(MixedRealityPointerEventData eventData)
	{
	}
	#endregion
}
