using UnityEngine;

public class SpriteAlphaFlicker : MonoBehaviour
{
	public float flickersPerSecond = 15f;
        public float flickerRangeMin = -0.1f;
        public float flickerRangeMax = 0.1f;

	private SpriteRenderer spriteRenderer;
	private float alpha;
        private float time;

	private void Start() 
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		alpha = spriteRenderer.color.a;
	}

	private void Update() 
	{
            if (GetMillisecs() > 1000f / flickersPerSecond) 
	    {
                float newAlpha = alpha + Random.Range(flickerRangeMin, flickerRangeMax);
                Color newColor = spriteRenderer.color;
		newColor.a = newAlpha;
		spriteRenderer.color = newColor;
                ResetTime();
            }
        }

	private float GetMillisecs() 
	{
		return (Time.realtimeSinceStartup - time) * 1000;
	}

	public void ResetTime() 
	{
		time = Time.realtimeSinceStartup;
	}
}
