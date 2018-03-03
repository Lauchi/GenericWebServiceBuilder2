using GenericWebServiceBuilder.FileToDSL.Lexer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileToDslTests
{
    [TestClass]
    public class TokenizerTests
    {
        [TestMethod]
        public void Tokenize_Mixed()
        {
            var tokenizer = new Tokenizer();
            var tokens = tokenizer.Tokenize(@"DomainClass User{
	                                Id: Guid
	                                
                                    UpdateName(Name: String): {
		                                UserId: Guid
		                                Age: Int32
	                                }

	                                Posts: [Post]
                                }");
            Assert.AreEqual(28, tokens.Count);

            Assert.AreEqual(TokenType.DomainClass, tokens[0].TokenType);
            Assert.AreEqual(TokenType.Value, tokens[1].TokenType);
            Assert.AreEqual(TokenType.ObjectBracketOpen, tokens[2].TokenType);
            Assert.AreEqual(TokenType.Value, tokens[3].TokenType);
            Assert.AreEqual(TokenType.ParameterBracketOpen, tokens[7].TokenType);
            Assert.AreEqual("UpdateName", tokens[6].Value);
            Assert.AreEqual("Name", tokens[8].Value);
            Assert.AreEqual("String", tokens[10].Value);
        }

        [TestMethod]
        public void Tokenize_DomainClass()
        {
            var tokenizer = new Tokenizer();
            var tokens = tokenizer.Tokenize(@"DomainClass User{
                                }");
            Assert.AreEqual(5, tokens.Count);

            Assert.AreEqual(TokenType.DomainClass, tokens[0].TokenType);
            Assert.AreEqual(TokenType.Value, tokens[1].TokenType);
            Assert.AreEqual("User", tokens[1].Value);
            Assert.AreEqual(TokenType.ObjectBracketOpen, tokens[2].TokenType);
            Assert.AreEqual(TokenType.ObjectBracketClose, tokens[3].TokenType);
        }

        [TestMethod]
        public void Tokenize_DomainClass_Property()
        {
            var tokenizer = new Tokenizer();
            var tokens = tokenizer.Tokenize(@"DomainClass User{
                                                Id: Guid
                                            }");
            Assert.AreEqual(8, tokens.Count);

            Assert.AreEqual("Id", tokens[3].Value);
            Assert.AreEqual(TokenType.TypeDef, tokens[4].TokenType);
            Assert.AreEqual("Guid", tokens[5].Value);
        }

        [TestMethod]
        public void Tokenize_DomainClass_Method()
        {
            var tokenizer = new Tokenizer();
            var tokens = tokenizer.Tokenize(@"DomainClass User{
                                                UpdateName(Name: String): {
		                                            UserId: Guid
		                                            Age: Int32
	                                            }
                                            }");
            Assert.AreEqual(20, tokens.Count);

            Assert.AreEqual("UpdateName", tokens[3].Value);
            Assert.AreEqual(TokenType.ParameterBracketOpen, tokens[4].TokenType);
            Assert.AreEqual(TokenType.TypeDef, tokens[6].TokenType);
            Assert.AreEqual(TokenType.TypeDef, tokens[9].TokenType);
            Assert.AreEqual(TokenType.ObjectBracketOpen, tokens[10].TokenType);
            Assert.AreEqual("UserId", tokens[11].Value);
            Assert.AreEqual(TokenType.TypeDef, tokens[12].TokenType);
            Assert.AreEqual("Guid", tokens[13].Value);
        }
    }
}