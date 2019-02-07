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

      public XDR()
      {
         Transducers = new System.Collections.ArrayList();
         Mnemonic = "XDR";
      }

      public override void Empty()
      {
         base.Empty();

         Transducers.Clear();

         Mnemonic = "XDR";
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
