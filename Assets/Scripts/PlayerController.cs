using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")][SerializeField] float controlSpeed = 10;//using same speed for horiz and vert
    [Tooltip("In m")] [SerializeField] float xLimit = 5f;
    [Tooltip("In m")] [SerializeField] float yLimitMax = 2f;
    [Tooltip("In m")] [SerializeField] float yLimitMin = -2f;

    [Header("Screen-position Based")]
    [SerializeField] float positionPitchFactor = -6.5f;
    [SerializeField] float positionYawFactor = 7.4f;
   

    [Header("Control-throw Based")]
    [SerializeField] float controlPitchFactor = -15f;
    [SerializeField] float controlYawFactor = 11f;
    [SerializeField] float controlRollFactor = -20f;
    float xThrow, yThrow;
    bool alive = true;

    void Update()
    {
        if (alive)
        {
            ProcessTranslation();
            ProcessRotation();
        }
    }
    void OnPlayerDeath()//Called by a string reference in CollisionHandler
    {
        alive = false;
        //print("STOP!!!!");
    }
    void ProcessRotation()//Makes ship point ahead instead of to center of screen
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

    void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * controlSpeed * Time.deltaTime;
        float yOffset = yThrow * controlSpeed * Time.deltaTime;

        float newXPos = transform.localPosition.x + xOffset;//takes current x pos and adds xOffset
        float newYPos = transform.localPosition.y + yOffset;//takes current y pos and adds yOffset

        float clampedXPos = Mathf.Clamp(newXPos, -xLimit, xLimit);//Limiting left right movement
        float clampedYPos = Mathf.Clamp(newYPos, yLimitMin, yLimitMax);//Limiting left right movement

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
