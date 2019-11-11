#region License
/*
** Author: Samuel R. Blackburn
** Internet: wfc@pobox.com
**
** "You can get credit for something or get it done, but not both."
** Dr. Richard Garwin
**
** A modified BSD License follows. This license is based on the
** 3-clause license ("New BSD License" or "Modified BSD License") found at
** http://en.wikipedia.org/wiki/BSD_license
** The difference is the dropping of the "advertising" clause. You do not,
** I repeat, DO NOT have to include this copyright notice in binary distributions.
**
** Copyright (c) 2011, Samuel R. Blackburn
** All rights reserved.
**
** Redistribution and use in source and binary forms, with or without
** modification, are permitted provided that the following conditions are met:
**     * Redistributions of source code must retain the above copyright
**       notice, this list of conditions and the following disclaimer.
**     * Neither the name of the <organization> nor the
**       names of its contributors may be used to endorse or promote products
**       derived from this software without specific prior written permission.
**
** THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
** ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
** WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
** DISCLAIMED. IN NO EVENT SHALL SAMUEL R. BLACKBURN BE LIABLE FOR ANY
** DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
** (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
** LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
** ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
** (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
** SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
**
*/
#endregion

#region Using
using NMEA;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
#endregion

namespace NMEATest
{
    /// <summary>
    ///This is a test class for SentenceTest and is intended
    ///to contain all SentenceTest Unit Tests
    ///</summary>
   [TestClass()]
   public class SentenceTest
   {
      private TestContext testContextInstance;

      /// <summary>
      ///Gets or sets the test context which provides
      ///information about and functionality for the current test run.
      ///</summary>
      public TestContext TestContext
      {
         get
         {
            return testContextInstance;
         }
         set
         {
            testContextInstance = value;
         }
      }

      #region Additional test attributes
      // 
      //You can use the following additional attributes as you write your tests:
      //
      //Use ClassInitialize to run code before running the first test in the class
      //[ClassInitialize()]
      //public static void MyClassInitialize(TestContext testContext)
      //{
      //}
      //
      //Use ClassCleanup to run code after all tests in a class have run
      //[ClassCleanup()]
      //public static void MyClassCleanup()
      //{
      //}
      //
      //Use TestInitialize to run code before running each test
      //[TestInitialize()]
      //public void MyTestInitialize()
      //{
      //}
      //
      //Use TestCleanup to run code after each test has run
      //[TestCleanup()]
      //public void MyTestCleanup()
      //{
      //}
      //
      #endregion


      /// <summary>
      ///A test for TransducerType
      ///</summary>
      [TestMethod()]
      public void TransducerTypeTest()
      {
         var target = new Sentence(); // TODO: Initialize to an appropriate value
         int field_number = 0; // TODO: Initialize to an appropriate value
         var expected = new TransducerType(); // TODO: Initialize to an appropriate value
         var actual = target.TransducerType(field_number);
         Assert.AreEqual(expected, actual);
         Assert.Inconclusive("Verify the correctness of this test method.");
      }

      /// <summary>
      /// A test for Time
      /// </summary>
      [TestMethod()]
      public void TimeTest()
      {
         var target = new Sentence(); // TODO: Initialize to an appropriate value
         int field_number = 0; // TODO: Initialize to an appropriate value
         var expected = new DateTime(); // TODO: Initialize to an appropriate value
         var actual = target.Time(field_number);
         Assert.AreEqual(expected, actual);
         Assert.Inconclusive("Verify the correctness of this test method.");
      }

      /// <summary>
      /// A test for Reference
      /// </summary>
      [TestMethod()]
      public void ReferenceTest()
      {
         var target = new Sentence(); // TODO: Initialize to an appropriate value
         int field_number = 0; // TODO: Initialize to an appropriate value
         var expected = new Reference(); // TODO: Initialize to an appropriate value
         var actual = target.Reference(field_number);
         Assert.AreEqual(expected, actual);
         Assert.Inconclusive("Verify the correctness of this test method.");
      }

      /// <summary>
      ///A test for NorthOrSouth
      ///</summary>
      [TestMethod()]
      public void NorthOrSouthTest()
      {
         var target = new Sentence(); // TODO: Initialize to an appropriate value
         int field_number = 0; // TODO: Initialize to an appropriate value
         var expected = new NorthOrSouth(); // TODO: Initialize to an appropriate value
         var actual = target.NorthOrSouth(field_number);
         Assert.AreEqual(expected, actual);
         Assert.Inconclusive("Verify the correctness of this test method.");
      }

      /// <summary>
      /// A test for LeftOrRight
      /// </summary>
      [TestMethod()]
      public void LeftOrRightTest()
      {
         var target = new Sentence(); // TODO: Initialize to an appropriate value
         int field_number = 0; // TODO: Initialize to an appropriate value
         var expected = new LeftOrRight(); // TODO: Initialize to an appropriate value
         var actual = target.LeftOrRight(field_number);
         Assert.AreEqual(expected, actual);
         Assert.Inconclusive("Verify the correctness of this test method.");
      }

      /// <summary>
      /// A test for IsGood
      /// </summary>
      [TestMethod()]
      public void IsGoodTest()
      {
         var target = new Sentence(); // TODO: Initialize to an appropriate value
         bool expected = false; // TODO: Initialize to an appropriate value
         var actual = target.IsGood();
         Assert.AreEqual(expected, actual);
         Assert.Inconclusive("Verify the correctness of this test method.");
      }

      /// <summary>
      ///A test for IsChecksumBad
      ///</summary>
      [TestMethod()]
      public void IsChecksumBadTest()
      {
         var target = new Sentence(); // TODO: Initialize to an appropriate value
         var expected = new NMEA.Boolean(); // TODO: Initialize to an appropriate value
         var actual = target.IsChecksumBad();
         Assert.AreEqual(expected, actual);
         Assert.Inconclusive("Verify the correctness of this test method.");
      }

      /// <summary>
      /// A test for Integer
      /// </summary>
      [TestMethod()]
      public void IntegerTest()
      {
         var target = new Sentence(); // TODO: Initialize to an appropriate value
         int field_number = 0; // TODO: Initialize to an appropriate value
         int expected = 0; // TODO: Initialize to an appropriate value
         int actual = target.Integer(field_number);
         Assert.AreEqual(expected, actual);
         Assert.Inconclusive("Verify the correctness of this test method.");
      }

      /// <summary>
      ///A test for HexValue
      ///</summary>
      [TestMethod()]
      public void HexValueTest()
      {
         var target = new Sentence(); // TODO: Initialize to an appropriate value
         var hex_string = string.Empty; // TODO: Initialize to an appropriate value
         int expected = 0; // TODO: Initialize to an appropriate value
         int actual = target.HexValue(hex_string);
         Assert.AreEqual(expected, actual);
         Assert.Inconclusive("Verify the correctness of this test method.");
      }

      /// <summary>
      /// A test for GetNumberOfDataFields
      /// </summary>
      [TestMethod()]
      public void GetNumberOfDataFieldsTest()
      {
         var target = new Sentence(); // TODO: Initialize to an appropriate value
         int expected = 0; // TODO: Initialize to an appropriate value
         int actual = target.GetNumberOfDataFields();
         Assert.AreEqual(expected, actual);
         Assert.Inconclusive("Verify the correctness of this test method.");
      }

      /// <summary>
      /// A test for Field
      /// </summary>
      [TestMethod()]
      public void FieldTest()
      {
         var target = new Sentence(); // TODO: Initialize to an appropriate value
         int desired_field_number = 0; // TODO: Initialize to an appropriate value
         var expected = string.Empty; // TODO: Initialize to an appropriate value
         var actual = target.Field(desired_field_number);
         Assert.AreEqual(expected, actual);
         Assert.Inconclusive("Verify the correctness of this test method.");
      }

      /// <summary>
      /// A test for FAAMode
      /// </summary>
      [TestMethod()]
      public void FAAModeTest()
      {
         var target = new Sentence(); // TODO: Initialize to an appropriate value
         int field_number = 0; // TODO: Initialize to an appropriate value
         var expected = new FAAModeIndicator(); // TODO: Initialize to an appropriate value
         var actual = target.FAAMode(field_number);
         Assert.AreEqual(expected, actual);
         Assert.Inconclusive("Verify the correctness of this test method.");
      }

      /// <summary>
      /// A test for Empty
      /// </summary>
      [TestMethod()]
      public void EmptyTest()
      {
         var target = new Sentence(); // TODO: Initialize to an appropriate value
         target.Empty();
         Assert.Inconclusive("A method that does not return a value cannot be verified.");
      }

      /// <summary>
      /// A test for EastOrWest
      /// </summary>
      [TestMethod()]
      public void EastOrWestTest()
      {
         var target = new Sentence(); // TODO: Initialize to an appropriate value
         int field_number = 0; // TODO: Initialize to an appropriate value
         var expected = new EastOrWest(); // TODO: Initialize to an appropriate value
         var actual = target.EastOrWest(field_number);
         Assert.AreEqual(expected, actual);
         Assert.Inconclusive("Verify the correctness of this test method.");
      }

      /// <summary>
      ///A test for Double
      ///</summary>
      [TestMethod()]
      public void DoubleTest()
      {
         var target = new Sentence(); // TODO: Initialize to an appropriate value
         int field_number = 0; // TODO: Initialize to an appropriate value
         double expected = 0F; // TODO: Initialize to an appropriate value
         double actual = target.Double(field_number);
         Assert.AreEqual(expected, actual);
         Assert.Inconclusive("Verify the correctness of this test method.");
      }

      /// <summary>
      /// A test for ComputeChecksum
      /// </summary>
      [TestMethod()]
      public void ComputeChecksumTest()
      {
         var target = new Sentence(); // TODO: Initialize to an appropriate value
         byte expected = 0; // TODO: Initialize to an appropriate value
         byte actual = target.ComputeChecksum();
         Assert.AreEqual(expected, actual);
         Assert.Inconclusive("Verify the correctness of this test method.");
      }

      /// <summary>
      /// A test for CommunicationsMode
      /// </summary>
      [TestMethod()]
      public void CommunicationsModeTest()
      {
         var target = new Sentence(); // TODO: Initialize to an appropriate value
         int field_number = 0; // TODO: Initialize to an appropriate value
         var expected = new CommunicationsMode(); // TODO: Initialize to an appropriate value
         var actual = target.CommunicationsMode(field_number);
         Assert.AreEqual(expected, actual);
         Assert.Inconclusive("Verify the correctness of this test method.");
      }

      /// <summary>
      ///A test for ChecksumFieldNumber
      ///</summary>
      [TestMethod()]
      public void ChecksumFieldNumberTest()
      {
         var target = new Sentence(); // TODO: Initialize to an appropriate value
         int expected = 0; // TODO: Initialize to an appropriate value
         int actual = target.ChecksumFieldNumber();
         Assert.AreEqual(expected, actual);
         Assert.Inconclusive("Verify the correctness of this test method.");
      }

      /// <summary>
      /// A test for Boolean
      /// </summary>
      [TestMethod()]
      public void BooleanTest()
      {
         var target = new Sentence(); // TODO: Initialize to an appropriate value
         int field_number = 0; // TODO: Initialize to an appropriate value
         var expected = new NMEA.Boolean(); // TODO: Initialize to an appropriate value
         var actual = target.Boolean(field_number);
         Assert.AreEqual(expected, actual);
         Assert.Inconclusive("Verify the correctness of this test method.");
      }

      /// <summary>
      /// A test for Sentence Constructor
      /// </summary>
      [TestMethod()]
      public void SentenceConstructorTest()
      {
         var target = new Sentence();
         Assert.Inconclusive("TODO: Implement code to verify target");
      }
   }
}
