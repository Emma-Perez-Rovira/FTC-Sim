using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Collections;
using Unity.Networking.Transport;
using UnityEditor.Experimental.GraphView;
using UnityEditor.MemoryProfiler;
using UnityEngine;

public class BaseServer : MonoBehaviour
{
    public NetworkDriver driver;
    protected NativeList<NetworkConnection> connections;


    public int myConnectionId = -1;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateServer();
    }

    private void OnDestroy()
    {
        Shutdown();
    }
    public virtual void Init()
    {
        driver = NetworkDriver.Create();
        NetworkEndpoint endpoint = NetworkEndpoint.AnyIpv4;
        endpoint.Port = 5522;
        if (driver.Bind(endpoint) != 0)
        {
            Debug.Log("ERROR BINDING TO PORT: " + endpoint.Port);
        } else
        {
            driver.Listen();
        }

        connections = new NativeList<NetworkConnection>(4, Allocator.Persistent);
    }
    public virtual void UpdateServer()
    {
        driver.ScheduleUpdate().Complete();
        CleanupConnections();
        AcceptNewConnections();
        UpdateMessagePump();
    }

    private void CleanupConnections()
    {
        for (int i = 0; i < connections.Length; i++) 
        {
            if (!connections[i].IsCreated)
            {
                connections.RemoveAtSwapBack(i);
                i--;
            }
        }
    }
    private void AcceptNewConnections()
    {
        NetworkConnection c;
        while((c = driver.Accept()) != default(NetworkConnection))
        {
            connections.Add(c);
            Debug.Log("Accepted a connection");
        }
    }
    protected virtual void UpdateMessagePump()
    {
        DataStreamReader stream;
        for (int i = 0; i < connections.Length; i++)
        {
            NetworkEvent.Type cmd;
            while ((cmd = driver.PopEventForConnection(connections[i], out stream)) != NetworkEvent.Type.Empty)
            {
                if (cmd == NetworkEvent.Type.Data)
                {
                    byte opCode = stream.ReadByte();
                    Debug.Log("Got " + opCode + " as opcode");
                }
                else if (cmd == NetworkEvent.Type.Disconnect) 
                {
                    Debug.Log("Client Disconnected from server");
                    connections[i] = default(NetworkConnection);
                }
            }
        }
    }
    public virtual void Shutdown()
    {
        driver.Dispose();
        connections.Dispose();
    }
}
