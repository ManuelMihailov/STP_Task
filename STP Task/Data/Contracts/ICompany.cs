using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Contracts
{
    public interface ICompany
    {
        int Id { get; set; }
        string Name { get; set; }
        DateTime CreationDate { get; set; }
        ICollection<Office> Offices { get; set; }
        byte Removed { get; set; }
    }
}
