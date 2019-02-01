using System;
using System.Threading.Tasks;
using Orleans;

namespace T.IGrains {
    public interface IValue : IGrainWithIntegerKey {

        Task<string> Say (string word);

    }
}