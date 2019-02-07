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
   public class OSD : Response
   {
      public double HeadingDegreesTrue;
      public Boolean IsHeadingValid;
      public double VesselCourseDegreesTrue;
      public Reference VesselCourseReference;
      public double VesselSpeed;
      public Reference VesselSpeedReference;
      public double VesselSetDegreesTrue;
      public double VesselDriftSpeed;
      public string VesselDriftSpeedUnits;

      public OSD()
      {
         Mnemonic = "OSD";
      }

      public override void Empty()
      {
         base.Empty();

         HeadingDegreesTrue = 0.0D;
         IsHeadingValid = Boolean.Unknown;
         VesselCourseDegreesTrue = 0.0D;
         VesselCourseReference = Reference.Unknown;
         VesselSpeed = 0.0D;
         VesselSpeedReference = Reference.Unknown;
         VesselSetDegreesTrue = 0.0D;
         VesselDriftSpeed = 0.0D;
         VesselDriftSpeedUnits = "";

         Mnemonic = "OSD";
      }

      public override bool Parse(Sentence sentence)
      {
         /*
         ** OSD - Own Ship Data
         **
         **        1   2 3   4 5   6 7   8   9 10
         **        |   | |   | |   | |   |   | |
         ** $--OSD,x.x,A,x.x,a,x.x,a,x.x,x.x,a*hh<CR><LF>
         **
         **  1) Heading, degrees true
         **  2) Status, A = Data Valid
         **  3) Vessel Course, degrees True
         **  4) Course Reference
         **  5) Vessel Speed
         **  6) Speed Reference
         **  7) Vessel Set, degrees True
         **  8) Vessel drift (speed)
         **  9) Speed Units
         ** 10) Checksum
         */

         /*
         ** First we check the checksum...
         */

         if (sentence.IsChecksumBad() == NMEA.Boolean.True)
         {
            Empty();
            return (false);
         }

         HeadingDegreesTrue = sentence.Double(1);
         IsHeadingValid = sentence.Boolean(2);
         VesselCourseDegreesTrue = sentence.Double(3);
         VesselCourseReference = sentence.Reference(4);
         VesselSpeed = sentence.Double(5);
         VesselSpeedReference = sentence.Reference(6);
         VesselSetDegreesTrue = sentence.Double(7);
         VesselDriftSpeed = sentence.Double(8);
         VesselDriftSpeedUnits = sentence.Field(9);

         return (true);
      }
   }
}
