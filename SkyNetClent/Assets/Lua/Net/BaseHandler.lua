BaseHandler=class("BaseHandler")

function BaseHandler:OnMessage(smallId, buffer)
   self[BaseHandler.MsgId[smallId]](self,buffer)
end