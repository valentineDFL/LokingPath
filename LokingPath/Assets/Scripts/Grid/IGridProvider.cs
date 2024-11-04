using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.GridNode;

namespace Assets.Scripts.Grid
{
    public interface IGridProvider
    {
        public Node GetGridNode(int indexX, int indexZ);
        public int GetGridLength(int demision);
    }
}
