using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BaseCsv
{
    public virtual int GetId()
    {
        return 0;
    }

    public virtual void InitByBinary(BinaryReader reader)
    {

    }
}


