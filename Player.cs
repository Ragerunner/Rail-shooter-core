using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Header("General")]
    [Tooltip("in ms^-1")] [SerializeField] float Speed = 15f;
    [Tooltip("In m")] [SerializeField] float xRange = 5f;
    [Tooltip("In m")] [SerializeField] float yRange = 3f;
    [Header("Screen position")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float positionYawFactor = 5f;
    [SerializeField] float controlRollFactor = -20f;

    float xThrow, yThrow;
    bool isControlEnabled = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled)
        {
            ProccessTranslation();
            ProccessRotation();
        }
       



    }
    private void OnPlayerDeath()
    {
        isControlEnabled = false;
    }
    private void ProccessRotation()
    { 
        // X asies pakrypimas
        float pitchDueToPostion = transform.localPosition.y* positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPostion + pitchDueToControlThrow;

        float yaw = transform.localPosition.x* positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
    private void ProccessTranslation() 
    {
        // judejimas pagal kamera
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * Speed * Time.deltaTime;
        float yOffset = yThrow * Speed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);


        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
        /* float xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
         float yThrow = CrossPlatformInputManager.GetAxis("Vertical");

         float xOffset = xThrow * Speed * Time.deltaTime;
         float yOffset = yThrow * Speed * Time.deltaTime;

         float rawxPos = transform.localPosition.x + xOffset;
         float backSpeed = 0;
         if (Mathf.Abs(rawxPos) > 0.5f && Mathf.Abs(xThrow) < 0.001f)
         {
             backSpeed = -Mathf.Sign(rawxPos) * Speed * Time.deltaTime;
         }
         float clampedXPos = Mathf.Clamp(rawxPos, -xRange, xRange);

         float rawyPos = transform.localPosition.y + yOffset;

         if (Mathf.Abs(rawyPos) > 0.5f && Mathf.Abs(yThrow) < 0.001f)
         {
             backSpeed = -Mathf.Sign(rawyPos) * Speed * Time.deltaTime;
         }
         float clampedyPos = Mathf.Clamp(rawyPos, -yRange, yRange);

         transform.localPosition = new Vector3(clampedXPos+backSpeed, clampedyPos+backSpeed, transform.localPosition.z);
         */
    } 
}
