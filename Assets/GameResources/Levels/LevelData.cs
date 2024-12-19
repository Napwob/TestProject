using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelData", menuName = "Game/LevelData")]
public class LevelData : ScriptableObject
{
    [SerializeField] private int rows;
    [SerializeField] private int columns;
    [SerializeField] private List<SpriteGroup> allowedSpriteGroups;

    public int Rows { get { return rows; } }
    public int Columns { get { return columns; } }
    public List<SpriteGroup> AllowedSpriteGroups => allowedSpriteGroups;
}