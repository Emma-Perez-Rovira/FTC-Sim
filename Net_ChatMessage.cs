using Unity.Collections;
using UnityEngine;

public class Net_ChatMessage : NetMessage
{
    public Net_ChatMessage() 
    {
        Code = OpCode.CHAT_MESSAGE;
    }
    public override void Serialize(ref DataStreamWriter writer)
    {
        writer.WriteByte((byte)Code);
    }
    public override void Deserialize()
    {

    }
}
