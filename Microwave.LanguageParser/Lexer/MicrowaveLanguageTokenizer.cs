﻿using System;
using System.Collections.Generic;

namespace Microwave.LanguageParser.Lexer
{
    public class MicrowaveLanguageTokenizer : ITokenizer
    {
        private readonly List<TokenDefinition> _tokenDefinitions;

        public MicrowaveLanguageTokenizer()
        {
            _tokenDefinitions = new List<TokenDefinition>
            {
                new TokenDefinition(TokenType.ObjectBracketOpen, "^\\{"),
                new TokenDefinition(TokenType.ObjectBracketClose, "^\\}"),

                new TokenDefinition(TokenType.ParameterBracketOpen, "^\\("),
                new TokenDefinition(TokenType.ParameterBracketClose, "^\\)"),

                new TokenDefinition(TokenType.ListBracketOpen, "^\\["),
                new TokenDefinition(TokenType.ListBracketClose, "^\\]"),

                new TokenDefinition(TokenType.TypeDefSeparator, "^:"),
                new TokenDefinition(TokenType.ParamSeparator, "^,"),
                new TokenDefinition(TokenType.DomainHookEventDefinition, "^\\w+\\.\\w+"),

                new TokenDefinition(TokenType.CreateMethod, "^Create"),
                new TokenDefinition(TokenType.LoadToken, "^@Load"),
                new TokenDefinition(TokenType.DomainClass, "^DomainClass"),
                new TokenDefinition(TokenType.SynchronouslyToken, "^synchronously"),
                new TokenDefinition(TokenType.AsynchronouslyToken, "^asynchronously"),
                new TokenDefinition(TokenType.OnChild, "^onChild"),
                new TokenDefinition(TokenType.DomainHookOn, "^on"),

                new TokenDefinition(TokenType.Value, "^\\w+")
            };
        }

        public List<DslToken> Tokenize(string lqlText)
        {
            var tokens = new List<DslToken>();
            var remainingText = lqlText;

            var lineCounter = 1;
            while (!string.IsNullOrWhiteSpace(remainingText))
            {
                if (remainingText.StartsWith(Environment.NewLine))
                    lineCounter++;
                var match = FindMatch(remainingText);
                if (match.IsMatch)
                {
                    tokens.Add(new DslToken(match.TokenType, match.Value, lineCounter));
                    remainingText = match.RemainingText;
                }
                else
                {
                    remainingText = remainingText.Substring(1);
                }
            }

            return tokens;
        }

        private TokenMatch FindMatch(string lqlText)
        {
            foreach (var tokenDefinition in _tokenDefinitions)
            {
                var match = tokenDefinition.Match(lqlText);
                if (match.IsMatch)
                    return match;
            }

            return new TokenMatch {IsMatch = false};
        }
    }
}