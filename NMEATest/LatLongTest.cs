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
#endregion

namespace NMEATest
{
    /// <summary>
    ///This is a test class for LatLongTest and is intended
    ///to contain all LatLongTest Unit Tests
    ///</summary>
   [TestClass()]
   public class LatLongTest
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
      ///A test for Parse
      ///</summary>
      [TestMethod()]
      public void ParseTest()
      {
         var target = new LatLong(); // TODO: Initialize to an appropriate value
         int LatitudePostionFieldNumber = 0; // TODO: Initialize to an appropriate value
         int NorthingFieldNumber = 0; // TODO: Initialize to an appropriate value
         int LongitudePositionFieldNumber = 0; // TODO: Initialize to an appropriate value
         int EastingFieldNumber = 0; // TODO: Initialize to an appropriate value
         Sentence LineToParse = null; // TODO: Initialize to an appropriate value
         bool expected = false; // TODO: Initialize to an appropriate value
         bool actual = target.Parse(LatitudePostionFieldNumber, NorthingFieldNumber, LongitudePositionFieldNumber, EastingFieldNumber, LineToParse);
         Assert.AreEqual(expected, actual);
         Assert.Inconclusive("Verify the correctness of this test method.");
      }

      /// <summary>
      ///A test for Empty
      ///</summary>
      [TestMethod()]
      public void EmptyTest()
      {
         var target = new LatLong(); // TODO: Initialize to an appropriate value
         target.Empty();
         Assert.Inconclusive("A method that does not return a value cannot be verified.");
      }

      /// <summary>
      ///A test for LatLong Constructor
      ///</summary>
      [TestMethod()]
      public void LatLongConstructorTest()
      {
         var target = new LatLong();
         Assert.Inconclusive("TODO: Implement code to verify target");
      }
   }
}
