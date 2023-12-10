using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable Objects/Create Tile Template", fileName = "Tile Template")]
public class TileBaseTemplate : ScriptableObject
{
    [SerializeField] private TileBase _tileFloor;

    [Header("Walls")]
    [SerializeField] private TileBase _tileWall;
    [SerializeField] private TileBase _tileWallTop;
    [SerializeField] private TileBase _tileWallBottom;
    [SerializeField] private TileBase _tileWallSideRight;
    [SerializeField] private TileBase _tileWallSideLeft;
    [Space(7.5f)]
    [SerializeField] private TileBase _tileWallInnerCornerDownLeft;
    [SerializeField] private TileBase _tileWallInnerCornerDownRight;
    [SerializeField] private TileBase _tileWallDiagonalCornerDownLeft;
    [SerializeField] private TileBase _tileWallDiagonalCornerDownRight;
    [SerializeField] private TileBase _tileWallDiagonalCornerUpLeft;
    [SerializeField] private TileBase _tileWallDiagonalCornerUpRight;

    public TileBase GetTileFloor { get { return _tileFloor; } }

    public TileBase GetTileWall { get { return _tileWall; } }
    public TileBase GetTileWallTop { get { return _tileWallTop; } }
    public TileBase GetTileWallBottom { get { return _tileWallBottom; } }
    public TileBase GetTileWallSideRight { get { return _tileWallSideRight; } }
    public TileBase GetTileWallSideLeft { get { return _tileWallSideLeft; } }
    public TileBase GetTileWallInnerCornerDownLeft { get { return _tileWallInnerCornerDownLeft; } }
    public TileBase GetTileWallInnerCornerDownRight { get { return _tileWallInnerCornerDownRight; } }
    public TileBase GetTileWallDiagonalCornerDownLeft { get { return _tileWallDiagonalCornerDownLeft; } }
    public TileBase GetTileWallDiagonalCornerDownRight { get { return _tileWallDiagonalCornerDownRight; } }
    public TileBase GetTileWallDiagonalCornerUpLeft { get { return _tileWallDiagonalCornerUpLeft; } }
    public TileBase GetTileWallDiagonalCornerUpRight { get { return _tileWallDiagonalCornerUpRight; } }
}
