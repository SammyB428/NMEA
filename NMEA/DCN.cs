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
   public class LineOfPosition
   {
      public string ZoneId;
      public double Position;
      public NMEA.Boolean MasterLine;

      public LineOfPosition()
      {
      }

      public void Empty()
      {
         ZoneId = "";
         Position = 0.0D;
         MasterLine = NMEA.Boolean.Unknown;
      }

      public void Parse( int first_field_number, Sentence sentence )
      {
         ZoneId     = sentence.Field(first_field_number );
         Position   = sentence.Double(first_field_number + 1);
         MasterLine = sentence.Boolean( first_field_number + 2 );
      }
   }

   public class DCN : Response
   {
      public int DeccaChainID;
      public LineOfPosition Red;
      public LineOfPosition Green;
      public LineOfPosition Purple;
      public NMEA.Boolean RedLineNavigationUse;
      public NMEA.Boolean GreenLineNavigationUse;
      public NMEA.Boolean PurpleLineNavigationUse;
      public double PositionUncertaintyNauticalMiles;
      public NMEA.FixDataBasis Basis;

      public DCN() : base("DCN")
      {
          Red = new LineOfPosition();
          Green = new LineOfPosition();
          Purple = new LineOfPosition();
          Empty();
      }

      public override void Empty()
      {
         base.Empty();

         DeccaChainID = 0;
         Red.Empty();
         Green.Empty();
         Purple.Empty();
         RedLineNavigationUse = Boolean.Unknown;
         GreenLineNavigationUse = Boolean.Unknown;
         PurpleLineNavigationUse = Boolean.Unknown;
         PositionUncertaintyNauticalMiles = 0.0D;
         Basis = FixDataBasis.Unknown;
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** DCN - Decca Position
         **                                      11  13      16
         **        1  2  3   4 5  6   7 8  9   10| 12| 14  15| 17
         **        |  |  |   | |  |   | |  |   | | | | |   | | |
         ** $--DCN,xx,cc,x.x,A,cc,x.x,A,cc,x.x,A,A,A,A,x.x,N,x*hh<CR><LF>
         **
         **  1) Decca chain identifier
         **  2) Red Zone Identifier
         **  3) Red Line Of Position
         **  4) Red Master Line Status
         **  5) Green Zone Identifier
         **  6) Green Line Of Position
         **  7) Green Master Line Status
         **  8) Purple Zone Identifier
         **  9) Purple Line Of Position
         ** 10) Purple Master Line Status
         ** 11) Red Line Navigation Use
         ** 12) Green Line Navigation Use
         ** 13) Purple Line Navigation Use
         ** 14) Position Uncertainity
         ** 15) N = Nautical Miles
         ** 16) Fix Data Basis
         **     1 = Normal Pattern
         **     2 = Lane Identification Pattern
         **     3 = Lane Identification Transmissions
         ** 17) Checksum
         */

         /*
         ** First we check the checksum...
         */

         if (sentence.IsChecksumBad() == Boolean.True)
         {
            Empty();
            return (false);
         }

         DeccaChainID = sentence.Integer(1);
         Red.Parse(2, sentence);
         Green.Parse(5, sentence);
         Purple.Parse(8, sentence);
         RedLineNavigationUse = sentence.Boolean(11);
         GreenLineNavigationUse = sentence.Boolean(12);
         PurpleLineNavigationUse = sentence.Boolean(13);
         PositionUncertaintyNauticalMiles = sentence.Double(14);

         int temp_integer = sentence.Integer(16);

         switch (temp_integer)
         {
            case 1:

               Basis = FixDataBasis.NormalPattern;
               break;

            case 2:

               Basis = FixDataBasis.LaneIdentificationPattern;
               break;

            case 3:

               Basis = FixDataBasis.LaneIdentificationTransmissions;
               break;

            default:

               Basis = FixDataBasis.Unknown;
               break;
         }

         return (true);
      }
   }
}
