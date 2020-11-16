﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.AccessControl;
using UnityEngine;

public class ResourceConversionEffect : MonoBehaviour, IUpgradeEffect
{
    [SerializeField] private Cost[] Effects;
    public string Describe()
    {
        string costs = Effects.Where(effect => effect.Amount < 0).Select(effect => $"{-effect.Amount} {effect.Type}")
            .Aggregate((left, right) => $"{left} and {right}");
        string results = Effects.Where(effect => effect.Amount > 0).Select(effect => $"{effect.Amount} {effect.Type}")
            .Aggregate((left, right) => $"{left} and {right}");
        return $"Each cycle, convert {costs} into {results}.";
    }

    public void Apply(Simulation sim, ResourceManager resources)
    {
        foreach (Cost effect in Effects)
        {
            resources.GetResource(effect.Type).Growth += effect.Amount;
        }
    }
}
