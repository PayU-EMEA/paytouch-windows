// Copyright @ 2014 - 2015 PayU

using System;
using PayU.SDK;
using PayU.SDK.Model;
using System.Threading.Tasks;

namespace NewMerchantSampleApp
{
    public class SampleMerchantAccessTokenProvider : IMerchantAccessTokenProvider
    {
        public async Task<ServiceResponse<AccessToken>> GetAccessTokenAsync()
        {
            return await Task.Factory.StartNew(() =>
            {
                throw new NotImplementedException("Get merchant access token from your own service");
                return new ServiceResponse<AccessToken>();
            });
        }
    }
}