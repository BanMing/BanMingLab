require("Net/MsgId")
MsgHandler = {}
local handler
function MsgHandler.DispatchMsg(bigId, smallId, buffer)
    print("Lua Get Msg BigId:", bigId, "smallId:", smallId)
    --    local = BigMsg[bigId]
    --    print("funcName:",funcName)
    --    if bigId==BigMsgId.Login then
    --        LoginMsgHandler:OnMessage(smallId,buffer)
    --    end

    if handler == nil then
        handler = LoginMsgHandler:new()
    end

    handler:OnMessage(smallId, buffer)
end
