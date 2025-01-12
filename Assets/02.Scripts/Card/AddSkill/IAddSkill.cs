using CardGame.Entity;
using DefaultNamespace;

namespace AddSkill
{
    public interface IAddSkill
    {
        public void AddSkillStart(Entity entity, CardData cardData);
        public void AddSkillEnd();
    }
}