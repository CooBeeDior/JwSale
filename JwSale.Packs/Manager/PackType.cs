using JwSale.Packs.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Packs.Manager
{
    public class PackType
    {
        public Type Type { get; set; }

        public Level Level { get; set; }


        public PackType(Type type, Level level)
        {
            this.Type = type;
            this.Level = level;
        }

        public override bool Equals(object obj)
        {
            var currentType = obj as PackType;
            if (currentType != null)
            {
                return Type.FullName.Equals(currentType.Type.FullName);
            }
            else
            {
                return false;
            }

    
        }

        public override int GetHashCode()
        {
            return Type.FullName.GetHashCode();
        }

    }
}
