[System.Serializable]
public class AbilityData
{
    public string id;
    public string name;
    public float cooldown;
    public float manaCost;

    public string attackReference; // Vincula a un ataque tipo Fireball, AoE, Melee especial...
}
