using System;
namespace SpanishVerbs
{
    public interface IConjugationProvider
    {
        Verb GetConjugation(string rawData);
    }
}
