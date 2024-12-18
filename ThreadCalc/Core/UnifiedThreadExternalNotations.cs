using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using ThreadCalc.Types;

namespace ThreadCalc.Core
{
    /// <summary>
    /// Holds static notation symbols for specifications in accordance with ASME B1.1-2019 (pg 146)
    /// External threads only.
    /// </summary>
    public static class UnifiedThreadExternalNotations
    {
        public const string MAJOR_DIAMETER = "d";
        public const string BASIC_MAJOR_DIAMETER = "dbsc";
        public const string MAX_MAJOR_DIAMETER = "dmax";
        public const string MIN_MAJOR_DIAMETER = "dmin";
        public const string MINOR_DIAMETER = "d3";
        public const string MAX_MINOR_DIAMETER = "d3max";
        public const string MIN_MINOR_DIAMETER = "d1min";
        public const string PITCH_DIAMETER = "d2";
        public const string BASIC_PITCH_DIAMETER = "d2bsc";
        public const string MAX_PITCH_DIAMETER = "d2max";
        public const string MIN_PITCH_DIAMETER = "d2min";
        public const string ROUNDED_ROOT_MINOR_DIAMETER = "d3min";
        public const string ALLOWANCE = "es";
        public const string COMPLETE_THREAD_LENGTH = "be";
        public const string THREAD_ENGAGEMENT_LENGTH = "LE";
    }



}
