namespace UnchordMetroidvania
{
    public abstract class EntityHealthArgs
    {
        public float minHealth;
        public float maxHealth;
        public float prevHealth;
        public float currentHealth;
        public float dHealthOriginal;
        public float dHealthReal => currentHealth - prevHealth;
    }

    public class EntityHealArgs : EntityHealthArgs
    {
        
    }

    public class EntityDamageArgs : EntityHealthArgs
    {

    }

    public class EntityManaArgs
    {
        public float minMana;
        public float maxMana;
        public float prevMana;
        public float currentMana;
        public float dManaOriginal;
        public float dManaReal => currentMana - prevMana;
    }

    public class EntityChargeArgs : EntityManaArgs
    {

    }

    public class EntityExpenseArgs : EntityManaArgs
    {

    }
}