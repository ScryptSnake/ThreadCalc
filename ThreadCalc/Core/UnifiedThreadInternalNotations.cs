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
    /// Internal threads only.
    /// </summary>
    public static class UnifiedThreadInternalNotations
    {
        public const string MAJOR_DIAMETER = "D";
        public const string BASIC_MAJOR_DIAMETER = "Dbsc";
        public const string MAX_MAJOR_DIAMETER = "Dmax";
        public const string MIN_MAJOR_DIAMETER = "Dmin";
        public const string MINOR_DIAMETER = "D1";
        public const string BASIC_MINOR_DIAMETER = "D1bsc";
        public const string MAX_MINOR_DIAMETER = "D1max";
        public const string MIN_MINOR_DIAMETER = "D2min";
        public const string PITCH_DIAMETER = "D2";
        public const string BASIC_PITCH_DIAMETER = "D2bsc";
        public const string MAX_PITCH_DIAMETER = "D2max";
        public const string MIN_PITCH_DIAMETER = "D2min";
        public const string ROUNDED_ROOT_MAJOR_DIAMETER = "D3";
        public const string ALLOWANCE = "EI";
        public const string COMPLETE_THREAD_LENGTH = "LTD";
        public const string THREAD_ENGAGEMENT_LENGTH = "LE";
    }



}
