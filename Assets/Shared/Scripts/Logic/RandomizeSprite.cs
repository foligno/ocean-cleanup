using UnityEngine;

namespace OceanCleanup.Shared.Logic
{
    public class RandomizeSprite : MonoBehaviour
    {
        [SerializeField] private Sprite[] sprites;
    
        private void Awake()
        {
            GetComponent<SpriteRenderer>().sprite = sprites[Mathf.FloorToInt(Random.Range(0, sprites.Length))];
        }
    }
}
