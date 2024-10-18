namespace Clases
{
    public class HealthController
    {
        public int Hp => _hp;
        private int _hp;
        
        public HealthController(int maxHp) => _hp = maxHp;
        public void TakeDamage(int damage) => _hp -= damage;
        public void HealHp(int health)
        {
            _hp += health;
            
            if (_hp > Hp)
            {
                _hp = Hp;
            }
        }
    }
}