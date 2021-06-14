using System;

namespace Asteroids
{
    class GameObjectException : ArgumentException
    {
        public GameObjectException(string msg) : base(msg) { }
    }
}
