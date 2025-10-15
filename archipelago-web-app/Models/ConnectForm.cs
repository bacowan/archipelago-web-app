using System;

namespace archipelagoWebApp.Models;

public class ConnectForm
{
    public string HostAndPort { get; set;  } = "archipelago.gg:38281";

    public string PlayerName { get; set;  } = "";

    public string Password { get; set;  } = "";
}
