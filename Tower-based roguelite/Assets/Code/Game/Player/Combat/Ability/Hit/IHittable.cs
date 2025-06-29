public interface IHittable
{
    void ApplyHit(HitContext context);
    void ApplyEffect(string effectId); // Opcional, por si quieres efectos como Knockback, Slow, etc
}