using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Networking.Transport;
using Unity.Collections;
using System.Xml.Serialization;

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

        NetworkEndpoint endpoint = NetworkEndpoint.LoopbackIpv4;
        endpoint.Port = 5522;
        connection = driver.Connect(endpoint);

        Debug.Log("Attempting to join (Client): " + endpoint.ToString());
    }
    public virtual void UpdateClient()
    {
        driver.ScheduleUpdate().Complete();
        CheckAlive();
        UpdateMessagePump();
    }
    private void CheckAlive()
    {
        if(!connection.IsCreated)
        {
            Debug.Log("Lost connnection to server");
        }
    }
    protected virtual void UpdateMessagePump()
    {
        DataStreamReader stream;

        NetworkEvent.Type cmd;
        while ((cmd = connection.PopEvent(driver, out stream)) != NetworkEvent.Type.Empty)
        {
            if(cmd == NetworkEvent.Type.Connect)
            {
                Debug.Log("Now connected to server");
            }
            else if (cmd == NetworkEvent.Type.Data)
            {
                uint value = stream.ReadByte();
                Debug.Log("Got the value: " + value + " back from the server");
            }
            else if(cmd == NetworkEvent.Type.Disconnect)
            {
                Debug.Log("Client got disconnected from server");
                connection = default(NetworkConnection);
            }
        }

    }
    public virtual void Shutdown()
    {
        driver.Dispose();
    }
    public virtual void SendToServer(NetMessage msg)
    {
        DataStreamWriter writer;
        driver.BeginSend(connection, out writer);
        msg.Serialize(ref writer);
        driver.EndSend(writer);
    }
}
