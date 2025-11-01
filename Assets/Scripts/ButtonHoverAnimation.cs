using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonHoverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Animation Settings")]
    [SerializeField] private float hoverScale = 1.1f;
    [SerializeField] private float animationDuration = 0.2f;
    
    [Header("Color Animation")]
    [SerializeField] private bool enableColorAnimation = true;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color hoverColor = new Color(1f, 0.9f, 0.7f, 1f);
    
    [Header("Shadow Animation")]
    [SerializeField] private bool enableShadowAnimation = true;
    [SerializeField] private Vector2 normalShadowDistance = new Vector2(2f, -2f);
    [SerializeField] private Vector2 hoverShadowDistance = new Vector2(4f, -4f);
    
    private Vector3 originalScale;
    private Image buttonImage;
    private Shadow buttonShadow;
    private bool isHovering = false;
    private Coroutine currentAnimation;
    
    private void Awake()
    {
        originalScale = transform.localScale;
        buttonImage = GetComponent<Image>();
        buttonShadow = GetComponent<Shadow>();
        
        // Set initial colors
        if (buttonImage != null && enableColorAnimation)
        {
            normalColor = buttonImage.color;
            buttonImage.color = normalColor;
        }
        
        // Set initial shadow
        if (buttonShadow != null && enableShadowAnimation)
        {
            buttonShadow.effectDistance = normalShadowDistance;
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isHovering) return;
        isHovering = true;
        
        if (currentAnimation != null)
            StopCoroutine(currentAnimation);
        
        currentAnimation = StartCoroutine(AnimateToHover());
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isHovering) return;
        isHovering = false;
        
        if (currentAnimation != null)
            StopCoroutine(currentAnimation);
        
        currentAnimation = StartCoroutine(AnimateToNormal());
    }
    
    private IEnumerator AnimateToHover()
    {
        float elapsedTime = 0f;
        Vector3 startScale = transform.localScale;
        Vector3 targetScale = originalScale * hoverScale;
        Color startColor = buttonImage != null ? buttonImage.color : Color.white;
        Vector2 startShadow = buttonShadow != null ? buttonShadow.effectDistance : Vector2.zero;
        
        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / animationDuration;
            
            // Ease out back animation curve
            t = EaseOutBack(t);
            
            // Scale animation
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            
            // Color animation
            if (buttonImage != null && enableColorAnimation)
            {
                buttonImage.color = Color.Lerp(startColor, hoverColor, t);
            }
            
            // Shadow animation
            if (buttonShadow != null && enableShadowAnimation)
            {
                buttonShadow.effectDistance = Vector2.Lerp(startShadow, hoverShadowDistance, t);
            }
            
            yield return null;
        }
        
        // Ensure final values
        transform.localScale = targetScale;
        if (buttonImage != null && enableColorAnimation)
            buttonImage.color = hoverColor;
        if (buttonShadow != null && enableShadowAnimation)
            buttonShadow.effectDistance = hoverShadowDistance;
    }
    
    private IEnumerator AnimateToNormal()
    {
        float elapsedTime = 0f;
        Vector3 startScale = transform.localScale;
        Vector3 targetScale = originalScale;
        Color startColor = buttonImage != null ? buttonImage.color : Color.white;
        Vector2 startShadow = buttonShadow != null ? buttonShadow.effectDistance : Vector2.zero;
        
        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / animationDuration;
            
            // Ease out back animation curve
            t = EaseOutBack(t);
            
            // Scale animation
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            
            // Color animation
            if (buttonImage != null && enableColorAnimation)
            {
                buttonImage.color = Color.Lerp(startColor, normalColor, t);
            }
            
            // Shadow animation
            if (buttonShadow != null && enableShadowAnimation)
            {
                buttonShadow.effectDistance = Vector2.Lerp(startShadow, normalShadowDistance, t);
            }
            
            yield return null;
        }
        
        // Ensure final values
        transform.localScale = targetScale;
        if (buttonImage != null && enableColorAnimation)
            buttonImage.color = normalColor;
        if (buttonShadow != null && enableShadowAnimation)
            buttonShadow.effectDistance = normalShadowDistance;
    }
    
    private float EaseOutBack(float t)
    {
        const float c1 = 1.70158f;
        const float c3 = c1 + 1f;
        
        return 1f + c3 * Mathf.Pow(t - 1f, 3f) + c1 * Mathf.Pow(t - 1f, 2f);
    }
    
    private void OnDisable()
    {
        // Reset to original state when disabled
        if (currentAnimation != null)
        {
            StopCoroutine(currentAnimation);
            currentAnimation = null;
        }
        
        transform.localScale = originalScale;
        isHovering = false;
        
        if (buttonImage != null && enableColorAnimation)
        {
            buttonImage.color = normalColor;
        }
        
        if (buttonShadow != null && enableShadowAnimation)
        {
            buttonShadow.effectDistance = normalShadowDistance;
        }
    }
}