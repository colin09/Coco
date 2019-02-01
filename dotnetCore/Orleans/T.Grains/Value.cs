using System;
using System.Threading.Tasks;
using Orleans;
using T.IGrains;

namespace T.Grains {
    public class Value : Grain, IValue {
        public Task<string> Say (string word) {
            System.Console.WriteLine ($"-------> say nothing, {word} , {DateTime.Now}");
            return Task.FromResult<string> ("done");
        }
    }
}