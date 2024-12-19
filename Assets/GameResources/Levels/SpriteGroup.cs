namespace AmayaSoft.Cell
{
    using UnityEngine;
    using System.Collections.Generic;

    [CreateAssetMenu(fileName = "SpriteGroup", menuName = "Game/SpriteGroup")]
    public class SpriteGroup : ScriptableObject
    {
        [SerializeField] private List<Sprite> sprites;

        public List<Sprite> Sprites => sprites;
    }
}
