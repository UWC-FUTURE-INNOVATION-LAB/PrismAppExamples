using Prism.Events;
using PrismAppExample.Model.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismAppExample.Messages.Security
{
    public class LoginMessage : PubSubEvent<UserProfile>
    {
    }
}
