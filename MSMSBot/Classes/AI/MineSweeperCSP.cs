using aima.search.csp;
using MSMSBot.Classes.Window_Interaction;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSMSBot.Classes.AI
{
    class MineSweeperCSP : CSP
    {
        static private MineSweeperCSP instance = null;

        MineSweeperCSP(ArrayList variables, Constraint constraints, Domain domains)
            : base(variables, constraints, domains)
		{
			//super ;
		}

        public static CSP getMap()
        {
            if (instance != null) return instance;

            ArrayList variables = new ArrayList();
            for (int col = 0; col < 9; col++)
            {
                for (int row = 0; row < 9; row++)
                {
                    variables.Add("point"+ row + "" + col);
                }
            }


            Domain domains = new Domain(variables);
            for (int i = 0; i < variables.Count; i++)
            {
                string variable = variables[i].ToString();
                domains.addToDomain(variable, "0");
                domains.addToDomain(variable, "1");
            }



            Constraint mineSweeperConstraints = new MineSweeperConstraints(ScreenGateway.GetBoardLayout());

            instance = new MineSweeperCSP(variables, mineSweeperConstraints, domains);
            return instance;
        }
    }
}
