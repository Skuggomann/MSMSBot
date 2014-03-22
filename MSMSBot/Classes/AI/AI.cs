using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using aima.search.csp;
namespace MSMSBot.Classes.AI
{
    class AI
    {
        CSP drasl;
        public AI()
        {
            drasl = MineSweeperCSP.getMap();
        }

        public Assignment Backtrack()
        {
            return drasl.backTrackingSearch();
        }
    }
}
