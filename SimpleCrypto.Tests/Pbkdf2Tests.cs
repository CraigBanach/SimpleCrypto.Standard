﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleCrypto.Standard;

namespace SimpleCrypto.Tests
{
    [TestClass]
    public class Pkbdf2Tests
    {
        #region [ Compute ]

        
        [TestMethod]
        public void Compute_Ok()
        {
            var pbkdf2 = new Pbkdf2 {PlainText = "Test", Salt = "100000.Random"};

            var result = pbkdf2.Compute();
            
            Assert.IsNotNull(result);
            Assert.IsNotNull(pbkdf2.Salt);
            Assert.IsNotNull(pbkdf2.PlainText);
            Assert.IsNotNull(pbkdf2.HashedText);
            
            Assert.AreEqual(result, pbkdf2.HashedText);
        }

        /// <summary>
        /// Ensures that a <see cref="InvalidOperationException"/> gets thrown when the Plaintext is empty or null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Compute_PlainTextIsEmpty()
        {
            var pbkdf2 = new Pbkdf2 {Salt = "100000.Random"};

            pbkdf2.Compute();
            
            Assert.Fail();
        }
        
        /// <summary>
        /// Ensures that Salt will be generated if not set.
        /// </summary>
        [TestMethod]
        public void Compute_SaltIsEmpty()
        {
            var pbkdf2 = new Pbkdf2 {PlainText = "Test"};

            var result = pbkdf2.Compute();
            
            Assert.IsNotNull(result);
            Assert.IsNotNull(pbkdf2.Salt);
            Assert.IsNotNull(pbkdf2.PlainText);
            Assert.IsNotNull(pbkdf2.HashedText);
            
            Assert.AreEqual(result, pbkdf2.HashedText);
        }
        /// <summary>
        /// Ensures a <see cref="FormatException"/> gets thrown when the format of the given salt is invalid.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Compute_ExtractSaltException()
        {
            var pbkdf2 = new Pbkdf2 {PlainText = "Test", Salt = "100A00.Random"};

            pbkdf2.Compute();
            
            Assert.Fail();
        }
        
        [DataTestMethod]
        [DataRow("passwordHash","passwordHash")]
        [DataRow("","")]
        [DataRow(null,null)]
        [DataRow("$","$")]
        [DataRow("汉字", "汉字")]
        public void Compare_ShouldReturnTrue(string passwordHash1, string passwordHash2)
        {
            var pbkdf2 = new Pbkdf2();

            var result = pbkdf2.Compare(passwordHash1, passwordHash2);

            Assert.IsTrue(result);
        }

        [DataTestMethod]
        [DataRow("passwordHash1", "passwordHash2")]
        [DataRow("Hello", "Hello World")]
        [DataRow("PasswordHash", null)]
        [DataRow("PasswordHash", "")]
        [DataRow("汉字", "$")]
        public void Compare_ShouldReturnFalse(string passwordHash1, string passwordHash2)
        {
            var pbkdf2 = new Pbkdf2();

            var result = pbkdf2.Compare(passwordHash1, passwordHash2);

            Assert.IsFalse(result);
        }

        #endregion
    }
}