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
using System;
using System.Text;
#endregion

/*
** Things to do
** Finish documenting BEC
** Do something about ASD
** Move all fields to properties
** Possibly put the checksum checking into the Response class and have
 * all of the derived classes call if ( base.Parse() == false ) return false;
*/

namespace NMEA
{
   public enum Boolean
   {
      Unknown = 0,
      True,
      False
   };

   public enum LeftOrRight
   {
      Unknown = 0,
      Left,
      Right
   };

   public enum EastOrWest
   {
      Unknown = 0,
      East,
      West
   };

   public enum NorthOrSouth
   {
      Unknown = 0,
      North,
      South
   };


   public enum RouteType
   {
      Unknown = 0,
      Complete,
      Working
   };

   public enum Reference
   {
      Unknown = 0,
      BottomTrackingLog,
      ManuallyEntered,
      WaterReferenced,
      RadarTrackingOfFixedTarget,
      PositioningSystemGroundReference
   };

   public enum Rotation
   {
      Unknown = 0,
      CourseUp,
      HeadUp,
      NorthUp
   };

   public enum CommunicationsMode
   {
      Unknown = 0,
      F3E_G3E_SimplexTelephone          = 'd',
      F3E_G3E_DuplexTelephone           = 'e',
      J3E_Telephone                     = 'm',
      H3E_Telephone                     = 'o',
      F1B_J2B_FEC_NBDP_TelexTeleprinter = 'q',
      F1B_J2B_ARQ_NBDP_TelexTeleprinter = 's',
      F1B_J2B_ReceiveOnlyTeleprinterDSC = 'w',
      A1A_MorseTapeRecorder             = 'x',
      A1A_MorseKeyHeadset               = '{',
      F1C_F2C_F3C_FaxMachine            = '|'
   };

   public enum TargetStatus
   {
      Unknown = 0,
      Lost,
      Query,
      Tracking
   };

   public enum TransducerType
   {
      Unknown             = 0,
      AngularDisplacement = 'A',
      Temperature         = 'C',
      LinearDisplacement  = 'D',
      Frequency           = 'F',
      Humidity            = 'H',
      Force               = 'N',
      Pressure            = 'P',
      FlowRate            = 'R',
      Tachometer          = 'T',
      Volume              = 'V'
   };

   public enum FAAModeIndicator
   {
      Unknown = 0,
      Autonomous     = 'A',
      Differential   = 'D',
      Estimated      = 'E',
      Manual         = 'M',
      Simulated      = 'S',
      NotValid       = 'N',
      Invalid        = 'V',
   };

   public enum FixDataBasis
   {
      Unknown = 0,
      NormalPattern,
      LaneIdentificationPattern,
      LaneIdentificationTransmissions
   };

   public enum LoranSignalStatus
   {
      Unknown = 0,
      Valid,
      BlinkWarning,
      CycleWarning,
      SignalToNoiseRatioWarning
   };
}
