
/// <summary>
/// Different possible effects of abilities, which can be listed inside each ability
/// </summary>
public enum AbilityEffectEnum
{ 
    //stats to affect
    Nothing = 0,
    //Ability augments MaxHealth
    AffectMaxHealth = 1,
    //Ability augments Speed
    AffectSpeed = 2,
    //Ability augments Defense
    AffectDefense = 3,
    //Ability augments RangedDefense
    AffectRangedDefense = 4,
    //Ability augments attack
    AffectAttack = 10,
    //Ability augments AffectHealing
    AffectHealing = 6,
    //Ability augments HealthRegen
    AffectHealthRegen =7,
    //specific to only sewer creatures
    SewerCreatureSpecific = 8,
    //specific to only mechs
    MechSpecific = 9
}

