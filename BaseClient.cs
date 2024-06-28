using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Networking.Transport;

public class BaseClient : MonoBehaviour
{
    public string ipAdress = "127.0.0.1";
    public ushort port = 8000;
    public NetworkDriver driver;
    protected NetworkConnection connection;
    public int myConnectionId = -1;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateClient();
    }

    private void OnDestroy()
    {
        Shutdown();
    }
    public virtual void Init()
    {
        driver = NetworkDriver.Create();
        connection = default(NetworkConnection);

        NetworkEndpoint endpoint = NetworkEndpoint.Parse(ipAdress, port);
        connection = driver.Connect(endpoint);

        Debug.Log("Attempting to join: " + endpoint.ToString());
    }
}
