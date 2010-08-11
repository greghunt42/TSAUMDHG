using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;

namespace TSAUMDHG
{
    class MapElement
    {
        public Vector2 position { get; set; }
        public string tileType { get; set; }
        public float rotation { get; set; }

        public MapElement(Vector2 position, string tileType, float rotation)
        {
            this.position = position;
            this.tileType = tileType;
            this.rotation = rotation;
        }
    }
}
