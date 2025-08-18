using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IRecord
{
    public void Do((object, object) collision); 

    public bool IsTarget((object, object) collision);
}