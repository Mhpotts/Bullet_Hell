using System;
using System.Collections.Generic;
using System.Text;

namespace BulletHell.Source
{
    public interface Command
    {
        public void execute();

        public void undo();

    }
}
