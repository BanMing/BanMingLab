BaseHandler=class("BaseHandler")
BaseHandler.S2CMsg={

}
BaseHandler.C2SMsgId={
    
}

function BaseHandler:OnMessage(smallId, buffer)
   self[BaseHandler.MsgId[smallId]](self,buffer)
end