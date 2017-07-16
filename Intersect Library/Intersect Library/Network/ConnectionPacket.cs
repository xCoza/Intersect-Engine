﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using Intersect.Logging;

namespace Intersect.Network
{
    public abstract class ConnectionPacket : AbstractPacket
    {
        protected const int SIZE_HANDSHAKE_SECRET = 32;

        protected byte[] mHandshakeSecret;
        public byte[] HandshakeSecret
        {
            get => mHandshakeSecret;
            set => mHandshakeSecret = value;
        }

        protected readonly RSACryptoServiceProvider mRsa;

        protected ConnectionPacket(RSACryptoServiceProvider rsa, byte[] handshakeSecret)
            : base(null, PacketCode.Unknown)
        {
            mRsa = rsa ?? throw new ArgumentNullException();

            mHandshakeSecret = handshakeSecret;
        }

        protected static void DumpKey(RSAParameters parameters, bool isPublic)
        {
#if INTERSECT_DIAGNOSTIC
            Log.Diagnostic($"Exponent: {BitConverter.ToString(parameters.Exponent)}");
            Log.Diagnostic($"Modulus: {BitConverter.ToString(parameters.Modulus)}");

            if (isPublic) return;
            Log.Diagnostic($"D: {BitConverter.ToString(parameters.D)}");
            Log.Diagnostic($"DP: {BitConverter.ToString(parameters.DP)}");
            Log.Diagnostic($"DQ: {BitConverter.ToString(parameters.DQ)}");
            Log.Diagnostic($"InverseQ: {BitConverter.ToString(parameters.InverseQ)}");
            Log.Diagnostic($"P: {BitConverter.ToString(parameters.P)}");
            Log.Diagnostic($"Q: {BitConverter.ToString(parameters.Q)}");
#endif
        }
    }
}