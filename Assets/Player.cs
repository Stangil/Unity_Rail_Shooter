using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Player : MonoBehaviour
{
    [Tooltip("In ms^-1")][SerializeField] float movementSpeed = 20;//using same speed for horiz and vert
    [Tooltip("In m")] [SerializeField] float xLimit = 4f;
    [Tooltip("In m")] [SerializeField] float yLimitMax = 2f;
    [Tooltip("In m")] [SerializeField] float yLimitMin = -5f;

    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float controlPitchFactor = -30f;
    [SerializeField] float positionYawFactor = 4f;
    [SerializeField] float controlYawFactor = 30f;
    [SerializeField] float controlRollFactor = -40f;
    private float xThrow, yThrow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        //TODO Take away pitch and yaw from contoler when movement is maxed out
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yawDueToPosition = transform.localPosition.x * positionYawFactor;
        float yawDueToControlThrow = xThrow * controlYawFactor;      
        float yaw = yawDueToPosition + yawDueToControlThrow;
        
        float roll = controlRollFactor * xThrow;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * movementSpeed * Time.deltaTime;
        float yOffset = yThrow * movementSpeed * Time.deltaTime;

        float newXPos = transform.localPosition.x + xOffset;//takes current x pos and adds xOffset
        float newYPos = transform.localPosition.y + yOffset;//takes current y pos and adds yOffset

        float clampedXPos = Mathf.Clamp(newXPos, -xLimit, xLimit);//Limiting left right movement
        float clampedYPos = Mathf.Clamp(newYPos, yLimitMin, yLimitMax);//Limiting left right movement

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
