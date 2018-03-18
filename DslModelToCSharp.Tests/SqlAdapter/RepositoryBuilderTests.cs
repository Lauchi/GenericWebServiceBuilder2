﻿using System.IO;
using DslModelToCSharp.SqlAdapter;
using FileToDslModel;
using FileToDslModel.Lexer;
using FileToDslModel.ParseAutomat;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DslModelToCSharp.Tests.SqlAdapter
{
    [TestClass]
    public class RepositoryBuilderTests : TestBase
    {
        [TestMethod]
        public void Write()
        {
            var storeBuilder = new RepositoryBuilder(SqlAdpaterNameSpace);

            using (var reader = new StreamReader("Schema.wsb"))
            {
                var content = reader.ReadToEnd();
                var domainTree = new DslParser(new Tokenizer(), new Parser()).Parse(content);

                foreach (var domainTreeClass in domainTree.Classes)
                {
                    var eventStore = storeBuilder.Build(domainTreeClass);
                    new FileWriter(SqlAdpaterNameSpace).WriteToFile(eventStore.Types[0].Name, domainTreeClass.Name + "s", eventStore);
                }
                
            }

            new PrivateSetPropertyHackCleaner().ReplaceHackPropertyNames(SqlAdpaterBasePath);

            Assert.AreEqual(File.ReadAllText("../../../SqlAdapterExpected/Generated/Users/UserRepository.g.cs"),
                File.ReadAllText("SqlAdapter/Users/UserRepository.g.cs"));
            Assert.AreEqual(File.ReadAllText("../../../SqlAdapterExpected/Generated/Posts/PostRepository.g.cs"),
                File.ReadAllText("SqlAdapter/Posts/PostRepository.g.cs"));
        }
    }
}