using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ediens
{
    internal class Food
    {
        public string Name { get; set; }
        public string PortionType { get; set; }
        public float KcalPerPortion { get; set; }
        public int Quantity { get; set; }

        public Food(string name, string portionType, float kcalPerPortion, int quantity)
        {
            Name = name;
            PortionType = portionType;
            KcalPerPortion = kcalPerPortion;
            Quantity = quantity;
        }

        public void ChangeName(string newName)
        {
            Name = newName;
        }

        public void ChangePortionType(string newPortionType)
        {
            PortionType = newPortionType;
        }

        public void ChangeKcalPerPortion(float newKcalPerPortion)
        {
            KcalPerPortion = newKcalPerPortion;
        }

        public void Delete()
        {
        }
    }
}
