# nullable disable

using System;

namespace IndicinaDecideLibrary
{
    public class Rule: JsonSerializable
    {
        public object condition = null;
        public object name = null;
        public object status = null;
    }
}
