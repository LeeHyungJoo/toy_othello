using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Othello;

class StoneInfo
{
    public Point Coord { get; private set; }
    public bool? Side { get; set; } = null;

    public StoneInfo(Point coord)
    {
        Coord = coord;
    }
}
