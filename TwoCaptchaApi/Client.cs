using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TwoCaptchaApi
{
    class Client
    {

        /// <summary>
        /// Api Key of 2Captcha.com
        /// </summary>
        public string APIKey { get; private set; }


        /// <summary>
        /// Total number of checks to be performed till recaptcha answer is received.
        /// </summary>
        public int TotalChecks { get; set; } = 50;


        /// <summary>
        /// Delay time in seconds between each check performed.
        /// </summary>
        public int DelayTime { get; set; } = 5;

        public Client(string apiKey)
        {
            APIKey = apiKey;
        }

        public async Task<double> GetBalanceAsync()
        {
            var src = await Req.GetAsync("http://2captcha.com/res.php?key=" + APIKey + "&action=getbalance");
            if (double.TryParse(src, out double balance))
                return balance;
            return 0.0;
        }

        public async Task<Answer> SolveRecaptchaV2(string googleKey, string pageUrl, bool isInvisibleCaptcha = false)
        {

            try
            {
                var ReqUrl = "http://2captcha.com/in.php?key=" + APIKey + "&method=userrecaptcha&googlekey=" + googleKey + "&pageurl=" + pageUrl;

                if (isInvisibleCaptcha)
                    ReqUrl += "&invisible=1";

                var response = await Req.GetAsync(ReqUrl);

                if (response.Length < 3)
                {
                    return new Answer(response, Status.Error);
                }
                else
                {
                    if (response.Substring(0, 3) == "OK|")
                    {
                        string captchaID = response.Remove(0, 3);

                        for (int i = 0; i < TotalChecks; i++)
                        {
                            var answerResponse = await Req.GetAsync("http://2captcha.com/res.php?key=" + APIKey + "&action=get&id=" + captchaID);

                            if (answerResponse.Length < 3)
                            {
                                return new Answer(answerResponse, Status.Error);
                            }
                            else
                            {
                                if (answerResponse.Substring(0, 3) == "OK|")
                                {
                                    return new Answer(answerResponse.Remove(0, 3), Status.Success);
                                }
                                else if (answerResponse != "CAPCHA_NOT_READY")
                                {
                                    return new Answer(answerResponse, Status.Error);
                                }
                            }

                            Thread.Sleep(DelayTime * 1000);
                        }
                        return new Answer(string.Empty, Status.Timeout);
                    }
                }
            }
            catch (Exception)
            {
            }

            return new Answer(string.Empty, Status.Error);
        }
    }
}
