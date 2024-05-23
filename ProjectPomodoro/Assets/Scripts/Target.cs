using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal class Target
    {
        bool isOcupado;
        bool isBroken;
        Transform location;
        public Target(Transform obj)
        {
            isOcupado = false;
            isBroken = false;
            location = obj.transform;
        }

        public bool IsBroken
        {
            get { return isBroken; }
            set { isBroken = value; }
        }
        public bool IsOcupado
        {
            get { return isOcupado; }
            set { isOcupado = value; }
        }
        public Transform Location
        {
            get { return location; }
            set { location = value; }
        }
    }
}
