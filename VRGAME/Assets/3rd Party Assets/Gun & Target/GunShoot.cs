using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GunShoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletPosition;
    [SerializeField] private float shootDelay = 2f;
    [Range(0, 30000), SerializeField] private float bulletSpeed;
    [Space, SerializeField] private AudioSource audiosource;
    [SerializeField] private Text cooldownText;
    [SerializeField] private Text cooldownText2;
    [SerializeField] private Text cooldownText3;

    [SerializeField] private Image battery1;
    [SerializeField] private Image battery2;
    [SerializeField] private Image battery3;
    [SerializeField] private Image battery4;


    private float lastShot;

    public void Shoot()
    {
        // ����Ƿ��Դ�����ȴʱ��
        if (lastShot > Time.time)
        {
            // �������ʱ�����������ȴ�ڣ������UI��ʾʣ��ʱ��
            UpdateCooldownUI(lastShot - Time.time);
            return;
        }

        // ������һ�����������ʱ��
        lastShot = Time.time + shootDelay;
        // ��ʼ��UI��ʾ��ȴʱ��
        UpdateCooldownUI(shootDelay);
        GunShotAudio();
        // �����ӵ��ĳ�ʼλ�ã�ʹ����ǹ��ǰ��10����λ
        Vector3 spawnPosition = bulletPosition.position + bulletPosition.forward * 10;
        var bulletPrefab = Instantiate(bullet, spawnPosition, bulletPosition.rotation);
        var bulletRb = bulletPrefab.GetComponent<Rigidbody>();
        var direction = bulletPrefab.transform.TransformDirection(Vector3.forward);
        bulletRb.AddForce(direction * bulletSpeed);
        Destroy(bulletPrefab, 0.5f);
    }

    private void UpdateCooldownUI(float timeRemaining)
    {
        // ����ʾ�߼���װΪһ���ڲ����������㸴��
        void UpdateText(Text textComponent)
        {
            if (textComponent != null) // ȷ��Text�����Ϊnull
            {
                // ����ʣ��ʱ�����Text������ı�
                textComponent.text = timeRemaining <= 0 ? "Ready!" : $"Cooldown: {Mathf.Max(0, timeRemaining).ToString("F2")}s";
            }
        }

        // ������������Text���
        UpdateText(cooldownText);
        UpdateText(cooldownText2);
        UpdateText(cooldownText3);
        float quarterCooldown = shootDelay / 4f;

        // ��ذ���ʣ��ʱ�����
        bool battery4Active = timeRemaining < quarterCooldown * 4;
        bool battery3Active = timeRemaining < quarterCooldown * 3;
        bool battery2Active = timeRemaining < quarterCooldown * 2;
        bool battery1Active = timeRemaining < quarterCooldown;

        SetBatteryActive(battery4Active, battery3Active, battery2Active, battery1Active);
    }

    // �������������Ƶ��ͼ��ļ���״̬
    private void SetBatteryActive(bool b4, bool b3, bool b2, bool b1)
    {
        if (battery1 != null) battery1.enabled = b1;
        if (battery2 != null) battery2.enabled = b2;
        if (battery3 != null) battery3.enabled = b3;
        if (battery4 != null) battery4.enabled = b4;
    }


    private void Update()
    {
        // ���������ȴ�ڣ�������ȴʱ��UI
        if (lastShot > Time.time)
        {
            UpdateCooldownUI(lastShot - Time.time);
        }
        else // �����������ȴ��
        {
            // ȷ��������ȴ�ı���ʾ"Ready!"
            if (cooldownText != null && cooldownText.text != "Ready!")
            {
                cooldownText.text = "Ready!";
            }
            if (cooldownText2 != null && cooldownText2.text != "Ready!")
            {
                cooldownText2.text = "Ready!";
            }
            if (cooldownText3 != null && cooldownText3.text != "Ready!")
            {
                cooldownText3.text = "Ready!";
            }
        }
        if (Time.time >= lastShot)
        {
            SetBatteryActive(true, true, true, true);
        }
    }




    private void GunShotAudio ()
    {
        var random = UnityEngine.Random.Range(0.8f, 1.2f);
        audiosource.pitch = random;
        audiosource.Play();
    }
}
