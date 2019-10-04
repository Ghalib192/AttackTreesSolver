using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z3TrialConsoleApp
{
    public class Attack : IAttack
    {
        public Attack(string description)
        {
            this.Description = description;
            this.IsLeaf = false;
            this.Operator = BooleanOperator.Identity;
        }

        public Attack(string description, BooleanOperator op)
        {
            this.Description = description;
            this.IsLeaf = false;
            this.Operator = op;

        }

        public Attack(string description, int cost, bool isPossible)
        {
            this.Description = description;
            this.Cost = cost;
            this.IsPossible = isPossible;
            this.IsLeaf = true;
            this.Operator = BooleanOperator.Identity;
        }

        public bool IsLeaf { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public bool IsPossible { get; set; }
        public BooleanOperator Operator { get; set; }
    }
}
