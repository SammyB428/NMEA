#region License
/*
Author: Samuel R. Blackburn
Internet: wfc@pobox.com

"You can get credit for something or get it done, but not both."
Dr. Richard Garwin

The MIT License (MIT)

Copyright (c) 1996-2019 Sam Blackburn

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

/* SPDX-License-Identifier: MIT */

#endregion

namespace NMEA
{
   public class TransducerData
   {
      public TransducerType TransducerType;
      public double MeasurementData;
      public string MeasurementUnits;
      public string TransducerName;

      public TransducerData()
      {
         Empty();
      }

      public void Empty()
      {
         TransducerType = TransducerType.Unknown;
         MeasurementData = 0.0D;
         MeasurementUnits = "";
         TransducerName = "";
      }

      public void Parse(int first_field_number, Sentence sentence)
      {
         Empty();
         TransducerType = sentence.TransducerType(first_field_number);
         MeasurementData = sentence.Double(first_field_number + 1);
         MeasurementUnits = sentence.Field(first_field_number + 2);
         TransducerName = sentence.Field(first_field_number + 3);
      }
   }

   public class XDR : Response
   {
      public System.Collections.ArrayList Transducers;

      public XDR() : base("XDR")
      {
         Transducers = new System.Collections.ArrayList();
         Empty();
      }

      public override void Empty()
      {
         base.Empty();

         Transducers.Clear();
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** XDR - Transducer Reading
         **
         **        1 2   3 4			    n
         **        | |   | |            |
         ** $--XDR,a,x.x,a,c--c, ..... *hh<CR><LF>
         **
         ** Field Number: 
         **  1) Transducer Type
         **  2) Measurement Data
         **  3) Units of measurement
         **  4) Name of transducer
         **  x) More of the same
         **  n) Checksum
         */

         /*
         ** First we check the checksum...
         */

         if (sentence.IsChecksumBad() == NMEA.Boolean.True)
         {
            Empty();
            return (false);
         }

         int field_number = 1;

         while( sentence.Field( field_number + 1 ) != "" )
         {
            TransducerData data = new TransducerData();

            data.Parse(field_number, sentence);
            Transducers.Add(data);

            field_number += 4;
         }

         return (true);
      }
   }
}
