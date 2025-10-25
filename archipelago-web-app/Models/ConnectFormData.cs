using System;

namespace archipelago_web_app.Models;

public class ConnectFormData
{
    public string HostAndPort { get; set;  } = "archipelago.gg:38281";

    public string PlayerName { get; set;  } = "";

    public string Password { get; set;  } = "";
}
