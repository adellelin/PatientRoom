/* Copyright (c) 2017 ExT (V.Sigalkin) */

namespace extOSC.Core.Network
{
    public abstract class OSCTransmitterBackend
    {
        #region Public Vars

        public abstract bool IsAvaible { get; }

        #endregion

        #region Public Methods

        public abstract void Connect(string remoteHost, int remotePort);

        public abstract void RefreshConnection(string remoteHost, int remotePort);

        public abstract void Close();

        public abstract void Send(byte[] data);

        #endregion
    }
}