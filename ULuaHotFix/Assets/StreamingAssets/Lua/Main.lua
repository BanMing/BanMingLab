--主入口函数。从这里开始lua逻辑
function Main()					
	print("logic start")	 
	print("logic start11")	 
	print("logic start223")	 
	-- coroutine.start(ImprotFiles)
	-- InitMain()
end

function InitMain()	
	UIWindowFirstLoading.Close()
	MyUnityTool.Find("Canvas/UILayer7/CommonViews"):SetActive(true)
end

--场景切换通知
function OnLevelWasLoaded(level)
	collectgarbage("collect")
	Time.timeSinceLevelLoad = 0
end

function OnApplicationQuit()
	
end

function ImprotFiles()
	-- coroutine.wait(1)
	InitMain()
end