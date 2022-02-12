using BulletHell.Source.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Source.Commands
{
    public class Shield : Command
    {
        private Player entity;
        public Shield(Player entity)
        {
            this.entity = entity;
        }
        public void execute()
        {
            entity.setImmunity();
        }

        public void undo()
        {
            throw new NotImplementedException();
        }
    }
}
