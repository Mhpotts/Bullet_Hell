using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Source.Commands
{
    public class Hit : Command
    {

        private List<Entity> entities1;
        private List<Entity> entities2;

        public Hit(Entity entity1, List<Entity> entities2)
        {
            entities1 = new List<Entity>();
            entities1.Add(entity1);
            this.entities2 = entities2;
        }

        public Hit(List<Entity> entities1, List<Entity> entities2)
        {
            this.entities1 = entities1;
            this.entities2 = entities2;
        }

        public void execute()
        {
            foreach (Entity e1 in entities1)
            {
                foreach (Entity e2 in entities2)
                {
                    if (hitting(e1, e2))
                    {
                        e1.hit(e2);
                        e2.hit(e1);
                    }
                }
            }
        }

        public bool hitting(Entity e1, Entity e2)
        {
            return e1.getHitBox().Intersects(e2.getHitBox());
        }



        public void undo()
        {
            throw new NotImplementedException();
        }
    }
}
