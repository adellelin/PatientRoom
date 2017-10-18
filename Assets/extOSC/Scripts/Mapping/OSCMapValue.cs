/* Copyright (c) 2017 ExT (V.Sigalkin) */

using System;

namespace extOSC.Mapping
{
    [Serializable]
    public class OSCMapValue
    {
        #region Public Vars

        public float InputMin;

        public float InputMax = 1;

        public float OutputMin;

        public float OutputMax = 1;

        public bool Clamp = true;

        public OSCValueType ValueType
        {
            get { return OSCValueType.Float; }
        }

        #endregion

        #region Public Methods

        public void Map(OSCValue value)
        {
            value.FloatValue = OSCUtilities.Map(value.FloatValue, InputMin, InputMax, OutputMin, OutputMax, Clamp);
        }

        #endregion
    }
}