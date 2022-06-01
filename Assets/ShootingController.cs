using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ShootingController : MonoBehaviour
{
    public float moveForce;
    public GameObject bullet;
    public Transform gun;
    public float shootRate;
    public float shootForce;
    private float m_shootRateTimeStamp;
    Animator m_Animator;
    public TextMeshProUGUI reloadText;
    public float timeToNextShot = 1;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space"))
        {
            if (Time.time > m_shootRateTimeStamp)
            {
                GameObject go = (GameObject)Instantiate(
                bullet, gun.position, gun.rotation);

                go.GetComponent<Rigidbody>().AddForce(gun.forward * shootForce);
                m_shootRateTimeStamp = Time.time + shootRate;
            }
        }

        UpdateReloadText();
    }

    public void UpdateReloadText() {
        timeToNextShot = m_shootRateTimeStamp - Time.time;

        if (timeToNextShot > 0 && timeToNextShot <= shootRate) {
            reloadText.text = "Next shot in: " + (Mathf.Round(timeToNextShot * 100f) / 100f).ToString(); 
        } else {
            reloadText.text = "Shot Ready!";
        }
    }
}