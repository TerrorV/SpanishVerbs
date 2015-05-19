using System;
namespace SpanishVerbs
{
    interface IProviderBase
    {
        Verb GetConjugation(string verbString);
    }
}
