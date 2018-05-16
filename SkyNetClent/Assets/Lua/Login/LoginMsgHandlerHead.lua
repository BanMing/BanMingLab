require("Login/LoginMsgHandler")
require("Protol.Base_pb")

LoginMsgHandler.S2CMsg = {
    [1] = "S2CLogin",
    [2] = "S2CLoginSuccess"
}
LoginMsgHandler.C2SMsgId={
    SendLogin=1
}

BigMsgId.Login = 1
BigMsgHandler[BigMsgId.Login] = LoginMsgHandler:new()
