using System;
using System.Collections.Generic;
using PrimeAssault.Models;

namespace PrimeAssault.Services
{
    public class MoveHolder
    {
        public List<MoveModel> datalist = new List<MoveModel>()
            {
                new MoveModel {
                    Name = "Crackshot",
                    Description = "Years of sewer-plinking simulations have given you formidable ranged ability",
                    attack = 5,
                    }
            };
    }
}
