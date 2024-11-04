using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.GridNode;

namespace Assets.Scripts.Grid
{
    public interface IGridEndpoints
    {
        public Node StartPositionNode { get; }
        public Node FinishPositionNode {  get; }
    }
}
