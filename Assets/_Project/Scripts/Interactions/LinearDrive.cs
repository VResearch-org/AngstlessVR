// ----------------------------------------------------------------------------
// <summary>
// Drives a linear mapping based on position between 2 points
// </summary>
// ----------------------------------------------------------------------------
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

public class LinearDrive : MonoBehaviour, IMixedRealityPointerHandler
{
    #region Common Variables
    public Transform startPosition;
    public Transform endPosition;
    public LinearMapping linearMapping;
    public bool repositionGameObject = true;
    public bool maintainMomentum = true;
    public float momentumDampenRate = 5.0f;

    public Vector3 currentHand;

    protected float initialMappingOffset;
    protected int numMappingChangeSamples = 5;
    protected float[] mappingChangeSamples;
    protected float prevMapping = 0.0f;
    protected float mappingChangeRate;
    protected int sampleCount = 0;

    public bool isAttached;
    #endregion

    #region Unity Callbacks
    protected virtual void Awake()
    {
        currentHand = transform.position;
        mappingChangeSamples = new float[numMappingChangeSamples];
    }

    protected virtual void Start()
    {
        if (linearMapping == null)
        {
            linearMapping = GetComponent<LinearMapping>();
        }

        if (linearMapping == null)
        {
            linearMapping = gameObject.AddComponent<LinearMapping>();
        }

        initialMappingOffset = linearMapping.value;

        if (repositionGameObject)
        {
            UpdateLinearMapping(transform.position);
        }
    }
    protected virtual void Update()
    {
        if (maintainMomentum && mappingChangeRate != 0.0f)
        {
            //Dampen the mapping change rate and apply it to the mapping
            mappingChangeRate = Mathf.Lerp(mappingChangeRate, 0.0f, momentumDampenRate * Time.deltaTime);
            linearMapping.value = Mathf.Clamp01(linearMapping.value + (mappingChangeRate * Time.deltaTime));

            if (repositionGameObject)
            {
                transform.position = Vector3.Lerp(startPosition.position, endPosition.position, linearMapping.value);
            }
        }
        if (isAttached)
            HandAttachedUpdate(currentHand);
    }
    #endregion

    #region Common Methods
    /// <summary>
    /// Is called when user grabs the driven interactable
    /// </summary>
    /// <param name="handTransform"></param>
    protected virtual void OnAttachedToHand(Vector3 handPosition)
    {
        currentHand = handPosition;
        initialMappingOffset = linearMapping.value - CalculateLinearMapping(currentHand);
        sampleCount = 0;
        mappingChangeRate = 0.0f;
        isAttached = true;
    }

    /// <summary>
    /// Updates the position of the driven interactable by hand position
    /// </summary>
    /// <param name="handTransform"></param>
    protected virtual void HandAttachedUpdate(Vector3 handPosition)
    {
        currentHand = handPosition;
        UpdateLinearMapping(handPosition);
    }

    /// <summary>
    /// Is called when user releases the interactable
    /// </summary>
    protected virtual void OnDetachedFromHand()
    {
        isAttached = false;
        CalculateMappingChangeRate();
    }

    /// <summary>
    /// Compute the mapping change rate
    /// </summary>
    protected void CalculateMappingChangeRate()
    {
        mappingChangeRate = 0.0f;
        int mappingSamplesCount = Mathf.Min(sampleCount, mappingChangeSamples.Length);
        if (mappingSamplesCount != 0)
        {
            for (int i = 0; i < mappingSamplesCount; ++i)
            {
                mappingChangeRate += mappingChangeSamples[i];
            }
            mappingChangeRate /= mappingSamplesCount;
        }
    }

    /// <summary>
    /// Updates the motion of driven object by hand position
    /// </summary>
    /// <param name="updateTransform"></param>
    protected void UpdateLinearMapping(Vector3 updatePosition)
    {
        prevMapping = linearMapping.value;
        linearMapping.value = Mathf.Clamp01(initialMappingOffset + CalculateLinearMapping(updatePosition));

        mappingChangeSamples[sampleCount % mappingChangeSamples.Length] = (1.0f / Time.deltaTime) * (linearMapping.value - prevMapping);
        sampleCount++;

        if (repositionGameObject)
        {
            transform.position = Vector3.Lerp(startPosition.position, endPosition.position, linearMapping.value);
        }
    }

    /// <summary>
    /// Calculates the motion of driven object by hand position
    /// </summary>
    /// <param name="updateTransform"></param>
    /// <returns></returns>
    protected float CalculateLinearMapping(Vector3 updatePosition)
    {
        Vector3 direction = endPosition.position - startPosition.position;
        float length = direction.magnitude;
        direction.Normalize();

        Vector3 displacement = updatePosition - startPosition.position;

        return Vector3.Dot(displacement, direction) / length;
    }
    #endregion

    #region IMixedRealityPointerHandler
    public void OnPointerDown(MixedRealityPointerEventData eventData)
    {
        OnAttachedToHand(eventData.Pointer.Position);
    }

    public void OnPointerDragged(MixedRealityPointerEventData eventData)
    {
        HandAttachedUpdate(eventData.Pointer.Position);
    }

    public void OnPointerUp(MixedRealityPointerEventData eventData)
    {
        OnDetachedFromHand();
    }

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
    }
    #endregion
}
