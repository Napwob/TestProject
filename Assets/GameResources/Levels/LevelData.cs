namespace AmayaSoft.Level
{
    using UnityEngine;
    using System.Collections.Generic;
    using AmayaSoft.Cell;

    [CreateAssetMenu(fileName = "LevelData", menuName = "Game/LevelData")]
    public class LevelData : ScriptableObject
    {
        [SerializeField] private int rows;
        [SerializeField] private int columns;
        [SerializeField] private List<SpriteGroup> allowedSpriteGroups;

        public int Rows { get { return rows; } }
        public int Columns { get { return columns; } }
        public IReadOnlyList<SpriteGroup> AllowedSpriteGroups => allowedSpriteGroups;
    }
}
