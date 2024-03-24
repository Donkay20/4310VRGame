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
        // 检查是否仍处于冷却时间
        if (lastShot > Time.time)
        {
            // 尝试射击时，如果处于冷却期，则更新UI显示剩余时间
            UpdateCooldownUI(lastShot - Time.time);
            return;
        }

        // 设置下一次射击的最早时间
        lastShot = Time.time + shootDelay;
        // 初始化UI显示冷却时间
        UpdateCooldownUI(shootDelay);
        GunShotAudio();
        // 调整子弹的初始位置，使其在枪口前方10个单位
        Vector3 spawnPosition = bulletPosition.position + bulletPosition.forward * 10;
        var bulletPrefab = Instantiate(bullet, spawnPosition, bulletPosition.rotation);
        var bulletRb = bulletPrefab.GetComponent<Rigidbody>();
        var direction = bulletPrefab.transform.TransformDirection(Vector3.forward);
        bulletRb.AddForce(direction * bulletSpeed);
        Destroy(bulletPrefab, 0.5f);
    }

    private void UpdateCooldownUI(float timeRemaining)
    {
        // 将显示逻辑封装为一个内部方法，方便复用
        void UpdateText(Text textComponent)
        {
            if (textComponent != null) // 确保Text组件不为null
            {
                // 根据剩余时间更新Text组件的文本
                textComponent.text = timeRemaining <= 0 ? "Ready!" : $"Cooldown: {Mathf.Max(0, timeRemaining).ToString("F2")}s";
            }
        }

        // 更新所有三个Text组件
        UpdateText(cooldownText);
        UpdateText(cooldownText2);
        UpdateText(cooldownText3);
        float quarterCooldown = shootDelay / 4f;

        // 电池按照剩余时间点亮
        bool battery4Active = timeRemaining < quarterCooldown * 4;
        bool battery3Active = timeRemaining < quarterCooldown * 3;
        bool battery2Active = timeRemaining < quarterCooldown * 2;
        bool battery1Active = timeRemaining < quarterCooldown;

        SetBatteryActive(battery4Active, battery3Active, battery2Active, battery1Active);
    }

    // 辅助方法来控制电池图标的激活状态
    private void SetBatteryActive(bool b4, bool b3, bool b2, bool b1)
    {
        if (battery1 != null) battery1.enabled = b1;
        if (battery2 != null) battery2.enabled = b2;
        if (battery3 != null) battery3.enabled = b3;
        if (battery4 != null) battery4.enabled = b4;
    }


    private void Update()
    {
        // 如果处于冷却期，更新冷却时间UI
        if (lastShot > Time.time)
        {
            UpdateCooldownUI(lastShot - Time.time);
        }
        else // 如果不处于冷却期
        {
            // 确保所有冷却文本显示"Ready!"
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
