using System.Collections.Generic;
using UnityEngine;

public class GameResources : Singleton<GameResources>
{
    #region Fields

    [Header("Level")]
    [SerializeField] private List<Level> _levels;

    [Header("FX")] 
    [SerializeField] private ParticleSystem _coinFX;
    [SerializeField] private ParticleSystem _boosterFX;

    #endregion

    #region Properties

    public List<Level> Levels => _levels;

    public ParticleSystem CoinFX => _coinFX;
    
    public ParticleSystem BoosterFX => _boosterFX;

    #endregion
}
