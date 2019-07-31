using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoCaptchaApi
{
    public class Answer
    {
        public string Recaptcha_Ans { get; set; }
        public Status Status { get; set; }

        public Answer(string ans, Status s)
        {
            Recaptcha_Ans = ans;
            Status = s;
        }
    }
}
