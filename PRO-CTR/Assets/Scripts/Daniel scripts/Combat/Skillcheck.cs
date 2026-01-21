using System;
using Daniel_scripts;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Skillcheck : MonoBehaviour
{
    [Header("UI")] 
    public RectTransform needle;
    public Image normalZone;
    public Image greatZone;

    [Header("Speed")] 
    public float rotationsPerSecond = 1.25f;

    [Header("Normalized Zones (0–1)")] 
    public float successMin = 0.5f;
    public float successMax = 0.6f;
    public float greatMin = 0.5f;
    public float greatMax = 0.55f;

    [Header("Randomization")] 
    public float minZoneStart = 0.05f;
    public float maxZoneStart = 0.85f;

    private float timer;
    private bool running;
    public float duration = 1f;

    public float baseDamage;
    public float totalDamage;
    [SerializeField] private float greatMultiplier = 1.3f;
    [SerializeField] private float normalMultiplier = 1f;
    [SerializeField] private float failedMultiplier = 0.7f;

    [SerializeField] private CanvasGroup canvasGroup;
    
    // Callback for when skillcheck completes
    private Action<float> onSkillcheckComplete;

    public void StartSkillCheck(float damage, Action<float> onComplete)
    {
        baseDamage = damage;
        onSkillcheckComplete = onComplete;
        ResetSkillCheck();
    }

    void Update()
    {
        if (!running) return;

        timer += Time.deltaTime / duration;

        RotateNeedle();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Evaluate();
            return;
        }

        // One pass like DBD
        if (timer >= 1f)
        {
            Fail();
        }
    }

    void RotateNeedle()
    {
        float angle = -timer * 360f;
        needle.localEulerAngles = new Vector3(0, 0, angle);
    }

    void ResetSkillCheck()
    {
        timer = 0f;
        running = true;
        totalDamage = baseDamage;
        ShowCanvas();
        RandomizeZones();
        SetupZones();
    }

    void RandomizeZones()
    {
        float normalWidth = successMax - successMin;
        float greatWidth = greatMax - greatMin;

        float start = Random.Range(
            minZoneStart,
            1f - normalWidth - 0.01f
        );

        successMin = start;
        successMax = start + normalWidth;

        float greatOffset = (normalWidth - greatWidth) * 0.5f;
        greatMin = successMin + greatOffset;
        greatMax = greatMin + greatWidth;
    }

    void SetupZones()
    {
        // NORMAL ZONE
        normalZone.type = Image.Type.Filled;
        normalZone.fillMethod = Image.FillMethod.Radial360;
        normalZone.fillOrigin = (int)Image.Origin360.Top;
        normalZone.fillClockwise = true;
        normalZone.fillAmount = successMax - successMin;

        normalZone.rectTransform.localEulerAngles =
            new Vector3(0, 0, -successMin * 360f);

        // GREAT ZONE
        greatZone.type = Image.Type.Filled;
        greatZone.fillMethod = Image.FillMethod.Radial360;
        greatZone.fillOrigin = (int)Image.Origin360.Top;
        greatZone.fillClockwise = true;
        greatZone.fillAmount = greatMax - greatMin;

        greatZone.rectTransform.localEulerAngles =
            new Vector3(0, 0, -greatMin * 360f);
    }

    void Evaluate()
    {
        running = false;

        Debug.Log($"Timer: {timer:F3} | Success: {successMin:F2}-{successMax:F2} | Great: {greatMin:F2}-{greatMax:F2}");

        if (timer >= greatMin && timer <= greatMax)
        {
            totalDamage = baseDamage * greatMultiplier;
            Debug.Log($"GREAT SKILL CHECK - Damage: {totalDamage}");
        }
        else if (timer >= successMin && timer <= successMax)
        {
            totalDamage = baseDamage * normalMultiplier;
            Debug.Log($"SUCCESS SKILL CHECK - Damage: {totalDamage}");
        }
        else
        {
            totalDamage = baseDamage * failedMultiplier;
            Debug.Log($"FAILED SKILL CHECK - Damage: {totalDamage}");
        }

        HideCanvas();
        
        // Invoke callback with final damage
        onSkillcheckComplete?.Invoke(totalDamage);
    }

    void Fail()
    {
        if (!running) return;

        running = false;
        totalDamage = baseDamage * failedMultiplier;
        HideCanvas();
        
        Debug.Log($"FAILED SKILL CHECK (timeout) - Damage: {totalDamage}");
        
        // Invoke callback with failed damage
        onSkillcheckComplete?.Invoke(totalDamage);
    }

    private void ShowCanvas()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
    }

    private void HideCanvas()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }
}