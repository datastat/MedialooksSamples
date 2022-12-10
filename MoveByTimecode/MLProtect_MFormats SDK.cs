//---------------------------------------------------------------------------
// MLProtect_MFormatsSDK.cs : Personal protection code for the Medialooks License system
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
// 3. Call MFormatsSDKLic.IntializeProtection() method before creating any Medialooks 
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

    public class MFormatsSDKLic
    {        
        private static MLPROXYLib.CoMLProxyClass m_objMLProxy;
        private static string strLicInfo = @"[MediaLooks]
License.ProductName=MFormats SDK
License.IssuedTo=Datastat d.o.o.
License.CompanyID=13446
License.UUID={8A95DA36-2C98-4B63-83ED-9F721807ECFB}
License.Key={FB02273D-9BB5-85BC-2B14-72433C0951BF}
License.Name=MFormats Module
License.UpdateExpirationDate=March 8, 2023
License.Edition=io_prof Professional gpu_h264 gpu_pipeline
License.AllowedModule=*.*
License.Signature=4467691042E34D7E5DF0207E2E9E4A3C4197C691554EFB3C0F13F333D76CF8DD7ADEE9395EC8AC91131C1F9F9404578AC9F5837BD67EC2E357D2576B0159A4B85EDFE258C79D4C15ACF5F1406007936CE7386DB436EF577D8BC12B95943F6C3EEABFA2852A3B6CEEA6390E27796336B85015842F3AF36C3A2D7C93AFFC57735C

[MediaLooks]
License.ProductName=MFormats SDK
License.IssuedTo=Datastat d.o.o.
License.CompanyID=13446
License.UUID={EB43CD16-C2F1-47EF-8D0F-AC7F01D774C7}
License.Key={FB02273D-E130-4CF0-C068-2662F8618883}
License.Name=MFRenderer Module
License.UpdateExpirationDate=March 8, 2023
License.Edition=io_prof Professional gpu_h264 gpu_pipeline
License.AllowedModule=*.*
License.Signature=F221038DEA4A5062A56619C3345852F7177D4F32CF3522EDCD2488E2398D5BEE824EF35BD16D934E53FE50C25194C240AFB08C89BEBBDF30C866655C10B9C33375F5B85C5934EEBE771C4295D6DB9B1BFFBD772D8F5973B25B7D4CFDE292F68371BD0328B025AA07A081EFE8B6AED2833AAFC4333FE387B227833DF3E8C82089

[MediaLooks]
License.ProductName=MFormats SDK
License.IssuedTo=Datastat d.o.o.
License.CompanyID=13446
License.UUID={968C0732-AFA1-4107-B9DE-076364D247E7}
License.Key={5F9800AC-5BDA-73AB-511D-56C3D8781495}
License.Name=MFReader Module
License.UpdateExpirationDate=March 8, 2023
License.Edition=io_prof Professional gpu_h264 gpu_pipeline
License.AllowedModule=*.*
License.Signature=D80E752068CDAA75946F505A9F4CF4BBD642424B38FE8CBD2BFA810C39C55AE1E83BDAC49402B359456F7DF07AB9E81959DB61B05EFFC3F8F4A7CDA858240856E42A0DBB4045170736CFB1C41BC3871BE9A1F5E67AE840121B0EB191C7E30D25B48D07720C0055B54520A04036FBCD43A6777784B33F465DA2B005BA5C2EC470

[MediaLooks]
License.ProductName=MFormats SDK
License.IssuedTo=Datastat d.o.o.
License.CompanyID=13446
License.UUID={5D3406AB-D8F2-4298-A56B-392EABBEE6D6}
License.Key={6CD427BD-3CCB-8D7F-7DBD-2CA193BB62A9}
License.Name=MFWriter Module
License.UpdateExpirationDate=March 8, 2023
License.Edition=io_prof Professional gpu_h264 gpu_pipeline
License.AllowedModule=*.*
License.Signature=E07139F652615024F534EAB08383F2C5E09A9233794F4F2864EC3D4F1CAC69B75961453DAAF84EFD80D34273CFAA13F3434F5C47F2E3188F146971E78C38238E667BA15D49078FD73735779F35E0FFAC0BB6AC75228E7FA78AB2B61140F695EBDA0EFEE85EB19A60987EE776A3806ED3989ABBB97675555259072D9E9C312C00

[MediaLooks]
License.ProductName=MFormats SDK
License.IssuedTo=Datastat d.o.o.
License.CompanyID=13446
License.UUID={B922CA86-D232-474A-964B-34550DBBFEC0}
License.Key={AF6237AD-B339-6621-BF8A-972FDA4A9755}
License.Name=Delay internal module
License.UpdateExpirationDate=March 8, 2023
License.Edition=io_prof Professional gpu_h264 gpu_pipeline
License.AllowedModule=*.*
License.Signature=C62334F9056753C130CAD9F84E689F9CCB67558D42EBFD9487FFCA7BD29E85C33AFBEBDB94FAF66EDC036613804DBF53D7C8E991DB4FE41DDBE894F214CF5574AD041E65262103B34443666BA0912F9A62FB1DBCAB987B12A02A124695AC460ED247902218C5E8D943E48F876E6F79CE3BA28D9560FC5B3923F46818D6C9C013

[MediaLooks]
License.ProductName=MFormats SDK
License.IssuedTo=Datastat d.o.o.
License.CompanyID=13446
License.UUID={22909841-F7DA-43D1-8243-D78668BF2A7F}
License.Key={67A17DC7-F883-F571-B024-11E2CD4E02D6}
License.Name=MWebRTC module
License.UpdateExpirationDate=March 8, 2023
License.Edition=Standard gpu_h264 gpu_pipeline
License.AllowedModule=*.*
License.Signature=6704BEDB5803E87967B9D1E5F5C0F43539D1DCB72BE3EC8C0F02A8093768786F1376B13DCE9ED581D41F7D0B5E6B8E7CDCD16C7B2462C9C8B3C72CA55DA5BE8E10C6A050D86CDB2995005ABB33AECB8A61A86E2AD68CE8CF54357BDB6F5CDD69FE679984E0C66FDF7C5F5295A2FAFF36A80945DC37EDD35C24BE1E944D7E0799

[MediaLooks]
License.ProductName=MFormats SDK
License.IssuedTo=Datastat d.o.o.
License.CompanyID=13446
License.UUID={B53E549B-5A08-4F46-8B90-58A9F75D87D8}
License.Key={5F9800AC-39DB-E64C-1775-CA738BBFF79E}
License.Name=Medialooks DXGI Screen Capture
License.UpdateExpirationDate=March 8, 2023
License.Edition=Standard
License.AllowedModule=*.*
License.Signature=C84904E6354E44C0FAF178FE9967736C5C21D7481FBABD0EC444A11CAD5DBD60392843ED4C8BEBD38310FB9726F8527B590656CF87C87897396D4380F79BA10E80C248D86AA908308A5DDE559937944BAD08592DECB194ECAB6B47ED7ADE645C8DD24A068CFE3B2ECBE143EF191B7602CDC2E5CA8D577BF6A5F8289123447E94

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