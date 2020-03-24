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
