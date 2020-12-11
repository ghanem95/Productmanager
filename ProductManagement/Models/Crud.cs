using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductManagement.Models
{
 public   abstract class Crud
    {
        string connect;

        public Crud()
        {
            
        }
        public abstract void Add();
        public abstract void Affiche(int id);
        public abstract void Update();
        public abstract string Delete(int id);
    }
}