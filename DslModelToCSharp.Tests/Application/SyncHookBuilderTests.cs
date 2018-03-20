﻿using System.IO;
using DslModelToCSharp.Application;
using FileToDslModel;
using FileToDslModel.Lexer;
using FileToDslModel.ParseAutomat;
using NUnit.Framework;

namespace DslModelToCSharp.Tests.Application
{
    [TestFixture]
    public class SyncHookBuilderTests : TestBase
    {
        [Test]
        public void BuildTests()
        {
            var commandHandlerBuilder = new SynchronousHookBuilder(ApplicationNameSpace);

            using (var reader = new StreamReader("Schema.wsb"))
            {
                var content = reader.ReadToEnd();
                var domainTree = new DslParser(new Tokenizer(), new Parser()).Parse(content);
                foreach (var hook in domainTree.SynchronousDomainHooks)
                {
                    var codeNamespace = commandHandlerBuilder.Build(hook);
                    new FileWriter(ApplicationBasePath).WriteToFile($"{hook.Name}Hook", hook.ClassType + "s/Hooks/", codeNamespace);
                }
            }

            new PrivateSetPropertyHackCleaner().ReplaceHackPropertyNames(ApplicationBasePath);

            Assert.AreEqual(File.ReadAllText("../../../ApplicationExpected/Generated/Users/Hooks/SendPasswordMailHook.g.cs"),
                File.ReadAllText("Application/Users/Hooks/SendPasswordMailHook.g.cs"));
        }

        [Test]
        public void BuildReplacementClass()
        {
            var commandHandlerBuilder = new SynchronousHookBuilder(ApplicationNameSpace);

            using (var reader = new StreamReader("Schema.wsb"))
            {
                var content = reader.ReadToEnd();
                var domainTree = new DslParser(new Tokenizer(), new Parser()).Parse(content);
                foreach (var hook in domainTree.SynchronousDomainHooks)
                {
                    var codeNamespace = commandHandlerBuilder.BuildReplacementClass(hook);
                    new FileWriter(ApplicationBasePath).WriteToFile($"{hook.Name}Hook", hook.ClassType + "s/Hooks/", codeNamespace, false);
                }
            }

            new PrivateSetPropertyHackCleaner().ReplaceHackPropertyNames(ApplicationBasePath);

            Assert.AreEqual(File.ReadAllText("../../../ApplicationExpected/Generated/Users/Hooks/SendPasswordMailHook.cs"),
                File.ReadAllText("Application/Users/Hooks/SendPasswordMailHook.cs"));
        }


    }
}