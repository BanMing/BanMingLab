LoginMsgHandler = class("LoginMsgHandler")
LoginMsgHandler.MsgId = {
    [1] = "S2CLogin",
    [2] = "S2CLoginSuccess"
}

function LoginMsgHandler:OnMessage(smallId, buffer)
    self[LoginMsgHandler.MsgId[smallId]](buffer)
end

function LoginMsgHandler:S2CLogin(buffer)   
    local base_pb=require("Assets.Lua.Proto.Base_pb")
    local msg= base_pb.C2SLogin()
    msg.account="mm"
    msg.pwd="pwd"
    msg.servernumber=33
    msg.mac="mac"
    local data= msg:SerializeToString()
    TestStart.networkManager:SendMessage(1,1,data)
end

function LoginMsgHandler:S2CLoginSuccess(buffer)
end
