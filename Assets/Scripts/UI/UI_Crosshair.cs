using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UI_Crosshair : MonoBehaviour
{
    RawImage crosshair;
    RectTransform rectTransform;
    [SerializeField] Texture normalTexture;
    [SerializeField] Vector2 normalScale;
    [SerializeField] Texture rapidFireTexture;
    [SerializeField] Vector2 rapidFireScale;
    [SerializeField] float holdDelay = 1.0f;

    bool attackStarted;
    float timer;

    private void Awake()
    {
        crosshair = GetComponent<RawImage>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        if (Player.Instance != null)
        {
            Player.Instance.Input.Attack += OnAttack;
            Player.Instance.Input.AttackComplete += OnAttackComplete;
        }
    }

    private void Start()
    {
        ShowCrosshair();
    }

    private void OnAttack()
    {
        attackStarted = true;
        timer = 0;
    }

    private void Update()
    {
        if (attackStarted)
        {
            timer += Time.deltaTime;
            if (timer > holdDelay)
            {
                if (crosshair.texture != rapidFireTexture)
                {
                    ShowRapidfire();
                }
            }
        }
    }

    private void ShowRapidfire()
    {
        crosshair.texture = rapidFireTexture;
        rectTransform.sizeDelta = rapidFireScale;
    }

    private void OnAttackComplete()
    {
        attackStarted = false;
        if (crosshair.texture == rapidFireTexture)
        {
            ShowNormal();
        }
    }

    private void ShowNormal()
    {
        crosshair.texture = normalTexture;
        rectTransform.sizeDelta = normalScale;
    }

    private void OnDisable()
    {
        Player.Instance.Input.Attack -= OnAttack;
        Player.Instance.Input.AttackComplete -= OnAttackComplete;
    }

    public void HideCrosshair()
    {
        crosshair.enabled = false;
    }

    public void ShowCrosshair()
    {
        crosshair.enabled = true;
        ShowNormal();
    }

}
