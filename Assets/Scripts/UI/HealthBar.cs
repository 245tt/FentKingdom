using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Color barColor;
    public LivingEntity livingEntity;
    public Slider slider;
    public Vector3 offset;

    void Start()
    {
        gameObject.SetActive(true);
        livingEntity = transform.parent.GetComponent<LivingEntity>();
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position+ offset);
        slider.fillRect.GetComponentInChildren<Image>().color = barColor;
    }
    private void Update()
    {
        UpdateHealthBar();
    }
    public void UpdateHealthBar() 
    {
        if (livingEntity == null) return;
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
        
        //slider.gameObject.SetActive(livingEntity.Health< livingEntity.MaxHealth);
        slider.value = livingEntity.Health / livingEntity.MaxHealth;
    }
}
