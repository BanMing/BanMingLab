--region *.lua
--Date
--此文件由[BabeLua]插件自动生成

LocationInfo = 
{
	latitude,
	longitude,
	locInfo,
}

--endregion
LocationManager =
{
    location = nil,
    latitude,--纬度(-1表示获取失败)
    longitude,--经度(-1表示获取失败)
    timeStamp,--时间戳(-1表示获取失败)
    enable = true,
    gotLoc = false,
    appKey = VersionConfig.LocationKey or "",
    locInfo = "", --位置信息
    reqId = "", --
	EARTH_RADIUS = 6378.137,--地球半径
	PI = 3.1415926,
	LocationList={}
}


local mapInfoUrl = "https://api.map.baidu.com/geocoder/v2/?ak=" --百度sdk
function LocationManager.Init()                         --初始化
    if VersionConfig.NeedLocation then
        ResourcesManager.Instance:GetInstanceSync("LocationRoot.prefab","",LocationManager.CreateRoot)
    else
        LocationManager.enable = false
    end
end

function LocationManager.CreateRoot(root)
    print("create root")
    LocationManager.location = MyUnityTool.Find("LocationRoot(Clone)"):GetComponent("Location")
    if LocationManager.location ~= nil then
        LocationManager.location:GetLoc(LocationManager.OnGetLoc)
    else
        print("not suppost to get location when CreateRoot")
        LocationManager.enable = false
    end
end

function LocationManager.SetParam(accuracy,updateDistance)--设置定位参数（精度，更新距离）
    if not LocationManager.enable then
        print("not suppost to get location when SetParam")
        return
    end
    LocationManager.location:SetGpsAccuracy(accuracy)
    LocationManager.location:SetGpsUpdateDistance(updateDistance)
end
------------------------------------------------------------------------------------------------------------------------------
function LocationManager.GetLoc()                       --获取位置
    if not LocationManager.enable then
        print("not suppost to get location when GetLoc")
        return
    end
    if LocationManager.location == nil then
        return nil
    elseif LocationManager.gotLoc then
        return  LocationManager.location:GetLocation(),LocationManager.location:GetLatitudee()
    else
        print("not got location yet,try again latter")
        LocationManager.location:Getloc(LocationManager.OnGetLoc)
        return nil
    end
end

function LocationManager.OnGetLoc(vector)               --获取位置回调
    if not LocationManager.enable then
        print("not suppost to get location when OnGetLoc")
        return
    end
    LocationManager.longitude = vector.x
    LocationManager.latitude = vector.y
    LocationManager.timeStamp = os.time()
    LocationManager.gotLoc = true
    print("got location :" .. LocationManager.latitude .. "," .. LocationManager.longitude)
    --直接转发 不再获取具体地址
    RenMaiMsgHandler.Handle_CMD_GP_GET_GPS_ADDRESS_REQ(LocationManager.reqId,UserModel.thePlayer.dwUserID)
    -- LocationManager.GetInfoOfLocation()
end

function LocationManager.GetInfoOfLocation(callback)    --获取地址详细信息(只作为回调，一般不被外部调用)
    if not LocationManager.enable then
        print("not suppost to get location when GetInfoOfLocation")
        return
    end
    if LocationManager.gotLoc then
        local url = mapInfoUrl
         .. LocationManager.appKey 
         .. "&location="
         .. LocationManager.latitude
         .. ","
         .. LocationManager.longitude
        print("url = ", url)
        if callback then
            HTTPTool.GetText(url,callback)
        else
            HTTPTool.GetText(url,LocationManager.OnGetInfo)--发送获取地址链接
        end
    else
        print("not got location yet,try again latter")
        LocationManager.location:GetLoc(LocationManager.OnGetLoc)
    end
end

function LocationManager.OnGetInfo(text)    --获取地址详细信息回调
	Log("yellow", "获取地址详细信息回调:", text)
--    LocationManager.locInfo = MyFileUtil.GetValueFromXmlString(text,"formatted_address")--取地址    
	LocationManager.locInfo = utils.GetXMLValue(text, "formatted_address")
    --to do 上传地址
    RenMaiMsgHandler.Handle_CMD_GP_GET_GPS_ADDRESS_REQ(LocationManager.reqId,UserModel.thePlayer.dwUserID,LocationManager.locInfo)
    LocationManager.reqId = ""
end

-------------------------------------------------------------------------------------------------------------------------------

function LocationManager.UpdateLocation(reqId)       --更新位置信息(当接到服务器获取地址信息请求时)
    if not LocationManager.enable then
        print("not suppost to get location when UpdateLocation")
        RenMaiMsgHandler.Handle_CMD_GP_GET_GPS_ADDRESS_REQ(reqId,UserModel.thePlayer.dwUserID,LocationManager.locInfo)
--        LocationManager.reqId = ""
        return
        --to do 返回GPS不可用消息
    end
    local deltaTime = tonumber(os.time()) - ( LocationManager.timeStamp ~= -1 and LocationManager.timeStamp or -200000)

    if deltaTime > 180 then
        LocationManager.location:UpdateLoc(LocationManager.OnGetLoc)        
    else
        RenMaiMsgHandler.Handle_CMD_GP_GET_GPS_ADDRESS_REQ(reqId,UserModel.thePlayer.dwUserID,LocationManager.locInfo)
--        LocationManager.reqId = ""
    end
end

function LocationManager.rad(d)
   return d * LocationManager.PI / 180.0
end
-- 获取两经纬度间的距离  返回单位 km
--function LocationManager.GetDistance(lat1, lng1, lat2, lng2)
--    if lat1 == -1 or lng1 == -1 or lat2 == -1 or lng2 == -1 then
--        return -1
--    end
--	local radLat1 = LocationManager.rad(lat1)
--	local radLat2 = LocationManager.rad(lat2)
--	local a = radLat1 - radLat2
--	local b = LocationManager.rad(lng1) - LocationManager.rad(lng2)

--	local s = 2 * math.asin( math.sqrt( math.sin(a/2) * math.sin(a/2) +
--    math.cos(radLat1) * math.cos(radLat2) * math.sin(b/2) * math.sin(b/2)))
--    s = s * LocationManager.EARTH_RADIUS
--    return s
--end
-- 赤道半径(单位米)
local  EARTH_RADIUS = 6378137
local  math_rad = math.rad
local  math_abs = math.abs
local  math_pi = math.pi
local  math_cos = math.cos
local  math_sin = math.sin

-- 获取两经纬度间的距离  返回单位 km
function LocationManager.GetDistance(lon1, lat1, lon2, lat2)
    local radLat1 = math_rad(lat1)
    local radLat2 = math_rad(lat2)

    local radLon1 = math_rad(lon1)

    local radLon2 = math_rad(lon2)

    if (radLat1 < 0)  then
        radLat1 = math_pi / 2 + math_abs(radLat1)-- south  
    end
    if (radLat1 > 0)  then
        radLat1 = math_pi / 2 - math_abs(radLat1)-- north  
    end
    if (radLon1 < 0)  then
        radLon1 = math_pi * 2 - math_abs(radLon1)-- west  
    end
    if (radLat2 < 0)  then
        radLat2 = math_pi / 2 + math_abs(radLat2)-- south  
    end
    if (radLat2 > 0)  then
        radLat2 = math_pi / 2 - math_abs(radLat2)-- north 
    end 
    if (radLon2 < 0)  then
        radLon2 = math_pi * 2 - math_abs(radLon2)-- west 
    end 
    local x1 = EARTH_RADIUS * math_cos(radLon1) * math_sin(radLat1)  
    local y1 = EARTH_RADIUS * math_sin(radLon1) * math_sin(radLat1)  
    local z1 = EARTH_RADIUS * math_cos(radLat1)  

    local x2 = EARTH_RADIUS * math_cos(radLon2) * math_sin(radLat2)  
    local y2 = EARTH_RADIUS * math_sin(radLon2) * math_sin(radLat2)  
    local z2 = EARTH_RADIUS * math_cos(radLat2)  

    local d = math.sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2)+ (z1 - z2) * (z1 - z2))  

    --余弦定理求夹角  
    local theta = math.acos((EARTH_RADIUS * EARTH_RADIUS + EARTH_RADIUS * EARTH_RADIUS - d * d) / (2 * EARTH_RADIUS * EARTH_RADIUS))  
    local dist = theta * EARTH_RADIUS  

    return dist
end


function LocationManager.CheckLonLat(lon1, lat1, lon2, lat2)

    if lon1 and lat1 and lon2 and lat2 then
        lon1 = tonumber(lon1)
        lat1 = tonumber(lat1)
        lon2 = tonumber(lon2)
        lat2 = tonumber(lat2)
        if lon1 == -1 or lon2 == -1 or lat1 == -1 or lat2 == -1 or
            lon1 == 0 or lon2 == 0 or lat1 == 0 or lat2 == 0 then
            return nil
        end

        return true
    end

    return false
end