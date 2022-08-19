//---------------------------------------------------------------------------
// MLProtect_HTML5Plugin.cs : Personal protection code for the Medialooks License system
//---------------------------------------------------------------------------
// Copyright (c) 2018, Medialooks Soft
// www.medialooks.com (dev@medialooks.com)
//
// Authors: Medialooks Team
// Version: 3.0.0.0
//
//---------------------------------------------------------------------------
// CONFIDENTIAL INFORMATION
//
// This file is Intellectual Property (IP) of Medialooks Soft and is
// strictly confidential. You can gain access to this file only if you
// sign a License Agreement and a Non-Disclosure Agreement (NDA) with
// Medialooks Soft. If you had not signed any of these documents, please
// contact <dev@medialooks.com> immediately.
//
//---------------------------------------------------------------------------
// Usage:
//
// 1. Add the reference to MLProxy.dll (located in the SDK /bin folder) in your C# project
//
// 2. Add this file to the project
//
// 3. Call HTML5PluginLic.IntializeProtection() method before creating any Medialooks 
//    objects (for e.g. in Form_Load event handler)
//
// 4. Compile the project
//
// IMPORTANT: If you have several Medialooks products, don't forget to initialize
//            protection for all of them. For e.g.
//
//            MPlatformSDKLic.IntializeProtection();
//            DecoderlibLic.IntializeProtection();
//            etc.

using System;

    public class HTML5PluginLic
    {        
        private static MLPROXYLib.CoMLProxyClass m_objMLProxy;
        private static string strLicInfo = @"[MediaLooks]
License.ProductName=HTML5 Plugin
License.IssuedTo=Datastat d.o.o.
License.CompanyID=13446
License.UUID={49F26C90-E5F2-475D-99E0-E9F7C4905BF7}
License.Key={17DF4EC6-1B39-4F07-7C00-CD2FB4387C8B}
License.Name=HTML page overlay plugin for MPlatform and MFormats
License.AllowedObject={877DD976-9AB6-4366-A3DA-89A76FD8B547}
License.UpdateExpirationDate=March 8, 2023
License.Edition=Standard
License.AllowedModule=*.*
License.Signature=2A6BE3576E787B3CCDACEBE5079761C18BB3324CA51E988029A2CBE57467FDB86BCB025AED4626FE8CF50735D29F1300994CEAC425786B3AB69233C676EECF26D2EE457BABB8955084E9CD7C5A0F1200B05DD9A38EF8B79C0989E69A9D99A9F7607CF370D58B0A4C8130CC49BD6390B8015813B5E1ED9D9CD215240B6923AC05

";

		//License initialization
        public static void IntializeProtection()
        {
            if (m_objMLProxy == null)
            {
                // Create MLProxy object 
                m_objMLProxy = new MLPROXYLib.CoMLProxyClass();
                m_objMLProxy.PutString(strLicInfo);                
            }
           UpdatePersonalProtection();
        }

        private static void UpdatePersonalProtection()
        {
            ////////////////////////////////////////////////////////////////////////
            // MediaLooks License secret key
            // Issued to: Datastat d.o.o.
            const long _Q1_ = 43917301;
            const long _P1_ = 39994267;
            const long _Q2_ = 57352817;
            const long _P2_ = 63462227;

            try
            {

                int nFirst = 0;
                int nSecond = 0;
                m_objMLProxy.GetData(out nFirst, out  nSecond);

                // Calculate First * Q1 mod P1
                long llFirst = (long)nFirst * _Q1_ % _P1_;
                // Calculate Second * Q2 mod P2
                long llSecond = (long)nSecond * _Q2_ % _P2_;

                uint uRes = SummBits((uint)(llFirst + llSecond));

                // Calculate check value
                long llCheck = (long)(nFirst - 29) * (nFirst - 23) % nSecond;
                // Calculate return value
                int nRand = new Random().Next(0x7FFF);
                int nValue = (int)llCheck + (int)nRand * (uRes > 0 ? 1 : -1);

                m_objMLProxy.SetData(nFirst, nSecond, (int)llCheck, nValue);

            }
            catch (System.Exception) { }

        }

        private static uint SummBits(uint _nValue)
        {
            uint nRes = 0;
            while (_nValue > 0)
            {
                nRes += (_nValue & 1);
                _nValue >>= 1;
            }

            return nRes % 2;
        }
    }