LoginMsgHandler = class("LoginMsgHandler")
LoginMsgHandler.MsgId = {
    [1] = "S2CLogin",
    [2] = "S2CLoginSuccess"
}

local base_pb = require("Protol.Base_pb")

function LoginMsgHandler:OnMessage(smallId, buffer)
    self[LoginMsgHandler.MsgId[smallId]](buffer)
end

function LoginMsgHandler:S2CLogin(buffer)  
    local msg = base_pb.C2SLogin()
    msg.account = "mm"
    msg.pwd = "pwd"
    msg.servernumber = 33
    msg.mac = "mac"
    local data = msg:SerializeToString()
    TestStart.networkManager:SendMessage(1, 1, data)
end

function LoginMsgHandler:S2CLoginSuccess(buffer)
    print("LoginMsgHandler:S2CLoginSuccess")
    local msg=Protol.Base_pb.S2CLoginSuccess()
    msg:ParseFromString(buffer)   
    print("id:",msg.id,"name:",msg.name,"servernumber:",msg.servernumber)
end
