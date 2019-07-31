# TwoCaptchaApi

    private async void SolveAsync()
    {
            var client = new Client("672862832aaa6d5d9ec9584a2a744fd3");
            var ans = await client.SolveRecaptchaV2("6Le-wvkSAAAAAPBMRTvw0Q4Muexq9bi0DJwx_mJ-", "https://www.google.com/recaptcha/api2/demo", false);

            if (ans.Status == Status.Success)
            {
                Console.WriteLine(ans.Recaptcha_Ans);
            }
     }
