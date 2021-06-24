using System;
using System.Collections.Generic;
using System.Text;

namespace Training.DataTests
{
    public class Basis
    {
        public Basis(int id, string basisCode, string basisDescription)
        {
            Id = id;
            BasisCode = basisCode;
            BasisDescription = basisDescription;
        }

        public int Id { get; }
        public string BasisCode { get; }
        public string BasisDescription { get; }
    }

}
