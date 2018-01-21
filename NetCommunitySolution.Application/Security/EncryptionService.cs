using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace NetCommunitySolution.Security
{
    public class EncryptionService : NetCommunitySolutionAppServiceBase, IEncryptionService
    {
        #region Fields

        /// <summary>
        /// RSA加密的密匙结构  公钥和私匙
        /// </summary>

        public string PublicKey
        {
            get { 
                return @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAq+1lHIoVdDPVlVaJZrqO
oW6dUHFRjPBY7WGTpawdNVfeXfBa6EhM8u/Vm2D91PsbNMtor8Eyq6xe/2JpdXwn
NNIQYCB5XqXEhxkwQISRkio0E1uqpxqJZ9lo7b/ELBeHyze7Z3MNXO8OmxmEyTdo
ao7iv5eqnUOcZtNhS4ddjuIPEfGL2UjHBhUEMrQeh8gWr11y0XEYNhhP7m6Yu+FQ
Yuz2HA6kvfONHHDuMiiL4y1yhK9dMWNkE57nwfCx7l5TtnesMxGKnPeBD74ZiXFe
/Ccau8gvUNFXnYG1PSP3C9cN+FzdpitCJitfK+P/bCMysP4Kn4F5+qxl8i3va6Q8
iQIDAQAB".Replace("\r\n", "");
            }
        }
        public string PrivateKey
        {
            get
            {
                return @"MIIEogIBAAKCAQEAsEgCVFSobsZqsSvJag1Tlj04PTdhLRf0UxWvYMHLEz9dVJH4
/B9gUYrwRPRgqj2ZBHPX2JvWv1/p5EypW6kWRZhhGwubRAsvKRYDXruSp9OChny+
0qq1aoC2hi9ybup+qVpDyFcChbmneBMAipDf/95S1ek+0KaZzpfpgiWJ+Q9oDx5e
EU0sxsUl1tFjCq2U+tiwJMafE8sKxWxVnvPyqG6IYzhRiVgiNPnfvyM2vBsaERpp
gulsC7b+vg+qoYCY5qqUI4iweDoR7tNLE3Q6iNVaiGlN5ChDpHxk5TRJopT75w67
Grxs0ROtixhrUOuRI6HQc6dM2u4KSLc4dgiABQIDAQABAoIBADyD29jbXx74GnRL
1c8EWCeBSKcrNb7nNEa5cQVEQdSPshhwLAtRMh2MFsN1KSIIF1mB8x03EleMM5zd
/F2tBCpMDznbmCeZt0zhc7K3rbbTU0Gb3V4woCq5mO8jpqMQ+P5mMFK8G++QNj9w
NoVbCqqpxcINWjUCnFZhrBWzkVPgNgQD/qMRVBaxDZtY3mbBKwXLRGZTRgvyEakO
DkUQKBHmeFLNrvZBvhw9UMZqyFq/6i2o48y7BitOaasLBNni16ry0OtASlA/Z5w+
71ccNOR7hcxOO5XixmYxWVw33gSQFkokSQ6KbOwaA7JKBCFmZZezzi7oP3uA9I1M
TWH7fhkCgYEA4Gt8rZzjuCR67rZfHqOFrHJp2buYGzeuVfrn7dRh64z0/X8YqAnz
ZBP/C+z2utxkmgKJzXCido4uUXGPqKYC7K+ZmDo9y8l0Bx+lm8KrOof4nrYKJ26H
XWKLBVbJWm61lvXqdgTxU2RLycOA/MbGf5SXz2YN5eZDQOs3C4ILn68CgYEAyRZg
DIrllcTM1ydKfWRoOUnkT1lqFxZkmhVqZrV99ydTSNWJNuQiIIhlklm5Eki2mnu4
iFhK+g+V3YFtJJYcAJRvaB9xG7RUZHpmeD3g9rXfDUAV6ubMP9fZkuzTLyWC66oK
W6yLonE7C5/tp83pLp55vsCPVsFSMNF8gP5a9IsCgYA3dPhbYrC3OXSTdsesp5YJ
1kAoCP5+g1T2dElJ8Ti9X5jO59Bs/gCCU5qsFMOny3ykvknVVacgCuSRRbHNaDDC
0mXvQz0wFbkxZXWkNwffL+iVN8Dsm5ih0A8wo5CgY0lGY8Crp28HP3qnAkPmsyws
LMkS/FIk5LDr4vQ4SMuciwKBgBYlorp+6b5PCIiByVB6KIh2vPCIVojobHgT974M
Ky13ZVLWQeCLy75th1JRE3MSExp8mdxabReRc94LjSvEZJ0jvAlpvRliCRFv8a04
k3hYm5JoBoHynXhCm7H052V/6iVueumR3WcstWYYXSJoKVB7H8mEh8T0UcsSp3XF
jdr1AoGADimaaRh8236Fp9vtSVqMZcLOkVDeDPJ/DTn0+0m1bezXtngVSDpv0cRm
aNYhG5bOCSqT/yKpgurDSZwoOLgcbeKejqLjqlkN1Wafzb5blbXRY9YC0NIgHftX
UuBgRVBInftR9k61OlQIvGm8gK8xKvdIFJg5Zg3ghNYv+5kyx58=".Replace("\r\n", "");
            }

        }


        public string SPublicKey
        {
            get
            {
                return @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAtWtLkmSKQ8P08wG58SvS
woKSwlGaDvvs/ZV/nroztCXAJ70AvNwurbuK9TaEWp1x2KLBr3vCuMfo4ENBUA69
GzUE7VgmOD1H3KOO+clOTEUbJpQXPCLQ5SIy/gk7Eh8YXBK9+oG/qn8ZPqPEsSDF
x3lQvto5FnmvG3bYZvEG+6j/iUM7dnfOa2BnNiFLknwjNqRAcV3vvsUxi3s4ni28
iV0bCjpU6tMwSg/tpcnausblIfL144PDZDF8fyTYkMHm9YiuQPlJXsf37eH6t8nB
ncIs87GuPt4AMcyYsxOV1naUqHtyAakqYUYVUelMuwaX38LgbwVjDYdSj/sqyXxG
0wIDAQAB".Replace("\r\n", "");
            }
        }


        private RSACryptoServiceProvider _privateKeyRsaProvider;
        private RSACryptoServiceProvider _publicKeyRsaProvider;
        #endregion

        #region Ctro
        //public EncryptionService()
        //{
        //    _privateKeyRsaProvider = DecodeRSAPrivateKey(PrivateKey);
        //    //_privateKeyRsaProvider.FromPKCS8PrivateKey(Convert.FromBase64String(PrivateKey));

        //    _publicKeyRsaProvider = new RSACryptoServiceProvider();
        //    _publicKeyRsaProvider.FromPKCS8PrivateKey(Convert.FromBase64String(PublicKey));
        //}
        #endregion


        #region method


        public string ConvertParamatersSign<T>(T model, string key) where T : IParamater
        {
            string tStr = string.Empty;
            var properties = model.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            if (properties.Length <= 0)
                return "";

            var akeys = new ArrayList();
            foreach (var item in properties)
            {
                akeys.Add(item.Name);
            }
            akeys.Sort();

            foreach (var item in akeys)
            {
                var prop = model.GetType().GetProperty(item.ToString());
                tStr += string.Format("{0}={1}", item.ToString(), prop.GetValue(model, null)) + "&";
            }
            tStr = tStr + key;
            return CommonHelper.GetMD5(tStr);
        }

        public virtual string CreateSaltKey(int size)
        {
            //生成加密随机数
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);

            //返回一个随机数的Base64字符串表示形式
            return Convert.ToBase64String(buff);
        }

        public string CreatePasswordHash(string password, string saltkey, string passwordFormat = "SHA1")
        {
            if (String.IsNullOrEmpty(passwordFormat))
                passwordFormat = "SHA1";
            string saltAndPassword = String.Concat(password, saltkey);

            var algorithm = HashAlgorithm.Create(passwordFormat);
            if (algorithm == null)
                throw new ArgumentException("Unrecognized hash name");

            var hashByteArray = algorithm.ComputeHash(Encoding.UTF8.GetBytes(saltAndPassword));
            return BitConverter.ToString(hashByteArray).Replace("-", "");
        }
        public string ConvertParamaters<T>(T model ,bool formatDecimal =true) where T : IParamater
        {
            string tStr = string.Empty;
            var properties = model.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            if (properties.Length <= 0)
                return "";

            var akeys = new ArrayList();
            foreach (var item in properties)
            {
                akeys.Add(item.Name);
            }
            akeys.Sort();
                       

            foreach (var item in akeys)
            {
                var types = model.GetType().GetProperty(item.ToString());
                if (formatDecimal && (types.PropertyType == typeof(double) || types.PropertyType == typeof(decimal)))
                {
                    var prop = model.GetType().GetProperty(item.ToString());
                    var propValue = prop.GetValue(model, null);
                    var value = Convert.ToDecimal(propValue);
                    tStr += string.Format("{0}={1}", item.ToString(), value.ToString("#0.00")) + "&";

                }
                else
                {
                    var prop = model.GetType().GetProperty(item.ToString());
                    tStr += string.Format("{0}={1}", item.ToString(), prop.GetValue(model, null)) + "&";
                }
            }
            return tStr.Substring(0, tStr.Length - 1);
        }

        #endregion


        #region Rsa Method

        public string Encrypt(string text)
        {
                _publicKeyRsaProvider = CreateRsaProviderFromPublicKey(PublicKey);

            var code = Encoding.ASCII.GetBytes(text);
            var PlaintextData = Encoding.UTF8.GetBytes(text);
            int MaxBlockSize = _publicKeyRsaProvider.KeySize / 8 - 11;    //加密块最大长度限制

            if (PlaintextData.Length <= MaxBlockSize)
                return Convert.ToBase64String(_publicKeyRsaProvider.Encrypt(PlaintextData, false));


            using (MemoryStream PlaiStream = new MemoryStream(PlaintextData))
            using (MemoryStream CrypStream = new MemoryStream())
            {
                Byte[] Buffer = new Byte[MaxBlockSize];
                int BlockSize = PlaiStream.Read(Buffer, 0, MaxBlockSize);

                while (BlockSize > 0)
                {
                    Byte[] ToEncrypt = new Byte[BlockSize];
                    Array.Copy(Buffer, 0, ToEncrypt, 0, BlockSize);

                    Byte[] Cryptograph = _publicKeyRsaProvider.Encrypt(ToEncrypt, false);
                    CrypStream.Write(Cryptograph, 0, Cryptograph.Length);

                    BlockSize = PlaiStream.Read(Buffer, 0, MaxBlockSize);
                }

                return Convert.ToBase64String(CrypStream.ToArray(), Base64FormattingOptions.None);
            }
        }

        public bool Verify(string text,string signature)
        {
            _publicKeyRsaProvider = CreateRsaProviderFromPublicKey(SPublicKey);
            return _publicKeyRsaProvider.VerifyData(Encoding.UTF8.GetBytes(text), Convert.FromBase64String(signature), HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
        }

        
        public string Sign(string text)
        {
            _privateKeyRsaProvider = CreateRsaFromPrivateKey();
            return Convert.ToBase64String(_privateKeyRsaProvider.SignData(Encoding.UTF8.GetBytes(text), HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1));
        }
        #endregion

        #region Utilities

        private RSACryptoServiceProvider CreateRsaFromPrivateKey()
        {
            var privateKeyBits = System.Convert.FromBase64String(PrivateKey);
            var rsa = new RSACryptoServiceProvider();
            var RSAparams = new RSAParameters();

            using (var binr = new BinaryReader(new MemoryStream(privateKeyBits)))
            {
                byte bt = 0;
                ushort twobytes = 0;
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)
                    binr.ReadByte();
                else if (twobytes == 0x8230)
                    binr.ReadInt16();
                else
                    throw new Exception("Unexpected value read binr.ReadUInt16()");

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102)
                    throw new Exception("Unexpected version");

                bt = binr.ReadByte();
                if (bt != 0x00)
                    throw new Exception("Unexpected value read binr.ReadByte()");

                RSAparams.Modulus = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.Exponent = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.D = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.P = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.Q = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.DP = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.DQ = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.InverseQ = binr.ReadBytes(GetIntegerSize(binr));
            }

            rsa.ImportParameters(RSAparams);
            return rsa;
        }


        private int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            byte lowbyte = 0x00;
            byte highbyte = 0x00;
            int count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02)
                return 0;
            bt = binr.ReadByte();

            if (bt == 0x81)
                count = binr.ReadByte();
            else
                if (bt == 0x82)
            {
                highbyte = binr.ReadByte();
                lowbyte = binr.ReadByte();
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = bt;
            }

            while (binr.ReadByte() == 0x00)
            {
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);
            return count;
        }



        private RSACryptoServiceProvider CreateRsaProviderFromPublicKey(string publicKeyString)
        {
            // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
            byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            byte[] x509key;
            byte[] seq = new byte[15];
            int x509size;

            x509key = Convert.FromBase64String(publicKeyString);
            x509size = x509key.Length;

            // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------
            using (MemoryStream mem = new MemoryStream(x509key))
            {
                using (BinaryReader binr = new BinaryReader(mem))  //wrap Memory Stream with BinaryReader for easy reading
                {
                    byte bt = 0;
                    ushort twobytes = 0;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                        binr.ReadByte();    //advance 1 byte
                    else if (twobytes == 0x8230)
                        binr.ReadInt16();   //advance 2 bytes
                    else
                        return null;

                    seq = binr.ReadBytes(15);       //read the Sequence OID
                    if (!CompareBytearrays(seq, SeqOID))    //make sure Sequence for OID is correct
                        return null;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8103) //data read as little endian order (actual data order for Bit String is 03 81)
                        binr.ReadByte();    //advance 1 byte
                    else if (twobytes == 0x8203)
                        binr.ReadInt16();   //advance 2 bytes
                    else
                        return null;

                    bt = binr.ReadByte();
                    if (bt != 0x00)     //expect null byte next
                        return null;

                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                        binr.ReadByte();    //advance 1 byte
                    else if (twobytes == 0x8230)
                        binr.ReadInt16();   //advance 2 bytes
                    else
                        return null;

                    twobytes = binr.ReadUInt16();
                    byte lowbyte = 0x00;
                    byte highbyte = 0x00;

                    if (twobytes == 0x8102) //data read as little endian order (actual data order for Integer is 02 81)
                        lowbyte = binr.ReadByte();  // read next bytes which is bytes in modulus
                    else if (twobytes == 0x8202)
                    {
                        highbyte = binr.ReadByte(); //advance 2 bytes
                        lowbyte = binr.ReadByte();
                    }
                    else
                        return null;
                    byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };   //reverse byte order since asn.1 key uses big endian order
                    int modsize = BitConverter.ToInt32(modint, 0);

                    int firstbyte = binr.PeekChar();
                    if (firstbyte == 0x00)
                    {   //if first byte (highest order) of modulus is zero, don't include it
                        binr.ReadByte();    //skip this null byte
                        modsize -= 1;   //reduce modulus buffer size by 1
                    }

                    byte[] modulus = binr.ReadBytes(modsize);   //read the modulus bytes

                    if (binr.ReadByte() != 0x02)            //expect an Integer for the exponent data
                        return null;
                    int expbytes = (int)binr.ReadByte();        // should only need one byte for actual exponent data (for all useful values)
                    byte[] exponent = binr.ReadBytes(expbytes);

                    // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                    RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                    RSAParameters RSAKeyInfo = new RSAParameters();
                    RSAKeyInfo.Modulus = modulus;
                    RSAKeyInfo.Exponent = exponent;
                    RSA.ImportParameters(RSAKeyInfo);

                    return RSA;
                }

            }


        }
        private bool CompareBytearrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            int i = 0;
            foreach (byte c in a)
            {
                if (c != b[i])
                    return false;
                i++;
            }
            return true;
        }

        #endregion
    }
}