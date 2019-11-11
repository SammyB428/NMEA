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
   public class RSD : Response
   {
      public RadarData Data1;
      public RadarData Data2;
      public double CursorRangeFromOwnShip;
      public double CursorBearingDegreesClockwiseFromZero;
      public double RangeScale;
      public string RangeUnits;
      public Rotation DisplayRotation;

      public RSD() : base("RSD")
      {
         Data1 = new RadarData();
         Data2 = new RadarData();
         Empty();
      }

      public override void Empty()
      {
         base.Empty();

         Data1.Empty();
         Data2.Empty();
         CursorRangeFromOwnShip = 0.0D;
         CursorBearingDegreesClockwiseFromZero = 0.0D;
         RangeScale = 0.0D;
         RangeUnits = string.Empty;
         DisplayRotation = Rotation.Unknown;
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** RSD - RADAR System Data
         **                                                        14
         **        1   2   3   4   5   6   7   8   9   10  11 12 13|
         **        |   |   |   |   |   |   |   |   |   |   |   | | |
         ** $--RSD,x.x,x.x,x.x,x.x,x.x,x.x,x.x,x.x,x.x,x.x,x.x,a,a*hh<CR><LF>
         **
         ** 14) Checksum
         */

         /*
         ** First we check the checksum...
         */

         if (sentence.IsChecksumBad() == NMEA.Boolean.True)
         {
            Empty();
            return (false);
         }

         Data1.Parse(1, sentence);
         Data2.Parse(5, sentence);
         CursorRangeFromOwnShip = sentence.Double(9);
         CursorBearingDegreesClockwiseFromZero = sentence.Double(10);
         RangeScale = sentence.Double(11);
         RangeUnits = sentence.Field(12);

         int temp_integer = sentence.Integer(13);

         switch (temp_integer)
         {
            case 'C':

               DisplayRotation = Rotation.CourseUp;
               break;

            case 'H':

               DisplayRotation = Rotation.HeadUp;
               break;

            case 'N':

               DisplayRotation = Rotation.NorthUp;
               break;

            default:

               DisplayRotation = Rotation.Unknown;
               break;
         }

         return (true);
      }
   }
}
