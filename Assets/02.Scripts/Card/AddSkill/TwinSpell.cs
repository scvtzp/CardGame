using CardGame.Entity;
using DefaultNamespace;
using Manager;

namespace AddSkill
{
    public class TwinSpell : IAddSkill
    {
        public void AddSkillStart(Entity entity, CardData cardData)
        {
            CardData usedCard= new CardData(cardData._costAndTarget, cardData.GetSkill());
            entity.deckService.AddCardInHand(usedCard);
        }

        public void AddSkillEnd()
        {
            ;
        }
    }
}