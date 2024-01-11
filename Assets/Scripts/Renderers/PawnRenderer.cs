using UnityEngine;

public class PawnRenderer : MonoBehaviour
{


    public string defaultHeadPath = "Ripoff/Heads/Male_Average_Normal_south";
    public string defaultBodyPath = "Ripoff/Body/Naked_Male_north";

    public Combatable combatScript;
    [SerializeField]
    private Transform bodyTransform; // Reference to the GameObject for the body part
    private SpriteRenderer bodyRenderer;

    [SerializeField]
    private Transform headTransform; // Reference to the GameObject for the head part
    private SpriteRenderer headRenderer;

    [SerializeField]
    private Transform clothesTransform; // Reference to the GameObject for the clothes part
    private SpriteRenderer clothesRenderer;

    [SerializeField]
    private Transform weaponTransform; // Reference to the GameObject for the weapon part
    [SerializeField]

    private SpriteRenderer weaponRenderer;


    private void Start (){
        InitializeSpriteRenderers();
        SetLayerOrder();
        combatScript = GetComponent<Combatable>();
        Render();
    }
    private void Update(){
        Render();
    }

    public void Render(){
        
        if (combatScript.equippedWeapon != null)
        {
            Sprite weaponSprite = Resources.Load<Sprite>(combatScript.equippedWeapon.spritePath);

            // Check if the spriteRenderer component is not null and the loaded sprite is not null
            if (weaponRenderer != null && weaponSprite != null)
            {
                // Set the sprite of the spriteRenderer to the loaded weapon sprite
                weaponRenderer.sprite = weaponSprite;
            }
        }
        if (bodyRenderer != null && bodyRenderer.sprite == null)
        {
            Sprite weaponSprite = Resources.Load<Sprite>(defaultBodyPath);
            bodyRenderer.sprite = weaponSprite;
            
        }
        if (headRenderer != null && headRenderer.sprite == null)
        {
            Sprite weaponSprite = Resources.Load<Sprite>(defaultHeadPath);
            headRenderer.sprite = weaponSprite;
        }

    }

    private void InitializeSpriteRenderers()
    {
        // Get or add SpriteRenderer components to the corresponding GameObjects
        bodyRenderer = GetOrAddSpriteRenderer(bodyTransform);
        headRenderer = GetOrAddSpriteRenderer(headTransform);
        clothesRenderer = GetOrAddSpriteRenderer(clothesTransform);
        weaponRenderer = GetOrAddSpriteRenderer(weaponTransform);

        // Additional initialization or configuration if needed...
    }

    private SpriteRenderer GetOrAddSpriteRenderer(Transform targetTransform)
    {
        if(targetTransform == null){
            targetTransform = transform;
        }
        // Try to get an existing SpriteRenderer component from the GameObject
        SpriteRenderer renderer = targetTransform.GetComponent<SpriteRenderer>();

        // If it doesn't exist, add a new SpriteRenderer component
        if (renderer == null)
        {
            renderer = targetTransform.gameObject.AddComponent<SpriteRenderer>();
        }

        // Configure any additional settings for the SpriteRenderer here
        // For example, you can set the sprite, sorting order, etc.

        return renderer;
    }

    public void SetBodySprite(Sprite sprite)
    {
        bodyRenderer.sprite = sprite;
    }

    public void SetHeadSprite(Sprite sprite)
    {
        headRenderer.sprite = sprite;
    }

    public void SetClothesSprite(Sprite sprite)
    {
        clothesRenderer.sprite = sprite;
    }

    public void SetWeaponSprite(Sprite sprite)
    {
        weaponRenderer.sprite = sprite;
    }

    public void SetLayerOrder()
    {
        if (bodyRenderer != null)
        {
            bodyRenderer.sortingOrder = 4;
        }
        
        if (headRenderer != null)
        {
            headRenderer.sortingOrder = 5;
        }
        
        if (clothesRenderer != null)
        {
            clothesRenderer.sortingOrder = 6;
        }
        
        if (weaponRenderer != null)
        {
            weaponRenderer.sortingOrder = 10;
        }
    }



}