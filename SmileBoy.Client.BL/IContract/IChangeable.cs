using System;
using System.Collections.Generic;
using System.Text;

namespace SmileBoyClient.Core.IContract
{
    public interface IChangeable<T>
    {
        event EventHandler<T> Changed;
    }
    public interface IClearable
    {
        event EventHandler Cleared;
    }
}
